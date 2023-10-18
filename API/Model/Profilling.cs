using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Profilling
    {
        [Key]
        public String NIK { get; set; }
        public int Education_id { get; set; }
        

        public Account Accounts { get; set; }

        public Education Educations { get; set; } 
    }
}
