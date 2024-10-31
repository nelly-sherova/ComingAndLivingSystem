using AutoMapper;
using ComingAndLivingSystem.DTO;
using ComingAndLivingSystem.Interfaces;
using ComingAndLivingSystem.Models;
using ComingAndLivingSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ComingAndLivingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper) 
        { 
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        [HttpGet("all")] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType(400)]
        public IActionResult GetAll(string? jobTitleName)
        {
            var employees = _mapper.Map<List<EmployeeDTO>>(_employeeRepository.GetAll(jobTitleName));
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }
        [HttpGet("jobTitles")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<JobTitle>))]
        [ProducesResponseType(400)]
        public IActionResult jobTitles()
        {
            var jobTitles = _employeeRepository.jobTitles(); 
            if (jobTitles == null || !jobTitles.Any())
            {
                return NotFound();
            }
            return Ok(jobTitles);
        }
        [HttpPost("add")]
        [ProducesResponseType(201, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult AddEmployee([FromBody] EmployeeDTO employeeDto)
        {
            if (string.IsNullOrEmpty(employeeDto.FirstName) || string.IsNullOrEmpty(employeeDto.LastName) || employeeDto.JobTitleId == 0)
            {
                return BadRequest("First Name, Last Name, and Job Title are required.");
            }

            var jobTitle = _employeeRepository.GetJobTitleById(employeeDto.JobTitleId);
            if (jobTitle == null)
            {
                return NotFound("Specified Job Title does not exist.");
            }

            var employee = _mapper.Map<Employee>(employeeDto);
            employee.JobTitle = jobTitle;

            var createdEmployee = _employeeRepository.AddEmployee(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(404)]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }
            return Ok(employee);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int id, EmployeeDTO employeeDto)
        {
            if (id <= 0 || employeeDto == null)
            {
                return BadRequest("Не указан Id сотрудника или данные для обновления.");
            }

            var existingEmployee = _employeeRepository.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return NotFound("Сотрудник не найден.");
            }

            var jobTitle = _employeeRepository.GetJobTitleById(employeeDto.JobTitleId);
            if (jobTitle == null)
            {
                return BadRequest("Должность не найдена.");
            }

            existingEmployee.FirstName = employeeDto.FirstName;
            existingEmployee.LastName = employeeDto.LastName;
            existingEmployee.MiddleName = employeeDto.MiddleName;
            existingEmployee.JobTitle = jobTitle; // Присваиваем должность

            var updatedEmployee = _employeeRepository.UpdateEmployee(existingEmployee);

            return Ok(_mapper.Map<EmployeeDTO>(updatedEmployee));
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Не указан корректный Id сотрудника.");
            }

            var existingEmployee = _employeeRepository.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return NotFound("Сотрудник не найден.");
            }

            _employeeRepository.DeleteEmployee(id);

            return NoContent();
        }
    }
}
