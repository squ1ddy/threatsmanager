// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.2.0.44
//  </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#pragma warning disable
namespace ThreatsManager.Extensions.Panels.ThreatSources.Cwe
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType=true)]
public partial class Common_ConsequenceConsequence_Technical_Impact
{
    #region Private fields
    private Common_ConsequenceConsequence_Technical_ImpactLikelihood _likelihood;
    private Impact_Type _value;
    #endregion
    
    [XmlAttribute]
    public Common_ConsequenceConsequence_Technical_ImpactLikelihood Likelihood
    {
        get
        {
            return _likelihood;
        }
        set
        {
            _likelihood = value;
        }
    }
    
    [XmlText]
    public Impact_Type Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }
}
}
#pragma warning restore