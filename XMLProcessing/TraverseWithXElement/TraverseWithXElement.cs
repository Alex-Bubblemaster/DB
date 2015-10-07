namespace TraverseWithXElement
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    public class TraverseWithXElement
    {
        public static void Main()
        {
            var fileLocation = Traverse(Path.GetDirectoryName(@"..\..\"));
            fileLocation.Save(@"..\..\directories.xml");

            Console.WriteLine("Result xml file saved in project folder");

        }
        static XElement Traverse(string dir)
        {
            var element = new XElement("dir", new XAttribute("path", dir));

            foreach (var directory in Directory.GetDirectories(dir))
            {
                element.Add(Traverse(directory));
            }

            foreach (var file in Directory.GetFiles(dir))
            {
                element.Add(new XElement("file",
                    new XAttribute("name", Path.GetFileNameWithoutExtension(file)),
                    new XAttribute("extension", Path.GetExtension(file))));
            }

            return element;
        }
    }
}
