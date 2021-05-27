using System;
using System.Linq;
using apiForReal.models;
using Microsoft.AspNet.OData;
using System.Web.Http;

namespace apiForReal.admin.api.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "test")]
    public class TicketController : ODataController
    {
        private DbTicketsContext _dbTicketsContext;

        public TicketController(DbTicketsContext dbTicketsContext)
        {
            this._dbTicketsContext = dbTicketsContext;
        }
        
        public IQueryable<Ticket> Get()
        {
            return _dbTicketsContext.Ticket.AsQueryable();
        }

        [EnableQuery]
        [HttpGet]
        public IQueryable<Object> Get(string search = "", int idToSearch = 1)
        {

            if (search.ToLower() == "department")
                return _dbTicketsContext.Department.Where(e => e.Id == idToSearch).ToList().AsQueryable();
            else if (search.ToLower() == "customer")
                return _dbTicketsContext.Customer.Where(e => e.Id == idToSearch).ToList().AsQueryable();
            else if (search.ToLower() == "worker")
                return _dbTicketsContext.Worker.Where(e => e.Id == idToSearch).ToList().AsQueryable();

            return null;
        }

    }
}