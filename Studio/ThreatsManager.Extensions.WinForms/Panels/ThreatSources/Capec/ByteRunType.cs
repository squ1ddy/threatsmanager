// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.2.0.44
//  </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#pragma warning disable
namespace ThreatsManager.Extensions.Panels.ThreatSources.Capec
{
    /// <summary>
/// The ByteRunType is used for representing a single byte run from within a raw object.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace="http://cybox.mitre.org/common-2")]
public partial class ByteRunType
{
    #region Private fields
    private IntegerObjectPropertyType _offset;
    private EndiannessType _byte_Order;
    private IntegerObjectPropertyType _file_System_Offset;
    private IntegerObjectPropertyType _image_Offset;
    private IntegerObjectPropertyType _length;
    private List<HashType> _hashes;
    private object _byte_Run_Data;
    #endregion
    
    /// <summary>
    /// ByteRunType class constructor
    /// </summary>
    public ByteRunType()
    {
        _hashes = new List<HashType>();
        _length = new IntegerObjectPropertyType();
        _image_Offset = new IntegerObjectPropertyType();
        _file_System_Offset = new IntegerObjectPropertyType();
        _byte_Order = new EndiannessType();
        _offset = new IntegerObjectPropertyType();
    }
    
    /// <summary>
    /// The Offset field specifies the offset of the beginning of the byte run as measured from the beginning of the object.
    /// </summary>
    public IntegerObjectPropertyType Offset
    {
        get => _offset;
        set => _offset = value;
    }
    
    /// <summary>
    /// The Byte_Order field specifies the endianness of the unpacked (e.g., unencoded, unencrypted, etc.) data contained within the Byte_Run_Data field.
    /// </summary>
    public EndiannessType Byte_Order
    {
        get => _byte_Order;
        set => _byte_Order = value;
    }
    
    /// <summary>
    /// The File_System_Offset field is relevant only for byte runs of files in forensic analysis.It specifies the offset of the beginning of the byte run as measured from the beginning of the relevant file system.
    /// </summary>
    public IntegerObjectPropertyType File_System_Offset
    {
        get => _file_System_Offset;
        set => _file_System_Offset = value;
    }
    
    /// <summary>
    /// The Image_Offset field is provided for forensic analysis purposes and specifies the offset of the beginning of the byte run as measured from the beginning of the relevant forensic image.
    /// </summary>
    public IntegerObjectPropertyType Image_Offset
    {
        get => _image_Offset;
        set => _image_Offset = value;
    }
    
    /// <summary>
    /// The Length field specifies the number of bytes in the byte run.
    /// </summary>
    public IntegerObjectPropertyType Length
    {
        get => _length;
        set => _length = value;
    }
    
    /// <summary>
    /// The Hashes field contains computed hash values for this the data in this byte run.
    /// </summary>
    [XmlArrayItem("Hash", IsNullable=false)]
    public List<HashType> Hashes
    {
        get => _hashes;
        set => _hashes = value;
    }
    
    /// <summary>
    /// The Byte_Run_Data field contains a raw dump of the byte run data, typically enclosed within an XML CDATA section.
    /// </summary>
    public object Byte_Run_Data
    {
        get => _byte_Run_Data;
        set => _byte_Run_Data = value;
    }
}
}
#pragma warning restore