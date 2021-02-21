// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.2.0.44
//  </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Xml.Serialization;

#pragma warning disable
namespace ThreatsManager.Extensions.Panels.ThreatSources.Capec
{
    /// <summary>
/// The PatternTypeEnum type is a non-exhaustive enumeration of potentially relevant pattern types.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
[Serializable]
[XmlType(Namespace="http://cybox.mitre.org/common-2")]
public enum PatternTypeEnum
{
    /// <summary>
    /// Specifies the regular expression pattern type.
    /// </summary>
    Regex,
    /// <summary>
    /// Specifies the binary (bit operations) pattern type.
    /// </summary>
    Binary,
    /// <summary>
    /// Specifies the XPath 1.0 expression pattern type.
    /// </summary>
    XPath,
}
}
#pragma warning restore