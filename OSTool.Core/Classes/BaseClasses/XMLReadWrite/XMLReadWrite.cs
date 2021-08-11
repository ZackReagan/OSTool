using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using static OSTool.Core.CrashLogger;

namespace OSTool.Core
{
    public class XmlFile
    {
        public static string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OSTool");
        public static string path = Path.Combine(root, "OSTool.xml");

        public static void xmlCreate()
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
                        new XElement("FirstTime", "True"),
                        new XElement("DisplayWarning", "True")
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
                            if (xml.HasElements == false) { xmlRemove(xml.Parent.Name.ToString(), xml.Name.ToString()); }
                        }
                    }
                    if (xmlExist("ProgramOptions", "", 2) == false) { xmlWrite("ProgramOptions", "", "", 2); }
                    if (xmlExist("ProgramOptions", "FirstTime", 0) == false) { xmlWrite("FirstTime", "True", "ProgramOptions", 1); }
                    if (xmlExist("ProgramOptions", "DisplayWarning", 0) == false) { xmlWrite("DisplayWarning", "True", "ProgramOptions", 1); }
                }
            }
            catch (Exception error)
            {
                Log(error);
                //XML creation failed. For more info, read the log(s).
            }
        }

        public static void xmlWrite(string getDescendant, string elementValue, string elementListName, int writeType)
        {
            XDocument load = XDocument.Load(path);

            if (writeType == 0)
            {
                load.Descendants(XmlConvert.EncodeName(getDescendant)).Single().Value = elementValue;
                load.Save(path);
            }
            else if (writeType == 1)
            {
                load.Root.Element(elementListName).Add(new XElement(XmlConvert.EncodeName(getDescendant), elementValue));
                load.Save(path);
            }
            else
            {
                load.Root.Add(new XElement(XmlConvert.EncodeName(getDescendant)));
                load.Save(path);
            }
        }

        public static bool xmlExist(string getElement, string childElement, int existType)
        {
            if (existType == 0)
            {
                return XDocument.Load(path).Root.Element(getElement).Element(XmlConvert.EncodeName(childElement)) == null ? false : true;
            }
            else if (existType == 1)
            {
                return XDocument.Load(path).Root.Element(getElement).HasElements;
            }
            else
            {
                return XDocument.Load(path).Root.Element(getElement) == null ? false : true;
            }
        }

        public static void xmlRemove(string getDescendant, string childElement)
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
                //XML removal failed, closing now. For more info, read the log(s).
            }
        }

        public static List<XName> xmlList(string getDescendant)
        {
            return XDocument.Load(path).Descendants(getDescendant).DescendantNodes().OfType<XElement>().Select(x => x.Name).Distinct().ToList();
        }

        public static string xmlRead(string getDescendant)
        {
            return XDocument.Load(path).Descendants(XmlConvert.EncodeName(getDescendant)).Single().Value;
        }

        public static string xmlDecode(string getElement)
        {
            return XmlConvert.DecodeName(getElement);
        }
    }
}