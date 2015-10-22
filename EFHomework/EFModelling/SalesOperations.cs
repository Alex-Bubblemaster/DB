namespace EFModelling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SalesOperations
    {
        public static List<Customer> FindOrdersByYearAndShipCountry(int year, string country, NorthwindEntities db)
        {
            var ordersByYearAndCountry = db.Customers
                                            .Where(c => c.Orders
                                                .Any(o => o.OrderDate != null && 
                                                    (o.OrderDate.Value.Year == year && o.ShipCountry == country)))
                                            .ToList(); 
                                        
            return ordersByYearAndCountry;
        }

        public static void FindOrdersByYearAndShipCountryNativeSQL(int year, string country, NorthwindEntities db)
        {
            string nativeSqlQuery = @"SELECT DISTINCT c.ContactName
                                         FROM Orders o JOIN 
                                         Customers c ON c.CustomerID = o.CustomerID
                                         WHERE (o.ShipCountry ='{0}') AND ({1} = DATEPART(year, o.OrderDate))";

            object[] parameters = { country, year };
            var customers = db.Database.SqlQuery<string>(string.Format(nativeSqlQuery, parameters)).ToList();
            Console.WriteLine("Orders to --------> {0} ", country);
            Console.WriteLine("Customers:");
            foreach (var cust in customers)
            {
                Console.WriteLine(cust);
            }
        }

        public static void FindSalesByRegionByPeriod(DateTime startDate, DateTime endDate, string region, NorthwindEntities db)
        {
            var salesByRegionAndDate = db.Orders
                                             .Where(o => o.ShipRegion == region)
                                             .Where(o => o.ShippedDate < endDate)
                                             .Where(o => o.ShippedDate > startDate)
                                             .Select(c => new
                                             {
                                                 c.ShippedDate,
                                                 c.ShipRegion
                                             }).ToList();

            foreach (var cust in salesByRegionAndDate)
            {
                Console.WriteLine("Shipped Date: {0}", cust.ShippedDate);
                Console.WriteLine("Customer Region: {0}", cust.ShipRegion);
                Console.WriteLine("------------------------------------");
            }                               
        }
    }
}
