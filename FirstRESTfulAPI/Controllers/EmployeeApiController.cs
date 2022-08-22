using FirstRESTfulAPI.Interface;
using FirstRESTfulAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        
        private readonly IEmoloyee _employeeServices;
        public EmployeeApiController(IEmoloyee employeeServices) 
        { 
            _employeeServices = employeeServices;
        }

        [HttpGet]
        [Route("GetAll/rawdata")]
        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            return _employeeServices.GetAll();
        }


        [HttpGet]
        [Route("GetOnce/{id}/rawdata")]
        public ActionResult<IEnumerable<Employee>> Get([FromRoute(Name ="id")]int id)
        {
            return _employeeServices.Get(id);
        }


        [HttpPost]
        [Route("Add/rawdata")]
        public Message AddEmployee([FromBody] Employee employee)
        {
            return _employeeServices.AddEmployee(employee);
        }


        [HttpPut]
        [Route("update/rawdata")]
        public Message UpdateEmployee([FromBody]Employee employee)
        {
           return _employeeServices.UpdateEmployee(employee);
        }


        [HttpDelete]
        [Route("delete/rawdata")]
        public Message Delete([FromQuery(Name ="id")] int id)
        {
            return _employeeServices.Delete(id);
        }
    }
}
