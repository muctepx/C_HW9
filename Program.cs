using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.Xml.Linq;
using System;
using System.Xml.Serialization;
using System.Data;
using System.Text.Json;

namespace Sem_9
{
    /*
     * С сайта о погоде была получена информация о текущей и прошлой (за три дня) погоде в виде JSON. 
     * Напишите класс способный хранить представленную информацию.
     * {"Current":{"Time":"2023-06-18T20:35:06.722127+04:00","Temperature":29,"Weathercode":1,"Windspeed":2.1,"Winddirection":1},"History":[{"Time":"2023-06-17T20:35:06.77707+04:00","Temperature":29,"Weathercode":2,"Windspeed":2.4,"Winddirection":1},{"Time":"2023-06-16T20:35:06.777081+04:00","Temperature":22,"Weathercode":2,"Windspeed":2.4,"Winddirection":1},{"Time":"2023-06-15T20:35:06.777082+04:00","Temperature":21,"Weathercode":4,"Windspeed":2.2,"Winddirection":1}]}


     */

    /*
     Напишите метод для поиска значений в JSON. В качестве JSON можно использовать JSON из предыдущего примера. 
    Метод должен принимать строку-название ключа и возвращать список найденных значений. 
    Используйте например JsonDocument.Parse
     */
    internal class Program
    {


        static string json = """{"Current":{"Time":"2023-06-18T20:35:06.722127+04:00","Temperature":29,"Weathercode":1,"Windspeed":2.1,"Winddirection":1},"History":[{"Time":"2023-06-17T20:35:06.77707+04:00","Temperature":29,"Weathercode":2,"Windspeed":2.4,"Winddirection":1},{"Time":"2023-06-16T20:35:06.777081+04:00","Temperature":22,"Weathercode":2,"Windspeed":2.4,"Winddirection":1},{"Time":"2023-06-15T20:35:06.777082+04:00","Temperature":21,"Weathercode":4,"Windspeed":2.2,"Winddirection":1}]}""";


        static void Main(string[] args)
        {
            var res = (new JsonParser()).ParseJson(json, "Temperature");
            foreach (var line in res)
            {
                Console.WriteLine(line);
            }
        }


    }


    public class JsonParser
    {
        private string? _value;

        private List<string> _results = new List<string>();

        public List<string> ParseJson(string json, string value)
        {
            _value = value;
            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;
            parseElement(root);
            return _results;
        }

        private void parseElement(JsonElement element, bool save = false)
        {

            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    parseObject(element);
                    break;
                case JsonValueKind.Array:
                    parseArray(element);
                    break;
                case JsonValueKind.String:
                    parseString(element, save);
                    break;
                case JsonValueKind.Number:
                    parseNumber(element, save);
                    break;
                case JsonValueKind.True:
                case JsonValueKind.False:
                    ParseBoolean(element);
                    break;
                case JsonValueKind.Null:
                    ParseNull();
                    break;
                default:
                    throw new NotSupportedException("Unsupported JSON value kind: " + element.ValueKind);
            }
        }

        private void parseObject(JsonElement element)
        {
            foreach (var el in element.EnumerateObject())
            {
                Console.WriteLine($"Property = {el.Name}");
                bool save = el.Name == _value;
                parseElement(el.Value, save);
            }
        }

        private void parseArray(JsonElement element)
        {
            foreach (var el in element.EnumerateArray())
            {
                parseElement(el);
            }
        }

        private void parseString(JsonElement element, bool save = false)
        {
            if (save)
            {
                _results.Add(element.GetString());
            }
            Console.WriteLine($"String = {element.GetString()}");
        }

        private void parseNumber(JsonElement element, bool save = false)
        {
            if (save)
            {
                _results.Add(element.GetRawText());
            }
            Console.WriteLine($"Number = {element.GetRawText()}");
        }

        private void ParseBoolean(JsonElement element)
        {
            Console.WriteLine("Boolean value: " + element.GetBoolean());
        }

        private void ParseNull()
        {
            Console.WriteLine("Null value");
        }



    }

    public class WeatherInfo
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public int Weathercode { get; set; }
        public double Windspeed { get; set; }
        public int Winddirection { get; set; }
    }

    public class WeatherData
    {
        public WeatherInfo Current { get; set; }
        public List<WeatherInfo> History { get; set; }
    }




}



/*
{

    internal class Program
{
        // static string json = "{\"Current\":{\"Time\":\"2023-06-18T20:35:06.722127+04:00\",\"Temperature\":29,\r\n * \"Weathercode\":1,\"Windspeed\":2.1,\"Winddirection\":1},\"History\":\r\n * [{\"Time\":\"2023-06-17T20:35:06.77707+04:00\",\"Temperature\":29,\"Weathercode\":2,\r\n * \"Windspeed\":2.4,\"Winddirection\":1},{\"Time\":\"2023-06-16T20:35:06.777081+04:00\",\r\n * \"Temperature\":22,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1},\r\n * {\"Time\":\"2023-06-15T20:35:06.777082+04:00\",\"Temperature\":21,\"Weathercode\":4,\r\n * \"Windspeed\":2.2,\"Winddirection\":1}]}";
        static string json = """{"Current":{"Time":"2023-06-18T20:35:06.722127+04:00","Temperature":29,"Weathercode":1,"Windspeed":2.1,"Winddirection":1},"History":[{"Time":"2023-06-17T20:35:06.77707+04:00","Temperature":29,"Weathercode":2,"Windspeed":2.4,"Winddirection":1},{"Time":"2023-06-16T20:35:06.777081+04:00","Temperature":22,"Weathercode":2,"Windspeed":2.4,"Winddirection":1},{"Time":"2023-06-15T20:35:06.777082+04:00","Temperature":21,"Weathercode":4,"Windspeed":2.2,"Winddirection":1}""";


        static void Main(string[] args)
        {
            Напишите метод для поиска значений в JSON.
    * В качестве JSON можно использовать JSON из предыдущего примера. 
    * Метод должен принимать строку-название ключа и возвращать список найденных 
    * значений. Используйте например JsonDocument.Parse

            var res = (new JsonParser()).ParseJson(json, "Temperature");
            foreach (var result in res) 
            {
                Console.WriteLine(result);
            }
        }
        public class JsonParser
        {
            private string? _value;
            private List<string> _results = new List<string>();
            public List<string> ParseJson (string json, string value)
            {
                _value = value;
                var jsonDocument = JsonDocument.Parse(json);
                var root = jsonDocument.RootElement;
                parseElement(root);
                return _results;
            }

            private void parseElement (JsonElement element, bool save=false)
            {

                switch (element.ValueKind)
                {
                    case JsonValueKind.Object:
                        parseObject(element);
                        break;
                    case JsonValueKind.Array:
                        parseArray(element);
                        break;
                    case JsonValueKind.String:
                        parseString(element, save);
                        break;
                    case JsonValueKind.Number:
                        parseNumber(element, save);
                        break;
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        parseBoolean(element);
                        break;
                    case JsonValueKind.Null:
                        parseNull();
                        break;
                    default:
                        throw new NotSupportedException("Unsupported JSON value kind: " + element.ValueKind);
 
                }
            }
            private void parseObject (JsonElement element)
            {
                foreach (var el in element.EnumerateObject())
                {
                    Console.WriteLine($"Property  = {el.Name}");
                    bool save = el.Name ==_value;
                    parseElement(el.Value, save);
                }
            }

            private void parseArray(JsonElement element)
            {
                foreach (var el in element.EnumerateArray())
                {
                    parseElement(el);
                }
            }
            private void parseString(JsonElement element, bool save)
            {
                if (save)
                {
                    _results.Add(element.GetString());
                }
                Console.WriteLine($"String = {element.GetString()}");
            }
            private void parseNumber(JsonElement element, bool save)
            {
                if (save)
                {
                    _results.Add(element.GetRawText());
                }
                Console.WriteLine($"Number = {element.GetRawText()}");
            }
            private void parseBoolean(JsonElement element)
            {
                Console.WriteLine("Boolean value: " + element.GetBoolean());
            }

            private void parseNull()
            {
                Console.WriteLine("Null value");
            }
        }
    }
}
/*
string s = "<?xml version=\"1.0\" encoding=\"utf-8\"?> " +
    "<Data.Root xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  " +
    "<Data.Root.Names>    " +
    "<Name>Name1</Name>   " +
    " <Name>Name2</Name>    " +
    "<Name>Name3</Name>  " +
    "</Data.Root.Names>  " +
    "<Data.Entry LinkedEntry=\"Name1\">    " +
    "<Data.Name>Name2</Data.Name> " +
    " </Data.Entry>  " +
    "<Data_x0023_ExtendedEntry LinkedEntry=\"Name2\">   " +
    " <Data.Name>Name1</Data.Name>   " +
    " <Data_x0023_Extended>NameOne</Data_x0023_Extended>  " +
    "</Data_x0023_ExtendedEntry> </Data.Root>";
var dl = new DataRoot
{
    Names = new List<string> { "Name1", "Name2", "Name3" },
    Entries = new List<DataEntry> {

new DataEntry{ LinkedEntry = "Name1", DataName = "Name2"},
new DataExtendedEntry { LinkedEntry = "Name2", DataName = "Name1", ExtendedEntry = "NameOne"}
}
};
var serializer = new XmlSerializer(typeof(DataRoot));
serializer.Serialize(Console.Out, dl);
using (TextReader streamReader = new StringReader(s))
{
    DataRoot v = serializer.Deserialize(streamReader) as DataRoot;
}
}
}
[XmlRoot("Data.Root")]

public class DataRoot
{
[XmlArray("Data.Root.Names")]
[XmlArrayItem("Name")]
public List<string> Names = new List<string>();

[XmlElement(typeof(DataEntry))]
[XmlElement(typeof(DataExtendedEntry))]
public List<DataEntry> Entries = new List<DataEntry>();

}

[XmlType("Data.Entry")]
public class DataEntry
{
[XmlAttribute]
public string LinkedEntry;
[XmlElement("Data.Name")]
public string DataName;
}

[XmlType("Data#ExtendedEntry")]
public class DataExtendedEntry : DataEntry
{
[XmlElement("Data#Extended")]
public string ExtendedEntry;
}


/*
* С сайта о погоде была получена информация о текущей и прошлой (за три дня) 
* погоде в виде JSON. Напишите класс способный хранить представленную информацию.
* {"Current":{"Time":"2023-06-18T20:35:06.722127+04:00","Temperature":29,
* "Weathercode":1,"Windspeed":2.1,"Winddirection":1},"History":
* [{"Time":"2023-06-17T20:35:06.77707+04:00","Temperature":29,"Weathercode":2,
* "Windspeed":2.4,"Winddirection":1},{"Time":"2023-06-16T20:35:06.777081+04:00",
* "Temperature":22,"Weathercode":2,"Windspeed":2.4,"Winddirection":1},
* {"Time":"2023-06-15T20:35:06.777082+04:00","Temperature":21,"Weathercode":4,
* "Windspeed":2.2,"Winddirection":1}]}
public class WeatherInfo
{
public DateTime Time { get; set; }
public double Temperature { get; set; }
public int WeatherCode { get; set; }

public double WindSpeed { get; set; }

public int Winddirection { get; set; }
}

public class WeatherData
{
public WeatherInfo Current { get; set; }
public List<WeatherInfo> History { get; set; }
}

*/

