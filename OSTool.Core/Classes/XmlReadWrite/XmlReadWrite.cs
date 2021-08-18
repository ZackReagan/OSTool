using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using static OSTool.Core.CrashLogger;

namespace OSTool.Core
{
    public class XmlReadWrite
    {
        public static string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OSTool");
        public static string path = Path.Combine(root, "OSTool.xml");

        public static void Create()
        {
            try
            {
                if (!Directory.Exists(root)) { Directory.CreateDirectory(root); }

                if (!File.Exists(path))
                {
                    XDocument xmlWrite = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Data",
                        new XElement("ProgramOptions",
                        new XElement("FirstTime", "True")
                        )
                    ));
                    xmlWrite.Save(path);
                }
                else
                {
                    foreach (var xml in XDocument.Load(path).Descendants("Data").DescendantNodes().OfType<XElement>().ToList())
                    {
                        if (xml.Parent.Name.ToString() == "Data")
                        {
                            if (!xml.HasElements)
                            {
                                Remove(xml.Parent.Name.ToString(), xml.Name.ToString());
                            }
                        }
                    }
                    if (!Exist("ProgramOptions", "", XmlExistEnums.List)) { Write("", "", "ProgramOptions", XmlWriteEnums.List); }
                    if (!Exist("ProgramOptions", "FirstTime", 0)) { Write("FirstTime", "True", "ProgramOptions", XmlWriteEnums.Element); }
                }
            }
            catch (Exception error)
            {
                Log(error);
                Dialog.Show("XML creation failed", "Program closing now. For more info, read the log(s).");
                Environment.Exit(0);
            }
        }

        public static void Write(string getDescendant, string elementValue, string elementListName = null, XmlWriteEnums type = XmlWriteEnums.Value)
        {
            XDocument load = XDocument.Load(path);

            if (type == XmlWriteEnums.Value)
            {
                load.Descendants(XmlConvert.EncodeName(getDescendant)).Single().Value = elementValue;
            }
            else if (type == XmlWriteEnums.Element)
            {
                if (!Exist(elementListName, getDescendant, 0))
                {
                    load.Root.Element(elementListName).Add(new XElement(XmlConvert.EncodeName(getDescendant), elementValue));
                }
            }
            else
            {
                if (!Exist(elementListName, "", XmlExistEnums.List))
                {
                    load.Root.Add(new XElement(XmlConvert.EncodeName(elementListName)));
                }
            }
            load.Save(path);
        }

        public static bool Exist(string getElement, string childElement, XmlExistEnums type)
        {
            if (type == XmlExistEnums.Element)
            {
                return XDocument.Load(path).Root.Element(getElement).Element(XmlConvert.EncodeName(childElement)) != null;
            }
            else
            {
                return XDocument.Load(path).Root.Element(getElement) != null;
            }
        }

        public static void Remove(string getDescendant, string childElement)
        {
            try
            {
                XDocument load = XDocument.Load(path);
                load.Descendants(getDescendant).Elements(childElement).Remove();
                load.Save(path);
            }
            catch (Exception error)
            {
                Log(error);
                Dialog.Show("XML removal failed", "Program closing now. For more info, read the log(s).");
                Environment.Exit(0);
            }
        }

        public static List<string> List(string getDescendant, string expectedvalue = null, XmlListEnums type = XmlListEnums.Element)
        {
            List<string> elements = XDocument.Load(path).Descendants(getDescendant).DescendantNodes().OfType<XElement>().Select(x => XmlConvert.DecodeName(x.Name.ToString())).Distinct().ToList();
            List<string> values = new List<string>();

            if (type == XmlListEnums.Element)
            {
                return elements;
            }
            else
            {
                foreach (var item in elements)
                {
                    if (Read(item).Contains(expectedvalue))
                    {
                        values.Add(item);
                    }
                }

                if (values.Count != 0)
                {
                    return values;
                }
                else
                {
                    return new List<string>();
                }
            }
        }

        public static string Read(string getDescendant)
        {
            return XDocument.Load(path).Descendants(XmlConvert.EncodeName(getDescendant)).Single().Value;
        }
    }
}