using CoreAPI_November.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI_November.Controllers
{
    [Route("api/EMP/")]
    [ApiController]
    public class EmpAPI : ControllerBase
    {
        EmpDB dbobj = new EmpDB();

        [HttpGet]
        [Route("GetAllEmployees")]
        public  List<EmployeeCls> Get()
        {
            return dbobj.ViewEmployees();
        }


        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public EmployeeCls Get(int id)
        {
            return dbobj.emp_profile(id);
        }


        [HttpPost]
        [Route("AddEmployee")]
        public void Post(EmployeeCls emp)
        {
            dbobj.InsertEmployee(emp);
        }

  
        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public string Put(int id, EmployeeCls emp)
        {
            return dbobj.UpdateEmployee(emp,id);
        }

        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public string Delete(int id)
        {
            return dbobj.DeleteEmployee(id);
        }
    }
}
