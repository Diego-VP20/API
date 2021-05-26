using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiForReal.models
{
    [Table("tbl_Tickets", Schema = "dbo")]
    public class Ticket
    {
        [Key, ReadOnly(true)]
        public int Id { get; set; }
        
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
        [ForeignKey("Worker")]
        public int? WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
        
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        
        public bool IsSolved { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}