namespace EFModelling
{
    using System.Data.Linq;

    public partial class Employee
    {
        // 8. By inheriting the Employee entity class create a class which allows employees
        // to access their corresponding territories as property of type EntitySet<T>.
        public EntitySet<Territory> TerritoriesSet
        {
            get
            {
                EntitySet<Territory> territoriesSet = new EntitySet<Territory>();
                territoriesSet.AddRange(this.Territories);
                return territoriesSet;
            }
        }
        
    }
}
