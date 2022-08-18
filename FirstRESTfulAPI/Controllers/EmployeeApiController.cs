using FirstRESTfulAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("Add/rawdata")]
        public Message AddEmployee([FromBody] Employee employee)
        {
            Message msg = new Message();
            _employeesContext.Employees.Add(employee);
            try
            {
                int num = _employeesContext.SaveChanges();
                if(num > 0)
                {
                    msg.Code = 200;
                    msg.msg = $"員工{employee.FirstName}新增成功!!";
                }
            }
            catch(DbUpdateException ex)
            {
                HttpResponse response = this.Response;
                response.StatusCode = 400;
                msg.Code = 400;
                msg.msg = $"員工{employee.FirstName}新增失敗!!";
            }
            return msg;
        }


        [HttpPut]
        [Route("update/rawdata")]
        public Message UpdateEmployee([FromBody]Employee employee)
        {
            Message msg = new Message();
            _employeesContext.Add(employee);
            var entity = _employeesContext.Entry<Employee>(employee);
            entity.State = EntityState.Modified;
            try
            {
                int num =_employeesContext.SaveChanges();
                if(num > 0)
                {
                    msg.Code = 200;
                    msg.msg = $"員工{employee.FirstName}修改完成";
                }
            }
            catch(DbUpdateException ex)
            {
                msg.Code = 400;
                msg.msg = $"員工{employee.FirstName}修改失敗";
                this.Response.StatusCode = 400;
            }
            return msg;
        }


        [HttpDelete]
        [Route("delete/rawdata")]
        public Message Delete([FromQuery(Name ="id")] int id)
        {
            Message msg = new Message();
            var query = _employeesContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>();
            if (query == null)
            {
                msg.Code = 400;
                msg.msg = $"查無此員工";
            }
            else 
            {
                _employeesContext.Entry<Employee>(query).State = EntityState.Deleted;
                try
                {
                    int num = _employeesContext.SaveChanges();
                    if(num>0)
                    {
                        msg.Code = 200;
                        msg.msg = $"刪除成功";
                    }
                    
                }
                catch(DbUpdateException ex)
                {
                    msg.Code = 400;
                    msg.msg = "刪除失敗";
                }
            }
            return msg;
        }
    }
}
