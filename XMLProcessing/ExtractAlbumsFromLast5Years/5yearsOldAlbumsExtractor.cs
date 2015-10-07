namespace ExtractAlbumsFromLast5Years
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class Program
    {
        public static void Main()
        {
            // Load the document and set the root element.
            var catalogue = XDocument.Load(@"..\..\..\XMLExtractor\bin\Debug\catalogue.xml");
            string xPathQuery = "/catalogue/album[year<=2010]/name";

            var albums = catalogue.XPathSelectElements(xPathQuery);
            Console.WriteLine("Albums published 5 years ago or earlier with XPath");
            Console.WriteLine();
            foreach (var album in albums)
            {
                Console.WriteLine("\"{0}\"", album.Value);
            }

            Console.WriteLine();
            Console.WriteLine("Albums published 5 years ago or earlier with Linq");
            Console.WriteLine();

            var albumNames = from album in catalogue.Descendants("album")
                             where int.Parse(album.Element("year").Value) <= 2010
                             select album.Element("name").Value;
            Console.WriteLine(string.Join(",",albumNames));
        }
    }
}
