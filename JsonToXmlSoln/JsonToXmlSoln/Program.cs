using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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

            //DS--Visual Indicators 1
            Console.WriteLine("Application Started, might take a while to Process...");

            //DS--Read Url through App.Config
            string url = ConfigurationManager.AppSettings["TargetUrl"];
            
            //DS--Path and file name to Save as XML from App.config
            string fileName = ConfigurationManager.AppSettings["FileName"];
            Stream stream = client.OpenRead(url);
                        
            //DS--Initializes a new instance of the StreamReader class
            StreamReader streamReader = new StreamReader(stream);
            JObject jObject = JObject.Parse(streamReader.ReadLine());
            
            //DS--Visual Indicators
            Console.WriteLine("JSON Parsing...");

            //DS--Create a JSON String
            string jsonString = JsonConvert.SerializeObject(jObject);            
           
            //DS--Visual Indicators
            Console.WriteLine("Xml Conversion Started...");

            //DS--JSON to XML Conversion
            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonString, "Root");

            //DS--Visual Indicators
            Console.WriteLine("Xml is being Saved, might take a while..");

            //DS--Write to a file in XML format
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(fileName, null);
            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
            doc.Save(xmlTextWriter);            
            stream.Close();
        }
    }
}
