using System;
using System.Xml.Serialization;
using System.IO;
using System.Text.Json;

namespace SerializationDeserialization
{

    public class Book
    {
        public string title { get; set; }

        public string author { get; set; }

    }

    class JsonOperations
    {
        public static Tuple<string, string> writeJson()
        {
            Book book = new Book();
            book.title = "BookTitle";
            book.author = "BookAuthor";
            string result;
            result = JsonSerializer.Serialize(book);
            string fileName = "SampleJsonFile.json";
            File.WriteAllText(fileName, result);
            return Tuple.Create(result, fileName);
        }

        public static void readJson(string fileName)
        {
            string json = File.ReadAllText(fileName);
            Console.WriteLine(json);
        }
    }

    class XMLOperations
    {
        static XmlSerializer writer = new XmlSerializer(typeof(Book));
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
        public static void writeStuff()
        {
            Book book = new Book();
            book.title = "Liam Story";
            book.author = "Liam";


            FileStream file = File.Create(path);

            writer.Serialize(file, book);
            file.Close();
        }

        public static void readStuff()
        {
            StreamReader file = new StreamReader(path);
            Book overview = (Book)writer.Deserialize(file);
            file.Close();
            Console.Write(overview.author);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            XMLOperations.writeStuff();
            XMLOperations.readStuff();
            Console.ReadLine();
            Tuple<string, string> json = JsonOperations.writeJson();
            JsonOperations.readJson(json.Item2);
            Console.ReadLine();
        }
    }
}