namespace Dal;

using DO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

/// <summary>
/// Provides XML utility methods for the DAL XML layer.
/// </summary>
static class XMLTools
{
    /// <summary>
    /// The directory path for XML files.
    /// </summary>
    const string s_xmlDir = @"..\xml\";

    /// <summary>
    /// Initializes the XMLTools class.
    /// </summary>
    static XMLTools()
    {
        if (!Directory.Exists(s_xmlDir))
            Directory.CreateDirectory(s_xmlDir);
    }

    #region SaveLoadWithXMLSerializer
        /// <summary>
    /// Saves a list of objects to an XML file using XML serialization.
    /// </summary>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    /// <param name="list">The list to save.</param>
    /// <param name="xmlFileName">The name of the XML file.</param>
    public static void SaveListToXMLSerializer<T>(List<T> list, string xmlFileName) where T : class
    {
        string xmlFilePath = s_xmlDir + xmlFileName;

        try
        {
            using FileStream file = new(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            new XmlSerializer(typeof(List<T>)).Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xmlDir + xmlFilePath}, {ex.Message}");
        }
    }
        /// <summary>
    /// Loads a list of objects from an XML file using XML serialization.
    /// </summary>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    /// <param name="xmlFileName">The name of the XML file.</param>
    /// <returns>The loaded list of objects.</returns>
    public static List<T> LoadListFromXMLSerializer<T>(string xmlFileName) where T : class
    {
        string xmlFilePath = s_xmlDir + xmlFileName;

        try
        {
            if (!File.Exists(xmlFilePath)) return new();
            using FileStream file = new(xmlFilePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T>));
            return x.Deserialize(file) as List<T> ?? new();
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {xmlFilePath}, {ex.Message}");
        }
    }
    #endregion

    #region SaveLoadWithXElement
        /// <summary>
    /// Saves an XElement to an XML file.
    /// </summary>
    /// <param name="rootElem">The root XElement to save.</param>
    /// <param name="xmlFileName">The name of the XML file.</param>
    public static void SaveListToXMLElement(XElement rootElem, string xmlFileName)
    {
        string xmlFilePath = s_xmlDir + xmlFileName;

        try
        {
            rootElem.Save(xmlFilePath);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xmlDir + xmlFilePath}, {ex.Message}");
        }
    }
        /// <summary>
    /// Loads an XElement from an XML file.
    /// </summary>
    /// <param name="xmlFileName">The name of the XML file.</param>
    /// <returns>The loaded XElement.</returns>
    public static XElement LoadListFromXMLElement(string xmlFileName)
    {
        string xmlFilePath = s_xmlDir + xmlFileName;

        try
        {
            if (File.Exists(xmlFilePath))
                return XElement.Load(xmlFilePath);
            XElement rootElem = new(xmlFileName);
            rootElem.Save(xmlFilePath);
            Console.WriteLine(rootElem.ToString());
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xmlDir + xmlFilePath}, {ex.Message}");
        }
    }
    #endregion

    #region XmlConfig
        /// <summary>
    /// Gets and increments an integer configuration value from an XML file.
    /// </summary>
    /// <param name="xmlFileName">The XML file name.</param>
    /// <param name="elemName">The element name.</param>
    /// <returns>The integer value before increment.</returns>
    public static int GetAndIncreaseConfigIntVal(string xmlFileName, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(xmlFileName);
        int nextId = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert:  {xmlFileName}, {elemName}");
        root.Element(elemName)?.SetValue((nextId + 1).ToString());
        XMLTools.SaveListToXMLElement(root, xmlFileName);
        return nextId;
    }
        /// <summary>
    /// Gets an integer configuration value from an XML file.
    /// </summary>
    /// <param name="xmlFileName">The XML file name.</param>
    /// <param name="elemName">The element name.</param>
    /// <returns>The integer value.</returns>
    public static int GetConfigIntVal(string xmlFileName, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(xmlFileName);
        int num = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert:  {xmlFileName}, {elemName}");
        return num;
    }
    //added
        /// <summary>
    /// Gets a TimeSpan configuration value from an XML file.
    /// </summary>
    /// <param name="xmlFileName">The XML file name.</param>
    /// <param name="elemName">The element name.</param>
    /// <returns>The TimeSpan value.</returns>
    public static TimeSpan GetConfigTimeSpanVal(string xmlFileName, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(xmlFileName);
        TimeSpan ts = root.ToTimeSpanNullable(elemName) ?? throw new FormatException($"Can't convert: {xmlFileName}, {elemName}");
        return ts;
    }
        /// <summary>
    /// Gets a DateTime configuration value from an XML file.
    /// </summary>
    /// <param name="xmlFileName">The XML file name.</param>
    /// <param name="elemName">The element name.</param>
    /// <returns>The DateTime value.</returns>
    public static DateTime GetConfigDateVal(string xmlFileName, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(xmlFileName);
        //XElement clockElement = root.Element(elemName)
        //?? throw new InvalidOperationException($"Element '{elemName}' not found in XML.");
        DateTime dt = root.ToDateTimeNullable(elemName) ?? throw new FormatException($"can't convert:  {xmlFileName}, {elemName}");
        return dt;
    }
        /// <summary>
    /// Sets an integer configuration value in an XML file.
    /// </summary>
    /// <param name="xmlFileName">The XML file name.</param>
    /// <param name="elemName">The element name.</param>
    /// <param name="elemVal">The integer value to set.</param>
    public static void SetConfigIntVal(string xmlFileName, string elemName, int elemVal)
    {
        XElement root = XMLTools.LoadListFromXMLElement(xmlFileName);
        root.Element(elemName)?.SetValue((elemVal).ToString());
        XMLTools.SaveListToXMLElement(root, xmlFileName);
    }
    //added
        /// <summary>
    /// Sets a TimeSpan configuration value in an XML file.
    /// </summary>
    /// <param name="xmlFileName">The XML file name.</param>
    /// <param name="elemName">The element name.</param>
    /// <param name="elemVal">The TimeSpan value to set.</param>
    public static void SetConfigTimeSpanVal(string xmlFileName, string elemName, TimeSpan elemVal)
    {
        XElement root = XMLTools.LoadListFromXMLElement(xmlFileName);
        root.Element(elemName)?.SetValue(elemVal.ToString());
        XMLTools.SaveListToXMLElement(root, xmlFileName);
    }
        /// <summary>
    /// Sets a DateTime configuration value in an XML file.
    /// </summary>
    /// <param name="xmlFileName">The XML file name.</param>
    /// <param name="elemName">The element name.</param>
    /// <param name="elemVal">The DateTime value to set.</param>
    public static void SetConfigDateVal(string xmlFileName, string elemName, DateTime elemVal)
    {
        XElement root = XMLTools.LoadListFromXMLElement(xmlFileName);
        root.Element(elemName)?.SetValue((elemVal).ToString());
        XMLTools.SaveListToXMLElement(root, xmlFileName);
    }
    #endregion


    #region ExtensionFuctions
        /// <summary>
    /// Converts an XElement value to a nullable enum of type T.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="element">The XElement.</param>
    /// <param name="name">The element name.</param>
    /// <returns>The enum value if conversion is successful; otherwise, null.</returns>
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;
        /// <summary>
    /// Converts an XElement value to a nullable DateTime.
    /// </summary>
    /// <param name="element">The XElement.</param>
    /// <param name="name">The element name.</param>
    /// <returns>The DateTime value if conversion is successful; otherwise, null.</returns>
    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;
    //added
        /// <summary>
    /// Converts an XElement value to a nullable TimeSpan.
    /// </summary>
    /// <param name="element">The XElement.</param>
    /// <param name="name">The element name.</param>
    /// <returns>The TimeSpan value if conversion is successful; otherwise, null.</returns>
    public static TimeSpan? ToTimeSpanNullable(this XElement element, string name) =>
    TimeSpan.TryParse((string?)element.Element(name), out var result) ? (TimeSpan?)result : null;
        /// <summary>
    /// Converts an XElement value to a nullable double.
    /// </summary>
    /// <param name="element">The XElement.</param>
    /// <param name="name">The element name.</param>
    /// <returns>The double value if conversion is successful; otherwise, null.</returns>
    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;
        /// <summary>
    /// Converts an XElement value to a nullable int.
    /// </summary>
    /// <param name="element">The XElement.</param>
    /// <param name="name">The element name.</param>
    /// <returns>The int value if conversion is successful; otherwise, null.</returns>
    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;
    #endregion

}