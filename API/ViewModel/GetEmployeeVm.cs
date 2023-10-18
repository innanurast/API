using API.Model;

namespace API.ViewModel
{
    public class GetEmployeeVm
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }

        public Degree Degree { get; set; }
        public string GPA { get; set; }

        public string university_name { get; set; }
    }
}

