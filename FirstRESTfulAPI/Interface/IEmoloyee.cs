using FirstRESTfulAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstRESTfulAPI.Interface
{
    public interface IEmoloyee
    {
         ActionResult<IEnumerable<Employee>> GetAll();
         ActionResult<IEnumerable<Employee>> Get( int id);
         Message AddEmployee(Employee employee);
         Message UpdateEmployee(Employee employee);
         Message Delete(int id);


    }
}
