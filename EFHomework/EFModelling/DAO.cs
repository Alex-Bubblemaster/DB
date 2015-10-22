namespace EFModelling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DAO 
    {
        public static void InsertCustomer(Customer customer, NorthwindEntities db)
        {           
                 db.Customers.Add(customer);
                 db.SaveChanges();             
        }

        public static void DeleteCustomer(string id, NorthwindEntities db)
        {
            var customer = db.Customers.Find(id.Trim());
            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        public static void ModifyCustomer(string id, string companyName, NorthwindEntities db)
        {
                 var customer = db.Customers.Find(id.Trim());
                 customer.CompanyName = companyName;
                 db.SaveChanges();
        }
    }
}