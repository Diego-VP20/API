using System.Linq;
using apiForReal.models;
using Microsoft.AspNet.OData;

namespace apiForReal.admin.api.Controllers
{
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