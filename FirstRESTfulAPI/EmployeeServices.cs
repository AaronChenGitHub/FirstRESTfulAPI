using FirstRESTfulAPI.Interface;
using FirstRESTfulAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstRESTfulAPI
{
    public class EmployeeServices : IEmoloyee
    {
        private readonly EmployeesContext _employeesContext;
        private readonly Message _message;
        public EmployeeServices(EmployeesContext employeesContext, Message message)
        {
            _employeesContext = employeesContext;
            _message = message;
        }
        
        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            return _employeesContext.Employees.ToList();
        }

        public ActionResult<IEnumerable<Employee>> Get([FromRoute(Name = "id")] int id)
        {
            return _employeesContext.Employees.Where(x => x.EmployeeId == id).ToList();
        }

        public Message AddEmployee([FromBody] Employee employee)
        {
            _employeesContext.Employees.Add(employee);
            try
            {
                int num = _employeesContext.SaveChanges();
                if (num > 0)
                {
                    _message.Code = 200;
                    _message.msg = $"員工{employee.FirstName}新增成功!!";
                    
                }
            }
            catch (DbUpdateException ex)
            {

                _message.Code = 400;
                _message.msg = $"員工{employee.FirstName}新增失敗!!";
                
            }
            return _message;
        }

        public Message UpdateEmployee([FromBody] Employee employee)
        {
            _employeesContext.Add(employee);
            _employeesContext.Entry<Employee>(employee).State = EntityState.Modified;
            try
            {
                int num = _employeesContext.SaveChanges();
                if (num > 0)
                {
                    _message.Code = 200;
                    _message.msg = $"員工{employee.FirstName}修改完成";
                    
                }
            }
            catch (DbUpdateException ex)
            {
                _message.Code = 400;
                _message.msg = $"員工{employee.FirstName}修改失敗";
                
            }
            return _message;
        }


        public Message Delete([FromQuery(Name = "id")] int id)
        {
            var query = _employeesContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>();
            if (query == null)
            {
                _message.Code = 400;
                _message.msg = $"查無此員工";
                
            }
            else
            {
                _employeesContext.Entry<Employee>(query).State = EntityState.Deleted;
                try
                {
                    int num = _employeesContext.SaveChanges();
                    if (num > 0)
                    {
                        _message.Code = 200;
                        _message.msg = $"刪除成功";
                        
                    }

                }
                catch (DbUpdateException ex)
                {
                    _message.Code = 400;
                    _message.msg = "刪除失敗";
                    
                }
            }
            return _message;
        }

    }
}
