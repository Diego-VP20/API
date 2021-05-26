using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiForReal.models
{
    [Table("tbl_Abteilung", Schema = "dbo")]
    public class Department
    {
        [Key, ReadOnly(true)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}