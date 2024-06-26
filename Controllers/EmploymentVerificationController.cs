using EmploymentVerificationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EmploymentVerificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploymentVerificationController : ControllerBase
    {
        private readonly List<Employee> _employees;

        public EmploymentVerificationController(List<Employee> employees)
        {
            _employees = employees;
        }

        [HttpPost("verify-employment")]
        public IActionResult VerifyEmployment([FromBody] EmployeeVerificationRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { error = "Invalid request" });
            }

            var employee = _employees.FirstOrDefault(e =>
                e.EmployeeId == request.EmployeeId &&
                e.CompanyName == request.CompanyName &&
                e.VerificationCode == request.VerificationCode);

            if (employee != null)
            {
                return Ok(new { verified = true });
            }

            return Ok(new { verified = false });
        }
    }
}
