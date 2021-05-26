using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiForReal.models
{
    [Table("tbl_Users", Schema = "dbo")]
    public class User
    {
        [Key, ReadOnly(true)]
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}