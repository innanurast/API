using API.repository.Interface;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using static System.Net.WebRequestMethods;
using API.ViewModel;
using Amazon.CloudTrail.Model;

namespace API.repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext context;

        public AccountRepository(MyContext context)
        {
            this.context = context;    //this.context sama seperti perintah _context
        }

        public bool changePassword(ChangePasswordVm changePassword)
        {
            //mengecek email ada didatabase atau enggak
            var cekEmail = context.Employees.First(emp => emp.Email == changePassword.Email);
            var cekAccount = context.Accounts.Find(cekEmail.NIK); // mengecek akun berdasarkan NIK dari email
            
            if(cekAccount.otp != changePassword.OTP) //akun nya nyimpan objek akun ada nik, password, otp, expired, isused. 
            {
                return false;                     //disini kita mengecek otp dari yang dimasukkan oleh user dengan otp yang tersimpan di database
            }

            //cek konfirmasi pass dan pass baru
            //cek otp, cari nik dari employeee dari email yg dimasukkan
            //nik dipakai untuk mencari otp ditabel akun
            //otp disamakan kalau enggak kasih response error
            //endpoint ada 4, email otp, pass baru dan konfirm pass baru

            cekAccount.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(changePassword.Password, 12); //menimpa dari password baru trus dihashing
            cekAccount.isUsed = true;     //untuk memberikan statement bahwa otp tersebut sudah digunakan
            context.SaveChanges();

            return true;
        }

        public bool forgotPassword(string email)
        {
            var chekEmail = context.Employees.AsNoTracking().FirstOrDefault(emp => emp.Email == email);

            if (chekEmail == null) 
            {
                return false;
            }
            
            var generateOTP = sendOTP();
            var saveOTP = context.Accounts.Find(chekEmail.NIK); //menyimpan OTP berdasarkan NIK
            saveOTP.isUsed = false;   //

            saveOTP.otp = generateOTP;  //save otp yang belum digunakan

            DateTime expiryTime = DateTime.Now.AddMinutes(3); // buat waktu expired otp nya
            saveOTP.Expired = expiryTime;
            
            context.SaveChanges(); //menyimpan data otp ke database yang sebelumnya null pada tabel account 

            sendEmail(email, generateOTP); //untuk mengirim otp melalui email
            return true;
        }

        public bool login(string email, string password)
        {
            var chekEmail = context.Employees.FirstOrDefault(emp => emp.Email == email);
            var checkPassword = context.Accounts.Single(acc => acc.NIK == chekEmail.NIK);   //hanya mencari NIK yang diinputkan

            bool isValid = BCrypt.Net.BCrypt.EnhancedVerify(password, checkPassword.Password);   //mengecek password yang dimasukkan 
            if (isValid)
            {
                return true;
            }
 
            return false;
        }

        public void sendEmail(string Email, string OTP)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Sender Name", "innanur16@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", Email));

            email.Subject = "send OTP Code";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain) //
            {
                Text = $"Your OTP code is: {OTP}"
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 465, true); //menghubungkan host bagian gmail

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("innanur16@gmail.com", "paxz glgr ovoe edxa"); // username dan password bagian mailtrap.io

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        public string sendOTP()
        {
            string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Employee EmailExist(string email)
        {
            //firstordefault mengembalikan null kalau first mengembalikan satu data
            var cekEmail = context.Employees.FirstOrDefault(e => e.Email == email); //mengambil data pertama  sesuai dengan data yang diberikan
            return cekEmail;
        }

        public bool IsUsed (string email)
        {
            //mengecek email
            var cekemail = EmailExist(email);
            var isused = context.Accounts.SingleOrDefault(Account => Account.NIK == cekemail.NIK).isUsed;
            return (bool) isused;
        }

        public bool Expired(string email)
        {
            //mengecek email
            var cekemail = context.Employees.FirstOrDefault(emp => emp.Email == email);
            var expired = context.Accounts.SingleOrDefault(Account => Account.NIK == cekemail.NIK).Expired;

            if (DateTime.Now > expired)  //membandingkan waktu sekarang dengan waktu expired OTP
            {
                return true;
            }
            return false;
        }
    }

            
}
