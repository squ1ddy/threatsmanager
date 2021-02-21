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
namespace ThreatsManager.Extensions.Panels.ThreatSources.Capec
{
    /// <summary>
/// This is the enumerated catalog of common attack patterns.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType=true, Namespace="http://capec.mitre.org/capec-2")]
[XmlRoot(Namespace="http://capec.mitre.org/capec-2", IsNullable=false)]
public partial class Environment
{
    #region Private fields
    private string _environment_Title;
    private string _environment_Description;
    private string _id;
    #endregion
    
    [XmlElement(DataType="token")]
    public string Environment_Title
    {
        get => _environment_Title;
        set => _environment_Title = value;
    }
    
    [XmlElement(DataType="token")]
    public string Environment_Description
    {
        get => _environment_Description;
        set => _environment_Description = value;
    }
    
    [XmlAttribute(DataType="ID")]
    public string ID
    {
        get => _id;
        set => _id = value;
    }
}
}
#pragma warning restore