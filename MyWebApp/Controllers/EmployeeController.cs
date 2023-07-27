using Microsoft.AspNetCore.Mvc;
using CrudOperationsInNetCore.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationsInNetCore.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly BrandContext _dbContext;

        public EmployeeController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployes()
        {
            if(_dbContext.employee == null){
                return NotFound();
            }
            return await _dbContext.employee.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmploye(int id)
        {
            if(_dbContext.employee == null){
                return NotFound();
            }

            var Emp = await _dbContext.employee.FindAsync(id);

            if(Emp == null){

                return NotFound();
            }
            return Emp;
        }

        [HttpPost ("post")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee){

            _dbContext.employee.Add(employee);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmploye) , new {id = employee.id} , employee);

        }

        [HttpGet("emp/{character}")]
        public ActionResult<IEnumerable<Employee>> Emp(char character)
        {
            var employees = _dbContext.employee.ToList()

                .Where(e => e.name.Contains(character))

                .ToList();
                
            if (employees.Count == 0)
            {
                return NotFound($"{character} not found in any employee name");
            }
            return employees;
        }

        [HttpPost]
         public String insertEmployee(Employee employee){

            _dbContext.employee.Add(employee);

            _dbContext.SaveChanges();

            return "Employee Entered Successfully" ;
        }

        [HttpDelete]
        public String deleteEmployee(int id){

            var emp = _dbContext.employee.SingleOrDefault(e => e.id == id);

            if(emp == null){

                return "id not found";
            }

            _dbContext.employee.Remove(emp);
            _dbContext.SaveChanges();

            return $"{id} deleted successfully";
        }

        [HttpPut]
        public ActionResult<Employee> updateEmployee(Employee employee){

            var emp = _dbContext.employee.SingleOrDefault(e => e.id == employee.id);

            if(emp == null){

                return NotFound($"{employee.id} not found");

            }

            emp.name = employee.name; 

            _dbContext.SaveChanges();

            return Ok("updated successfull");

        }
    }
}