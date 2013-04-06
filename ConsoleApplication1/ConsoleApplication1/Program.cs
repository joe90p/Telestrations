using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringWriter();

            using (var xml = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = true }))
            {
                xml.WriteStartElement("root");

                using (var inner = XmlWriter.Create(xml, new XmlWriterSettings() { WriteEndDocumentOnClose = false, CloseOutput = false }))
                {
                    inner.WriteStartElement("payload1");
                    //ThirdPartyLibrary.Serialise(results, inner);
                }

                xml.WriteStartElement("payload2");
            }

            //sb.ToString().Dump();
        }
    }
}
