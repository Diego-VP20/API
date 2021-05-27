using System.Linq;
using Microsoft.AspNetCore.Authorization;
using apiForReal.models;
using Microsoft.AspNet.OData;

namespace apiForReal.admin.api.Controllers
{
    [Authorize(Roles = "test")]
    public class WorkerController : ODataController
    {
        private DbTicketsContext dbTicketsContext;

        public WorkerController(DbTicketsContext dbTicketsContext)
        {
            this.dbTicketsContext = dbTicketsContext;
        }
        
        public IQueryable<Worker> Get()
        {
            return dbTicketsContext.Worker.AsQueryable();
        }
    }
}