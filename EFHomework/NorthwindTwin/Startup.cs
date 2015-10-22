namespace NorthwindTwin
{
    using System;
    using EFModelling;

    public class Startup
    {
        public static void Main()
        {
            // 6. Create a database called NorthwindTwin with the same structure as Northwind using the features from DbContext.
            // Have to add connection string in app.config with initialcatalog = clone name
            using (var db = new NorthwindEntities())
            {
                var result = db.Database.CreateIfNotExists();
                Console.WriteLine(result);
            }
        }
    }
}
