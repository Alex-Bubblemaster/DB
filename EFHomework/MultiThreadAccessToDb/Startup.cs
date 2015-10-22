namespace MultiThreadAccessToDb
{
    using System;
    using System.Linq;
    using System.Threading;
    using EFModelling;

    public class Startup
    {
        public static void Main()
        {
            // Try to open two different data contexts and perform concurrent changes on the same records.
            //What will happen at SaveChanges()?
            //How to deal with it?

            Console.WriteLine("First Connection engaged...");
            Thread.Sleep(1000);
            using (var firstDb = new NorthwindEntities())
            {
                var custDemographics = firstDb.CustomerDemographics.Count();
                Console.WriteLine("Customer demographics count from first connection: {0}", custDemographics);
                Thread.Sleep(1000);

                var demographicInfo = new CustomerDemographic();
                demographicInfo.CustomerDesc = "Family";
                demographicInfo.CustomerTypeID = "404";
                firstDb.CustomerDemographics.Add(demographicInfo);
                Thread.Sleep(1000);

                Console.WriteLine("Second Connection engaged...");
                using (var secondDb = new NorthwindEntities())
                {
                    var secondCustDemographics = secondDb.CustomerDemographics.Count();
                    Console.WriteLine("Customer demographics count from second connection: {0}", custDemographics);
                    Thread.Sleep(1000);

                    var secondDemographicInfo = new CustomerDemographic();
                    secondDemographicInfo.CustomerDesc = "Single";
                    secondDemographicInfo.CustomerTypeID = "304";
                    secondDb.CustomerDemographics.Add(secondDemographicInfo);
                    Thread.Sleep(1000);

                    var custDemographicsUpdated = secondDb.CustomerDemographics.Count();
                    Console.WriteLine("First connection still opened, changes unsaved. Customer demographics count from second connection: {0}", custDemographicsUpdated);

                    firstDb.SaveChanges();
                    secondDb.SaveChanges();

                    Console.WriteLine("Closing second connection after saving");
                    Thread.Sleep(1000);
                }
                var finalCustomerDemographicsCount = firstDb.CustomerDemographics.Count();
                Console.WriteLine("First connection still opened,  all changes saved. Customer demographics count from second connection: {0}", finalCustomerDemographicsCount);

                Console.WriteLine("Closing first connection after saving");
            }
        }
    }
}
