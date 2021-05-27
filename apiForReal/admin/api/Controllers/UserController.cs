using System.Linq;
using Microsoft.AspNetCore.Authorization;
using apiForReal.models;
using Microsoft.AspNet.OData;

namespace apiForReal.admin.api.Controllers
{
    [Authorize(Roles = "test")]
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