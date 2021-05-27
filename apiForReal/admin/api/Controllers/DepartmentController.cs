using System.Linq;
using Microsoft.AspNetCore.Authorization;
using apiForReal.models;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;

namespace apiForReal.admin.api.Controllers
{
    [Authorize(Roles = "test")]
    public class DepartmentController : ODataController
    {
        private DbTicketsContext dbTicketsContext;

        public DepartmentController(DbTicketsContext dbTicketsContext)
        {
            this.dbTicketsContext = dbTicketsContext;
        }
        
        public IQueryable<Department> Get()
        {
            return dbTicketsContext.Department.AsQueryable();
        }
    }
}