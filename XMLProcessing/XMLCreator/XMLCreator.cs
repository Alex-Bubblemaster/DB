namespace XMLCreator
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    public class XMLCreator
    {
        static void Main()
        {
            string[] peopleInfo = File.ReadAllLines("../../PeopleInfo.txt");
            XElement root = new XElement("person");

            for (int i = 0; i < peopleInfo.Length; i++)
            {
                if (i % 3 == 0)
                {
                    root.Add(new XElement("name", peopleInfo[i]));
                }
                if (i % 3 == 1)
                {
                    root.Add(new XElement("address", peopleInfo[i]));
                }
                if (i % 3 == 2)
                {
                    root.Add(new XElement("phone", peopleInfo[i]));
                }
            }
            root.Save("../../XMLPeople.xml");
            Console.WriteLine("The file has been saved in the XMLCreator folder");
        }

    }
}
