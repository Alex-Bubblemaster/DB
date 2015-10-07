namespace ConsoleApplication1
{
    using System;
    using System.Xml.Linq;

    public class XMLBasics
    {
        static void Main()
        {
            var students = XElement.Load(@"..\..\students.xml");
            Console.WriteLine(students);
        }
    }
}
