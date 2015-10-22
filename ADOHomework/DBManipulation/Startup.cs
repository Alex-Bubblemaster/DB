namespace DBManipulation
{
    using System;
    using System.Data;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.IO;

    public class Startup
    {
        public static void Main()
        {
            // 1. Write a program that retrieves from the Northwind sample database in MS SQL Server the number of rows in the Categories table.
            var connection = new SqlConnection("Server=.;Database=Northwind;Integrated Security=true");
            connection.Open();
            SqlCommand countCategories = new SqlCommand("SELECT COUNT(*) FROM Categories", connection);
            object numberOfCategories = countCategories.ExecuteScalar();
            Console.WriteLine("Categories in Northwind: {0}", (int)numberOfCategories);
            Console.WriteLine("------------------------------------------------------");
            // 2. Write a program that retrieves the name and description of all categories in the Northwind DB.

            SqlCommand requestCategoriesInfo = new SqlCommand("SELECT CategoryName, Description FROM Categories", connection);
            SqlDataReader categoriesReader = requestCategoriesInfo.ExecuteReader();
            while (categoriesReader.Read())
            {
                Console.WriteLine("Category Name: {0} - Category Description: {1}", (string)categoriesReader["CategoryName"], (string)categoriesReader["Description"]);
            }
            categoriesReader.Close();
            // 3. Write a program that retrieves from the Northwind database all product categories and the names of the products in each category.
           // Can you do this with a single SQL query (with table join)?

            SqlCommand requestProductInfo  = new SqlCommand(@"SELECT p.ProductName, c.CategoryName 
                                                            FROM Products p JOIN Categories c ON p.CategoryID = c.CategoryID
                                                            GROUP BY c.CategoryName, p.ProductName", connection);

            SqlDataReader productReader = requestProductInfo.ExecuteReader();
            Console.WriteLine("------------------------------------------------------");

            while (productReader.Read())
            {
                Console.WriteLine("Category Name: {0} - Product Name: {1}", (string)productReader[0], (string)productReader[1]);
            }

            productReader.Close();

            // 4. Write a method that adds a new product in the products table in the Northwind database.
            // Use a parameterized SQL command.
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Adding a new product....");
            SqlCommand insertProduct = new SqlCommand(@"INSERT INTO Products(ProductName, Discontinued)
                                                        VALUES(@name, @discontinued)", connection);
            insertProduct.Parameters.AddWithValue("@name", "Randoms");
            insertProduct.Parameters.AddWithValue("@discontinued", 0);

            int rowsAffected = (int)insertProduct.ExecuteNonQuery();            
            Console.WriteLine("Rows affected: {0}", rowsAffected);

            // 5. Write a program that retrieves the images for all categories in the Northwind database and stores them as JPG files in the file system.

            SqlCommand getPhotos = new SqlCommand(@"SELECT Picture, CategoryName FROM Categories", connection);
            SqlDataReader photoReader = getPhotos.ExecuteReader();
            Console.Write("Exporting pictures in folder DBManipulation\\Photos");
            while (photoReader.Read())
            {
                string categoryName = ((string)photoReader["CategoryName"]).Replace('/', '_');
                string fileName = string.Format(@"..\..\Photos\{0}.jpg", categoryName);
                FileStream stream = File.OpenWrite(fileName);
                using (stream)
                {
                    Console.Write('.');
                    byte[] fileContent = (byte[])photoReader["Picture"];
                    stream.Write(fileContent, 78, fileContent.Length - 78);
                }
            }
            photoReader.Close();
            Console.WriteLine();

            // 8. Write a program that reads a string from the console and finds all products that contain this string.
            // Ensure you handle correctly characters like ', %, ", \ and _.

            Console.WriteLine("Enter product name to check NorthwindDB for it and press enter");

            string productName = Console.ReadLine();
            productName = EscapeSpecialStrings(productName); // try anton's in the console

            SqlCommand checkProduct = new SqlCommand(@"SELECT ProductName
                                                       FROM Products 
                                                       WHERE ProductName LIKE '%" + productName + "%'", connection);
            
            SqlDataReader searchReader = checkProduct.ExecuteReader();

            while (searchReader.Read())
            {
                Console.WriteLine("{0} found", searchReader["ProductName"]);
            }
        }

        private static string EscapeSpecialStrings(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\'')
                {
                    input = input.Substring(0, i) + "'" + input.Substring(i, input.Length - i);
                    i++;
                }

                if (input[i] == '_')
                {
                    input = input.Substring(0, i) + "/" + input.Substring(i, input.Length - i);
                    i++;
                }

                if (input[i] == '%')
                {
                    input = input.Substring(0, i) + "\\" + input.Substring(i, input.Length - i);
                    i++;
                }

                if (input[i] == '&')
                {
                    input = input.Substring(0, i) + "\\" + input.Substring(i, input.Length - i);
                    i++;
                }
            }
            return input;
        }
    }
}
