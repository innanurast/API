using Amazon.SimpleEmail.Model;
using API.repository;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository loginRepo;
        public AccountController(AccountRepository loginRepo)
        {
            this.loginRepo = loginRepo;
        }

        [HttpPost("login")]
        public virtual ActionResult login(string email, string password)
        {
             if (loginRepo.EmailExist(email)==null) {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "incorret email, please try again!" });
            }
            var login = loginRepo.login(email, password);

            if (login == true)
            {
                return Ok(new { HttpStatusCode.OK, Message = "Login is successfully" });
            }
            return BadRequest(new { HttpStatusCode.BadRequest, massage = "incorret password, please try again!" });
        }

        [HttpPost("forgotPassword")]
        public virtual ActionResult SendEmail(string email)
        {
            var sendEmail = loginRepo.forgotPassword(email);

            if (sendEmail == false)
            {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "invalid Email, please try again!" });
            }
            return Ok(new { HttpStatusCode.OK, massage = "Successfully" });
        }

        [HttpPost("changepassword")]
        public virtual ActionResult changePassword(ChangePasswordVm changePassword)
        {
            //mengecek email ada didatabase atau enggak

            var cekEmail = loginRepo.EmailExist(changePassword.Email);
            if (cekEmail == null)
            {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "invalid Email, please try again!" });
            }
            //cek konfirmasi pass dan pass baru
            else if (changePassword.Password != changePassword.ConfirmPassword) 
            {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "password confirmation does not match" });
            } else if (changePassword.OTP == "")
            {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "OTP code is empty, please try again!" });
            }
            //cek otp, cari nik dari employeee dari email yg dimasukkan

            //mengecek otp Is Used
            else if (loginRepo.IsUsed(changePassword.Email))
            {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "The OTP code has been used" });
            }
            //mengecek expiry time pada OTP
            else if (loginRepo.Expired(changePassword.Email))
            {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "The OTP code has expired" });
            }

            //otp disamakan kalau enggak kasih response error
            var cekOTP = loginRepo.changePassword(changePassword);
            if (cekOTP == false)
            {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "OPT code is incorret!" });
            }
            return Ok(new { HttpStatusCode.OK, Message = "Change password is successfully!"});

            //nik dipakai untuk mencari otp ditabel akun
           
            //endpoint ada 4, email otp, pass baru dan konfirm pass baru
    }
    }

}
