namespace AlbumXMLReaderWriter
{
    using System;
    using System.Text;
    using System.Xml;
    public class AlbumXMLCreator
    {
        public static void Main()
        {
            var path = @"..\..\..\XMLExtractor\bin\Debug\catalogue.xml";
            var reader = XmlReader.Create(path);
            string filename = @"..\..\albums.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            var writer = new XmlTextWriter(filename, encoding);
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = '\t';
            writer.Indentation = 1;

            writer.WriteStartDocument();
            writer.WriteStartElement("album");

            using (writer)
            {
                using (reader)
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "name")
                            {
                                var albumName = reader.ReadElementContentAsString();
                                writer.WriteElementString("name", albumName);

                            }
                            if (reader.Name == "artist")
                            {
                                var artistName = reader.ReadElementContentAsString();
                                writer.WriteElementString("artist", artistName);
                            }
                            if (reader.Name == "price")
                            {
                                var albumPrice = reader.ReadElementContentAsString();
                                writer.WriteElementString("price", albumPrice);
                            }
                        }
                    }
                }
                writer.WriteEndDocument();
                Console.WriteLine("Document albums.xls created and can be found in the project folder");
            }
        }
    }
}
