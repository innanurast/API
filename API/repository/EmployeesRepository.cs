using API.Model;
using API.Repository.Interface;
using API.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using System.Drawing;

namespace API.repository
{
    public class EmployeesRepository : IEmployeeRepository
    {
        private readonly MyContext context;

        public EmployeesRepository(MyContext context)
        {
            this.context = context; //this.context sama seperti perintah _context
        }

        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);  // untuk menghapus nik yang sudah ditemukan
            var result = context.SaveChanges(); //kemudian perubahan tersebut akan disimpan
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            return entity;
        }

        public int Insert(Employee employee)
        {
            string uniqueNIK = generateNIK();
            employee.NIK = uniqueNIK;
            context.Employees.Add(employee);    //menambah data ke tabel employee sesuai dengan NIK
            var result = context.SaveChanges(); // digunakan untuk menyimpan data hasil kode otomatis NIK
            return result;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }

        public string generateNIK()
        {
            string date = DateTime.Now.ToString("ddMMyy"); //format tgl sekarang
            string uniqueNIK = "";

            var cekLastData = context.Employees.OrderBy(data => data.NIK).LastOrDefault(); //Cek data terakhir
            if (cekLastData == null) // data null adalah nilai pada suatu kolom yang berarti tidak mempunyai nilai.
            {
                uniqueNIK = date + "001";
            }
            else
            {
                var NIKLastData = cekLastData.NIK;            //dari cek data terakhir nik maka akan di
                string lastThree = NIKLastData.Substring(NIKLastData.Length - 3);       //substring menspesifikan karakter

                int kode = int.Parse(lastThree) + 1;        //increement
                uniqueNIK = date + kode.ToString("000"); // membuat kode unik nik dengan format tgl bulan tahun kemudian tambah dengan urutan
                                                         //  mengembalikan nilai string yang merupakan representasi obyek
            }
            return uniqueNIK;
        }

        public bool CheckPhoneDuplicate(string phone)
        {
            var duplicate = context.Employees.AsNoTracking().FirstOrDefault(e => e.Phone == phone);
            if (duplicate != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmailDuplicate(string email)
        {
            var duplicate = context.Employees.AsNoTracking().FirstOrDefault(e => e.Email == email); //mengambil data pertama  sesuai dengan data yang diberikan
            if (duplicate != null) //single memastikan data nya itu satu aja
            {
                return true;
            }
            return false;
        }


        public bool CheckNIKExist(string NIK)
        {
            var checkNIK = context.Employees.AsNoTracking().FirstOrDefault(e => e.NIK == NIK);
            if (checkNIK == null)
            {
                return false;
            }
            return true;
        }

        public int Register(viewModel register)
        {

            string NIKnew = generateNIK(); //manggil generate nik

            //data employee [] //
            Employee emp = new Employee();
            emp.NIK = NIKnew;
            emp.FisrtName = register.FirstName;
            emp.LastName = register.LastName;
            emp.Phone = register.Phone;
            emp.BirthDate = register.BirthDate;
            emp.Email = register.Email;
            emp.salary = register.salary;
            emp.Email = register.Email;
            emp.Gender = register.Gender;
            context.Employees.Add(emp);
            var result_emp = context.SaveChanges(); // digunakan untuk menyimpan data hasil kode otomatis NIK

            //data account [] //
            Account acc = new Account();
            acc.NIK = NIKnew;
            acc.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(register.Password, 12);
            context.Add(acc);
            var result_acc = context.SaveChanges();


            // data education //
            Education edu = new Education();
            edu.Degree = register.Degree;
            edu.GPA = register.GPA;
            edu.University_id = register.university_id;
            context.Add(edu);
            var result_edu = context.SaveChanges();

            // data relasi profilling dengan education menggunakan id education //
            Profilling prof = new Profilling();
            prof.Education_id = edu.Id;
            prof.NIK = emp.NIK;
            context.Add(prof);
            var result_prof = context.SaveChanges();
            return result_prof;
        }

        public GetEmployeeVm getEmploy(string NIK)
        {
            var data = context.Employees.Join(context.Accounts,
                emp => emp.NIK,
                acc => acc.NIK,
                (emp, acc) => new { emp, acc }
                )
                .Join(context.profillings,
                e_acc => e_acc.acc.NIK,
                Prof => Prof.NIK,
                (e_acc, prof) => new { e_acc, prof }
                )
                .Join(context.Educations,
                p_edu => p_edu.prof.Education_id,
                edu => edu.Id,
                (p_edu, edu) => new { p_edu, edu }
                ).
                Join(context.universities,
                e_univ => e_univ.edu.University_id,
                univ => univ.Id,
                (e_univ, univ) => new
                {
                    NIK = e_univ.p_edu.e_acc.emp.NIK,
                    FullName = e_univ.p_edu.e_acc.emp.FisrtName + " " + e_univ.p_edu.e_acc.emp.LastName,
                    Phone = e_univ.p_edu.e_acc.emp.Phone,
                    BirthDate = e_univ.p_edu.e_acc.emp.BirthDate,
                    salary = e_univ.p_edu.e_acc.emp.salary,
                    Email = e_univ.p_edu.e_acc.emp.Email,
                    Gender = e_univ.p_edu.e_acc.emp.Gender,
                    Degree = e_univ.edu.Degree,
                    GPA = e_univ.edu.GPA,
                    university_name = univ.Name
                })
                .Where(e => e.NIK == NIK)
                .Select(r => new GetEmployeeVm
                {
                    FullName = r.FullName,
                    Phone = r.Phone,
                    BirthDate = r.BirthDate,
                    salary = r.salary,
                    Email = r.Email,
                    Gender = r.Gender,
                    Degree = r.Degree,
                    GPA = r.GPA,
                    university_name = r.university_name
                }).FirstOrDefault();
            return data;
        }

        public IEnumerable<GetEmployeeVm> GetAll()
        {
            var data = context.Employees.Join(context.Accounts,
                emp => emp.NIK,
                acc => acc.NIK,
                (emp, acc) => new { emp, acc }
                )
                .Join(context.profillings,
                e_acc => e_acc.acc.NIK,
                Prof => Prof.NIK,
                (e_acc, prof) => new { e_acc, prof }
                )
                .Join(context.Educations,
                p_edu => p_edu.prof.Education_id,
                edu => edu.Id,
                (p_edu, edu) => new { p_edu, edu }
                ).
                Join(context.universities,
                e_univ => e_univ.edu.University_id,
                univ => univ.Id,
                (e_univ, univ) => new GetEmployeeVm
                {
                    FullName = e_univ.p_edu.e_acc.emp.FisrtName + " " + e_univ.p_edu.e_acc.emp.LastName,
                    Phone = e_univ.p_edu.e_acc.emp.Phone,
                    BirthDate = e_univ.p_edu.e_acc.emp.BirthDate,
                    salary = e_univ.p_edu.e_acc.emp.salary,
                    Email = e_univ.p_edu.e_acc.emp.Email,
                    Gender = e_univ.p_edu.e_acc.emp.Gender,
                    Degree = e_univ.edu.Degree,
                    GPA = e_univ.edu.GPA,
                    university_name = univ.Name
                }).ToList();

            return data;
        }
    }
}
