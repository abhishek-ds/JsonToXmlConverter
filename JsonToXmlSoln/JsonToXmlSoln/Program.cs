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

            //DS--Read Url through App.Config
            string url = ConfigurationManager.AppSettings["TargetUrl"];
            Stream stream = client.OpenRead(url);

            //DS--Initializes a new instance of the StreamReader class
            StreamReader streamReader = new StreamReader(stream);
            JObject jObject = JObject.Parse(streamReader.ReadLine());

            //DS--Create a JSON String
            string jsonString = JsonConvert.SerializeObject(jObject);

            //DS--JSON to XML Conversion
            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonString, "Root");
            
            //DS--Write to a file in XML format
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter("TestJson.xml", null);
            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
            doc.Save(xmlTextWriter);            
            stream.Close();
        }
    }
}
