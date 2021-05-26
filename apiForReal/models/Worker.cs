using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiForReal.models
{
    [Table("tbl_Mitarbeiter", Schema = "dbo")]
    public class Worker
    {
        [Key, ReadOnly(true)]
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
    }
}