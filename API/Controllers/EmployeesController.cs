using Amazon.SimpleEmail.Model;
using API.repository;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesRepository repository;
        public EmployeesController(EmployeesRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var get1 = repository.Get();
            if (get1 == null)
            {
                return StatusCode(404, new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data tidak ditemukan",
                    Data = get1
                });
            }
            return StatusCode(200, new
            {
                status = HttpStatusCode.OK,
                Message = "Data ditemukan",
                Data = get1
            });
        }

        [HttpGet("{NIK}")]
        public virtual ActionResult Get(string NIK)
        {
            var get2 = repository.Get(NIK);
            if (get2 == null)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data tidak ditemukan",
                    Data = get2
                });
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "Data Ditemukan",
                Data = get2
            });
        }

        [HttpPost]
        public virtual ActionResult Insert(Employee employee)
        {
            var nik = repository.Get(employee.NIK);
            if (nik != null)
            {
                return BadRequest("Data tersedia");
            }

            else if (repository.CheckEmailDuplicate(employee.Email) == true)
            {
                return BadRequest("Data email tidak boleh sama.");
            }
            else if (repository.CheckPhoneDuplicate(employee.Phone) == true)
            {
                return BadRequest("Phone tidak boleh sama.");
            }
            else
            {
                var result = repository.Insert(employee);
                return Ok(new
                {
                    StatusCode = HttpStatusCode.OK,
                    message = "Data Berhasil Ditambahkan",
                    data = result
                });
            }
        }

        [HttpPut]
        public virtual ActionResult Update(Employee employee)
        {
            if (repository.CheckNIKExist(employee.NIK) == false)
            {
                return NotFound("NIK tidak ditemukan.");
            }
            else if (repository.CheckEmailDuplicate(employee.Email) == true)
            {
                return BadRequest("Data Email tidak boleh sama.");
            }
            else if (repository.CheckPhoneDuplicate(employee.Phone) == true)
            {
                return BadRequest("Phone tidak boleh sama.");
            }

            var update = repository.Get(employee.NIK);
            if (update == null)
            {
                return NotFound(new
                {
                    StatusCode = HttpStatusCode.NotFound,
                    message = "Data Tidak Ditemukan!",
                    data = update
                });
            }
            return Ok(new
            {
                StatusCode = HttpStatusCode.OK,
                message = "Data Ditemukan!",
                data = update
            });
        }

        [HttpDelete("{NIK}")]
        public virtual ActionResult Delete(string NIK)
        {
            if (repository.CheckNIKExist(NIK) == false)
            {
                return NotFound("NIK tidak ditemukan.");
            }

            repository.Delete(NIK);
            return Ok(new {
                StatusCode = HttpStatusCode.OK,
                massage = "Data berhasil dihapus.",
            });
        }


        [HttpPost("register")]
        public virtual ActionResult Register(viewModel register)
        {
            if (repository.CheckEmailDuplicate(register.Email) == true)
            {
                return BadRequest(new {HttpStatusCode.BadRequest, Message = "Email sudah digunakan" });
            }
            else if (repository.CheckPhoneDuplicate(register.Phone) == true)
            {
                return BadRequest(new { HttpStatusCode.BadRequest, Message = "Phone tidak boleh sama." });
            }
            else
            {
                var result = repository.Register(register);
                return Ok(new
                {
                    StatusCode = HttpStatusCode.OK,
                    message = "Data Berhasil Ditambahkan",
                    data = result
                });
            }
        }

        [HttpGet("GetEmp/{NIK}")]
        public virtual ActionResult GetEmp(string NIK)
        {
            var get2 = repository.getEmploy(NIK);
            if (get2 == null)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data tidak ditemukan",
                    Data = get2
                });
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "Data Ditemukan",
                Data = get2
            });
        }

        [HttpGet("GetAllEmp")]
        public ActionResult GetAllEmp()
        {
            var get1 = repository.GetAll();
            if (get1 == null)
            {
                return StatusCode(404, new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data tidak ditemukan",
                    Data = get1
                });
            }
            return StatusCode(200, new
            {
                status = HttpStatusCode.OK,
                Message = "Data ditemukan",
                Data = get1
            });
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS berhasil");
        }
    }
}
