using System;
using System.Collections.Generic;
using System.Xml;

namespace ReadFromFile
{
    public interface IDataProvider
    {
        IEnumerable<TransactionDay> GetData(string id);
    }

    public class XmlParser : IDataProvider
    {
        public string FileRoot { get; private set; }

        public XmlParser()
        {
        }

        public XmlParser(string fileRoot)
        {
            FileRoot = fileRoot;
        }

        public IEnumerable<TransactionDay> GetData(string id)
        {
            var result = new List<TransactionDay>();

            using(var reader = XmlReader.Create(System.IO.Path.Combine(FileRoot, id + ".xml")))
            {
                FindTBody(reader);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "tr")
                    {
                        var date = ReadNextTdAsDate(reader);
                        var high = ReadNextTdAsDecimal(reader);
                        var low = ReadNextTdAsDecimal(reader);
                        var close = ReadNextTdAsDecimal(reader);
                        var average = ReadNextTdAsDecimal(reader);
                        var volume = ReadNextTdAsLong(reader);
                        var value = ReadNextTdAsLong(reader);

                        result.Add(new TransactionDay(date, 0, close, high, low, value, volume));
                    }
                }
            }

            return result;
        }

        private void FindTBody(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "tbody")
                    return;
            }
        }

        private DateTime ReadNextTdAsDate(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "td")
                {
                    return reader.ReadElementContentAsDateTime();
                }
            }
            return DateTime.MinValue;
        }

        private decimal ReadNextTdAsDecimal(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "td")
                {
                    var s = reader.ReadElementContentAsString();
                    decimal value;
                    if (decimal.TryParse(s, out value))
                        return value;
                    return 0;
                }
            }
            return 0;
        }

        private long ReadNextTdAsLong(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "td")
                {
                    var s = reader.ReadElementContentAsString();
                    s = s.Replace(" ", "");
                    long value;
                    if (long.TryParse(s, out value))
                        return value;
                    return 0;
                }
            }
            return 0;
        }
    }
}
