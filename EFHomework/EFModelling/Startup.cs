namespace EFModelling
{
    using System;
    using System.Data.Linq;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {

            using (var db = new NorthwindEntities())
            {
                // 2. Create a DAO class with static methods which provide functionality for inserting, modifying and deleting customers.
                Customer customer = new Customer();
                customer.CompanyName = "GhostBustersLTD";
                customer.CustomerID = "202";
                DAO.InsertCustomer(customer, db);

                Customer anotherCustomer = new Customer();
                anotherCustomer.CompanyName = "GhostBustersLTD";
                anotherCustomer.CustomerID = "203";
                DAO.InsertCustomer(anotherCustomer, db);

                DAO.DeleteCustomer("202", db);

                DAO.ModifyCustomer("203", "Alcoholics Anonymous", db);

                // 3. Write a method that finds all customers who have orders made in 1997 and shipped to Canada.
                var salesToCanada1997 = SalesOperations.FindOrdersByYearAndShipCountry(1997, "Canada", db);
                foreach (var sales in salesToCanada1997)
                {
                    Console.WriteLine(sales.ContactName);
                    Console.WriteLine(sales.Country);
                }

                // 4. Implement previous by using native SQL query and executing it through the DbContext.
                SalesOperations.FindOrdersByYearAndShipCountryNativeSQL(1997, "Canada", db);

                // 5. Write a method that finds all the sales by specified region and period (start / end dates).
                SalesOperations.FindSalesByRegionByPeriod(new DateTime(1996, 01, 01), new DateTime(1997, 12, 31), "BC", db);

                 8. By inheriting the Employee entity class create a class which allows employees to access their corresponding territories as property of type EntitySet<T>.
                var testEmployee = db.Employees.FirstOrDefault();
                EntitySet<Territory> territories = testEmployee.TerritoriesSet;
                Console.WriteLine("Territories for employee {0} are:", testEmployee.FirstName);
                Console.WriteLine("-------------------------------------------");
                foreach (var territory in territories)
                {
                    Console.WriteLine(territory.TerritoryDescription);
                }
            }
        }
    }
}
