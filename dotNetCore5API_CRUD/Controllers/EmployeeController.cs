using dotNetCore5API_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotNetCore5API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly pubsContext _db;
        public EmployeeController(pubsContext _pubsContext)
        {
            _db = _pubsContext;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return _db.Employees;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(string id)
        {
            try
            {
                var result = _db.Employees.Find(id);
                if (result.EmpId == null)
                {
                    return NotFound("can't found!");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Search Employee Error by EmpId {id}.", nameof(id), ex);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] Employee value, int Gender)
        {
            try
            {
                var pub = _db.Publishers.Find(value.PubId);
                var job = _db.Jobs.Find(value.JobId);
                if (pub == null)
                {
                    return BadRequest($"Not current PubId : {value.PubId}");
                }
                if (job == null)
                {
                    return BadRequest($"Not current JobId : {value.JobId}");
                }

                if(value.JobLvl == null)
                {
                    value.JobLvl = (byte)new Random().Next(job.MinLvl, job.MaxLvl);
                }

                string newEmpId = CreateEmpId(value, Gender);
                var isCreatedEmpId = _db.Employees.Where(x=>x.EmpId == newEmpId).FirstOrDefault(); 
                while(isCreatedEmpId !=null)
                {
                    newEmpId = CreateEmpId(value, Gender);
                    isCreatedEmpId = _db.Employees.Where(x => x.EmpId == newEmpId).FirstOrDefault();
                }
                value.EmpId = newEmpId;
                value.HireDate = DateTime.Now;

                _db.Employees.Add(value);
                _db.SaveChanges();

                return Ok(value);/* CreatedAtAction(nameof(Get),new { id = value.EmpId }, value);*/
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Employee value)
        {

            _db.Entry(value).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (!_db.Employees.Any(x => x.EmpId == value.EmpId))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "Update Error");
                }
            }
            return NoContent();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var d = _db.Employees.Find(id);
            if (d == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(d);
            _db.SaveChanges();
            return NoContent();
        }


        private string CreateEmpId(Employee data, int Gender)
        {
            StringBuilder newEmpId =
                    new StringBuilder(data.Fname.ToUpper().Substring(0, 1));

            if (!string.IsNullOrEmpty(data.Minit))
            {
                newEmpId.Append(data.Minit.ToUpper());
            }
            else
            {
                newEmpId.Append("-");
            }
            newEmpId.Append(data.Lname.ToUpper().Substring(0, 1));

            newEmpId.Append(new Random().Next(0, 100000).ToString());

            if (Gender == 0)
            {
                newEmpId.Append("F");
            }
            else
            {
                newEmpId.Append("M");
            }
            return newEmpId.ToString();
        }
    }
}
