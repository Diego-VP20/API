using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using apiForReal.models;
using Microsoft.AspNet.OData;
using SingleResult = Microsoft.AspNet.OData.SingleResult;

namespace apiForReal.admin.api.Controllers
{
    [Authorize(Roles = "test")]
    public class CustomerController : ODataController
    {
        private DbTicketsContext dbTicketsContext;

        public CustomerController(DbTicketsContext dbTicketsContext)
        {
            this.dbTicketsContext = dbTicketsContext;
        }
        
        public IQueryable<Customer> Get()
        {
            return dbTicketsContext.Customer.AsQueryable();
        }

        private bool CustomerExists(int key)
        {
            return dbTicketsContext.Customer.Any(c => c.Id == key);
        } 
        
        [EnableQuery]
        public Microsoft.AspNet.OData.SingleResult<Customer> Get([FromODataUri] int key)
        {
            var result = dbTicketsContext.Customer.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }
        
        /* [EnableQuery]
        public Microsoft.AspNet.OData.SingleResult<Customer> GetCustomer([FromODataUri] int key)
        {
            var result = dbTicketsContext.Customer.Where(c => c.Id == key).Select(c => c.Supplier);
            return SingleResult.Create(result);
        } */
        
    }
}