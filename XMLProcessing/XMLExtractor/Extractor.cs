namespace XMLExtractor
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class Extractor
    {
        static void Main()
        {
            var catalogue = XDocument.Load(@"..\..\catalogue.xml");
            var hashtable = new Hashtable();
            var artists = catalogue.Descendants("album").Elements("artist");
            
            int albumsCount = 1;
            foreach (var artist in artists)
            {
                if (hashtable.ContainsKey(artist.Value))
                {
                    int currentAlbumsCount = (int)hashtable[artist.Value];
                    hashtable[artist.Value] = ++currentAlbumsCount;
                }
                else
                {
                    //since keys have to be unique, I put the artists as keys
                    hashtable.Add(artist.Value, albumsCount);
                }
            }

            Console.WriteLine("Here are all the artists extracted through DOM and Hashtable:");
            Console.WriteLine("_________________________");
            Console.WriteLine();

            foreach (DictionaryEntry artist in hashtable)
            {
                Console.WriteLine("Artist : {0} - Albums count: {1}", artist.Key, artist.Value);
            }

            Console.WriteLine();

            string xPathQuery = "/catalogue/album/artist";
            var xArtists = catalogue.XPathSelectElements(xPathQuery);

            var xHashtable = new Hashtable();
            int xAlbumsCount = 1;
            foreach (var xArtist in xArtists)
            {
                if (xHashtable.ContainsKey(xArtist.Value))
                {
                    int currentAlbumsCount = (int)xHashtable[xArtist.Value];
                    xHashtable[xArtist.Value] = ++currentAlbumsCount;
                }
                else
                {
                    //since keys have to be unique, I put the artists as keys
                    xHashtable.Add(xArtist.Value, xAlbumsCount);
                }
            }

            Console.WriteLine("Here are all the artists extracted through XPath and Hashtable:");
            Console.WriteLine("_________________________");
            Console.WriteLine();

            foreach (DictionaryEntry xArtist in xHashtable)
            {
                Console.WriteLine("Artist : {0} - Albums count: {1}", xArtist.Key, xArtist.Value);
            }

            //removing elements
            Console.WriteLine("Albums with price over $20 have been deleted.Here are the rest");
            Console.WriteLine("---------------------------");

            var albumsWithPrice = catalogue.Descendants("album");
            foreach (var item in albumsWithPrice)
            {
                var currentAlbum = item.Element("price").Value;
                double price = XmlConvert.ToDouble(currentAlbum);
                if (price > 20)
                {
                    item.RemoveAll();
                }
                else
                {
                    Console.WriteLine("Artist - {0}, Album: \"{1}\"",item.Element("artist").Value, item.Element("name").Value);
                }

            }
            //Extracting songs titles using XmlReader
            var reader = XmlReader.Create(@"..\..\catalogue.xml");

            using (reader)
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "title"))
                    {
                        Console.WriteLine(reader.ReadElementContentAsString());
                    }
                }
            }

            //extracting songs using Xdocument
            Console.WriteLine();
            Console.WriteLine("Extracting songs using LINQ and XDocument");
            var childNodes = from el in catalogue.Descendants().Elements("title")
                             select el.Value;
                         

            foreach (var child in childNodes)
            {
                    Console.WriteLine(child);
            }
        }
    }
}
