using ComingAndLivingSystem.Data;
using ComingAndLivingSystem.DTO;
using ComingAndLivingSystem.Interfaces;
using ComingAndLivingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ComingAndLivingSystem.Repository
{
    public class EmployeeRepository : IEmployeeRepository 
    {
        private readonly DataContext _context; 
        public EmployeeRepository(DataContext context)
        {
           _context = context;
        }

        public Employee AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public IEnumerable<Employee> GetAll(string? jobTitleName)
        {
            if (jobTitleName != null)
            {
                var employees = _context.Employees
            .Include(e => e.JobTitle) 
            .Where(e => e.JobTitle.Name == jobTitleName)
            .ToList();

                return _context.Employees.Where(e => e.JobTitle.Name == jobTitleName).ToList();
                    
            }
            if(jobTitleName == null) 
                return  _context.Employees.ToList();
            return _context.Employees.ToList();

        }

        public IEnumerable<JobTitle> getJobTitlesByName(string jobTitleName)
        {
            return _context.JobTitles.Where(n => n.Name == jobTitleName).ToList();
        }

        public IEnumerable<JobTitle> jobTitles()
        {
            return _context.JobTitles.ToList();
        }
        public JobTitle GetJobTitleById(int jobTitleId)
        {
            return _context.JobTitles.FirstOrDefault(j => j.Id == jobTitleId);
        }
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }
        public Employee UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.Find(employee.Id);
            if (existingEmployee != null)
            {
                _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                _context.SaveChanges();
            }
            return existingEmployee;
        }

        public void DeleteEmployee(int id)
        {
            var employee = GetEmployeeById(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges(); 
            }
        }

       
    }
}
