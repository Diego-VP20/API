using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiForReal.models
{
    [Table("tbl_Kunden", Schema = "dbo")]
    public class Customer
    {
        [Key, ReadOnly(true)]
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string TlfNr { get; set; }
    }
}