using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public Degree Degree { get; set; }
        public string GPA { get; set; }

        public int University_id { get; set; }  //enggak perlu pakai ini karena 

        public ICollection<Profilling> profillings { get; set; }

        public University universities { get; set; } // pada university (nama model) (nama objek) pk sudah berelasi
    }

    public enum Degree
    {
        D3,
        D4,
        S1,
        S2,
        S3
    }
}
