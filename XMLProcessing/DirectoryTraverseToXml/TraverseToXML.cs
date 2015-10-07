namespace DirectoryTraverseToXml
{
    using System.IO;
    using System.Text;
    using System.Xml;

    public class TraverseToXML
    {
        public static void Main()
        {
            string[] subFiles = Directory.GetFiles(@"..\..\");
            string[] subDirectories = Directory.GetDirectories(@"..\..\");

            string filename = @"..\..\directories.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            var writer = new XmlTextWriter(filename, encoding);
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = '\t';
            writer.Indentation = 1;


            using (writer)
            {
                writer.WriteStartElement("currentDirectory");
                writer.WriteElementString("name", "DirectoryTraverseToXml");
                writer.WriteEndElement();

                foreach (var dir in subDirectories)
                {
                    writer.WriteStartElement("directory");
                    writer.WriteAttributeString("path", dir);
                    writer.WriteEndElement();
                    foreach (var file in subFiles)
                    {
                        writer.WriteStartElement("file");
                        writer.WriteAttributeString("name", Path.GetFileNameWithoutExtension(file));
                        writer.WriteAttributeString("extension", Path.GetExtension(file));
                        writer.WriteEndElement();
                    }
                }
            }
        }   
    }
}
