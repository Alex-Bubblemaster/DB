namespace ExcelOperations
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;

    public class Startup
    {
        public static void Main()
        {
            // I needed to install Access Engine to run this on my computer with Excel 2010. Hope it works for you as well
            // 6. Create an Excel file with 2 columns: name and score:
            // Write a program that reads your MS Excel file through the OLE DB data provider and displays the name and score row by row.
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Table.xlsx;"
                                                               + @"Extended Properties='Excel 12.0 Xml;HDR=YES'");

            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT * from [Sheet1$]", connection);
                Console.WriteLine(@"Reading file from ExcelOperations\bin\Debug\Table.xlsx");
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader["Name"];
                        var score = reader["Score"];
                        Console.WriteLine("Name - {0}; Score - {1}", name, score);
                    }
                }

                // 7. Implement appending new rows to the Excel file.
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("Adding a new record....");

                OleDbCommand insertRecord = new OleDbCommand(@"INSERT INTO [Sheet1$](Name, Score)
                                                        VALUES(@name, @score)", connection);
                insertRecord.Parameters.AddWithValue("@name", "Marcheto");
                insertRecord.Parameters.AddWithValue("@score", 10);
                int rowsAffected = (int)insertRecord.ExecuteNonQuery();
                Console.WriteLine("Rows affected: {0}", rowsAffected);
            }
        }
    }
}