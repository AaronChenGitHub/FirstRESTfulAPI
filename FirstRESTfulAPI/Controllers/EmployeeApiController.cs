using FirstRESTfulAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly EmployeesContext _employeesContext;
        public EmployeeApiController(EmployeesContext employeesContext) 
        { 
            _employeesContext = employeesContext;
        }

        [HttpGet]
        [Route("GetAll/rawdata")]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return _employeesContext.Employees.ToList();
        }


        [HttpGet]
        [Route("GetOnce/{id}/rawdata")]
        public ActionResult<IEnumerable<Employee>> Get([FromRoute(Name ="id")]int id)
        {
            return _employeesContext.Employees.Where(x=>x.EmployeeId == id).ToList();
        }


        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
