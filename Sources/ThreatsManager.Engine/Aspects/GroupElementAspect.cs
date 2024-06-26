﻿using System;
using System.Linq;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Aspects.Dependencies;
using PostSharp.Patterns.Model;
using PostSharp.Serialization;
using ThreatsManager.Interfaces.ObjectModel;
using ThreatsManager.Interfaces.ObjectModel.Entities;
using ThreatsManager.Utilities;
using ThreatsManager.Utilities.Aspects.Engine;

namespace ThreatsManager.Engine.Aspects
{
    //#region Additional placeholders required.
    //[JsonProperty("parentId")]
    //private Guid _parentId { get; set; }
    //[Reference]
    //[field: NotRecorded]
    //private IGroup _parent { get; set; }
    //#endregion    

    [PSerializable]
    [AspectTypeDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, typeof(SimpleNotifyPropertyChangedAttribute))]
    public class GroupElementAspect : InstanceLevelAspect
    {
        #region Extra elements to be added.
        [ImportMember(nameof(_parentId))]
        public Property<Guid> _parentId;

        [ImportMember(nameof(_parent))]
        public Property<IGroup> _parent;
        #endregion

        #region Implementation of interface IGroupElement.
        private Action<IGroupElement, IGroup, IGroup> _parentChanged;

        [OnEventAddHandlerAdvice]
        [MulticastPointcut(MemberName = "ParentChanged", Targets = PostSharp.Extensibility.MulticastTargets.Event, Attributes = PostSharp.Extensibility.MulticastAttributes.AnyVisibility)]
        public void OnParentChangedAdd(EventInterceptionArgs args)
        {
            if (_parentChanged == null || !_parentChanged.GetInvocationList().Contains(args.Handler))
            {
                _parentChanged += (Action<IGroupElement, IGroup, IGroup>)args.Handler;
                args.ProceedAddHandler();
            }
        }

        [OnEventRemoveHandlerAdvice(Master = nameof(OnParentChangedAdd))]
        public void OnParentChangedRemove(EventInterceptionArgs args)
        {
            _parentChanged -= (Action<IGroupElement, IGroup, IGroup>)args.Handler;
            args.ProceedRemoveHandler();
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 1)]
        public Guid ParentId => _parentId?.Get() ?? Guid.Empty;

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 3)]
        public IGroup Parent
        {
            get
            {
                IGroup result = _parent?.Get();

                var parentId = ParentId;
                if (parentId != Guid.Empty && (result == null || result.Id != parentId) && Instance is IThreatModelChild child)
                {
                    result = child.Model?.GetGroup(parentId);
                    if (result != null)
                        _parent?.Set(result);
                }
                else if (parentId == Guid.Empty && result != null)
                {
                    _parent?.Set(null);
                }

                return result;
            }
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 9)]
        public void SetParent(IGroup parent)
        {
            IGroup oldParent = null;

            var parentId = _parentId?.Get() ?? Guid.Empty;
            if ((parent == null && parentId != Guid.Empty) || (parent != null && parentId != parent.Id))
            {
                using (var scope = UndoRedoManager.OpenScope("Set parent"))
                {
                    oldParent = Parent;
                    _parentId.Set(parent?.Id ?? Guid.Empty);
                    _parent.Set(parent);
                    scope?.Complete();
                }
 
                if (Instance is IGroupElement groupElement)
                    _parentChanged?.Invoke(groupElement, oldParent, parent);
           }
        }
        #endregion
    }
}
