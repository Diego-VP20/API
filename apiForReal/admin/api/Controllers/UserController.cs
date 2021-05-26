using System.Linq;
using apiForReal.models;
using Microsoft.AspNet.OData;

namespace apiForReal.admin.api.Controllers
{
    public class UserController : ODataController
    {
        private DbTicketsContext dbTicketsContext;

        public UserController(DbTicketsContext dbTicketsContext)
        {
            this.dbTicketsContext = dbTicketsContext;
        }
        
        public IQueryable<User> Get()
        {
            return dbTicketsContext.User.AsQueryable();
        }
    }
}