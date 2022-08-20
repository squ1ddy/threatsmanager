﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Aspects.Dependencies;
using PostSharp.Patterns.Collections;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;
using PostSharp.Serialization;
using ThreatsManager.Engine.ObjectModel.ThreatsMitigations;
using ThreatsManager.Interfaces.ObjectModel;
using ThreatsManager.Interfaces.ObjectModel.ThreatsMitigations;
using ThreatsManager.Utilities;

namespace ThreatsManager.Engine.Aspects
{
    //#region Additional placeholders required.
    //[Child]
    //[JsonProperty("vulnerabilities")]
    //private IList<IVulnerability> _vulnerabilities { get; set; }
    //#endregion    

    [PSerializable]
    [AspectTypeDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, typeof(NotifyPropertyChangedAttribute))]
    public class VulnerabilitiesContainerAspect : InstanceLevelAspect
    {
        #region Extra elements to be added.
        [ImportMember(nameof(_vulnerabilities))]
        public Property<IList<IVulnerability>> _vulnerabilities;
        #endregion

        #region Implementation of interface IVulnerabilitiesContainer.
        private Action<IVulnerabilitiesContainer, IVulnerability> _vulnerabilityAdded;

        [OnEventAddHandlerAdvice]
        [MulticastPointcut(MemberName = "VulnerabilityAdded", Targets = PostSharp.Extensibility.MulticastTargets.Event, Attributes = PostSharp.Extensibility.MulticastAttributes.AnyVisibility)]
        public void OnVulnerabilityAddedAdd(EventInterceptionArgs args)
        {
            if (_vulnerabilityAdded == null || !_vulnerabilityAdded.GetInvocationList().Contains(args.Handler))
            {
                _vulnerabilityAdded += (Action<IVulnerabilitiesContainer, IVulnerability>)args.Handler;
                args.ProceedAddHandler();
            }
        }

        [OnEventRemoveHandlerAdvice(Master = nameof(OnVulnerabilityAddedAdd))]
        public void OnVulnerabilityAddedRemove(EventInterceptionArgs args)
        {
            _vulnerabilityAdded -= (Action<IVulnerabilitiesContainer, IVulnerability>)args.Handler;
            args.ProceedRemoveHandler();
        }

        private Action<IVulnerabilitiesContainer, IVulnerability> _vulnerabilityRemoved;

        [OnEventAddHandlerAdvice]
        [MulticastPointcut(MemberName = "VulnerabilityRemoved", Targets = PostSharp.Extensibility.MulticastTargets.Event, Attributes = PostSharp.Extensibility.MulticastAttributes.AnyVisibility)]
        public void OnVulnerabilityRemovedAdd(EventInterceptionArgs args)
        {
            if (_vulnerabilityRemoved == null || !_vulnerabilityRemoved.GetInvocationList().Contains(args.Handler))
            {
                _vulnerabilityRemoved += (Action<IVulnerabilitiesContainer, IVulnerability>)args.Handler;
                args.ProceedAddHandler();
            }
        }

        [OnEventRemoveHandlerAdvice(Master = nameof(OnVulnerabilityRemovedAdd))]
        public void OnVulnerabilityRemovedRemove(EventInterceptionArgs args)
        {
            _vulnerabilityRemoved -= (Action<IVulnerabilitiesContainer, IVulnerability>)args.Handler;
            args.ProceedRemoveHandler();
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 1)]
        [CopyCustomAttributes(typeof(JsonIgnoreAttribute), OverrideAction = CustomAttributeOverrideAction.Ignore)]
        [JsonIgnore]
        public IEnumerable<IVulnerability> Vulnerabilities => _vulnerabilities?.Get()?.AsEnumerable();

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 1)]
        public IVulnerability GetVulnerability(Guid id)
        {
            return _vulnerabilities?.Get()?.FirstOrDefault(x => x.Id == id);
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 1)]
        public IVulnerability GetVulnerabilityByWeakness(Guid weaknessId)
        {
            return _vulnerabilities?.Get()?.FirstOrDefault(x => x.WeaknessId == weaknessId);
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 11)]
        public void Add(IVulnerability vulnerability)
        {
            if (vulnerability == null)
                throw new ArgumentNullException(nameof(vulnerability));
            if (vulnerability is IThreatModelChild child && child.Model != (Instance as IThreatModelChild)?.Model)
                throw new ArgumentException();

            using (UndoRedoManager.OpenScope("Add Vulnerability"))
            {
                var vulnerabilities = _vulnerabilities?.Get();
                if (vulnerabilities == null)
                {
                    vulnerabilities = new AdvisableCollection<IVulnerability>();
                    _vulnerabilities?.Set(vulnerabilities);
                }

                vulnerabilities.Add(vulnerability);
                UndoRedoManager.Attach(vulnerability);
                if (Instance is IVulnerabilitiesContainer container)
                    _vulnerabilityAdded?.Invoke(container, vulnerability);
            }
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 10)]
        public IVulnerability AddVulnerability(IWeakness weakness)
        {
            IVulnerability result = null;

            if (Instance is IIdentity identity)
            {
                IThreatModel model = (Instance as IThreatModel) ?? (Instance as IThreatModelChild)?.Model;

                if (model != null)
                {
                    if (_vulnerabilities?.Get()?.All(x => x.WeaknessId != weakness.Id) ?? true)
                    {
                        result = new Vulnerability(weakness);
                        Add(result);
                    }
                }
            }

            return result;
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrFail, LinesOfCodeAvoided = 10)]
        public bool RemoveVulnerability(Guid id)
        {
            bool result = false;

            var vulnerability = GetVulnerability(id);
            if (vulnerability != null)
            {
                using (UndoRedoManager.OpenScope("Remove Vulnerability"))
                {
                    result = _vulnerabilities?.Get()?.Remove(vulnerability) ?? false;
                    if (result)
                    {
                        UndoRedoManager.Detach(vulnerability);
                        if (Instance is IVulnerabilitiesContainer container)
                            _vulnerabilityRemoved?.Invoke(container, vulnerability);
                    }
                }
            }

            return result;
        }
        #endregion
    }
}
