using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using GestureRecognition.Data.Interfaces;

namespace GestureRecognition.Data.DataSerialization
{
    public class SerializeToXml<T>
    {
        public static void Serialize<T>(T model, string output)
        {
            var serializer = new XmlSerializer(typeof(T));
            var writer = new StreamWriter(@"C:\Users\macki\Desktop\magisterka\GestureRecognition\Output\" + output +  ".xml");
            serializer.Serialize(writer, model);
            writer.Close();
        }

        public static void Serialize<T>(List<T> model, string output)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            var writer = new StreamWriter(@"C:\Users\macki\Desktop\magisterka\GestureRecognition\Output\" + output + ".xml");
            serializer.Serialize(writer, model);
            writer.Close();
        }

        public static List<T> Deserialize(string input)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<T>));
            TextReader textReader = new StreamReader(@"C:\Users\macki\Desktop\magisterka\GestureRecognition\Output\" + input + ".xml");
            var obj = (List<T>)deserializer.Deserialize(textReader);
            textReader.Close();
            return obj;
        }

        public static void SerializeList<T>(T model, string input)
        {
            var items = (List<T>)SerializeToXml<T>.Deserialize(input);
            items.Add(model);
            SerializeToXml<T>.Serialize(items, input);
        }
    }
}
