using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;
using System.Net;
using System.Xml;

namespace JsonToXmlSoln
{
    class Program
    {
        static void Main()
        {

            WebClient client = new WebClient();

            //place holder for url and file name
            //var holder = ConfigurationManager.AppSettings[""];

            Stream stream = client.OpenRead("url");
            StreamReader streamReader = new StreamReader(stream);

            JObject jObject = JObject.Parse(streamReader.ReadLine());
            string jsonString = JsonConvert.SerializeObject(jObject);

            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonString, "Root");

            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter("TestJson.xml", null);
            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
            doc.Save(xmlTextWriter);
            
            stream.Close();
        }
    }
}
