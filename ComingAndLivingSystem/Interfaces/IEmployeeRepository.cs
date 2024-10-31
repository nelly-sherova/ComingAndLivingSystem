using ComingAndLivingSystem.DTO;
using ComingAndLivingSystem.Models;

namespace ComingAndLivingSystem.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll(string? jobTitleName);
        IEnumerable<JobTitle> jobTitles();
        IEnumerable<JobTitle> getJobTitlesByName(string jobTitleName);
        public JobTitle GetJobTitleById(int jobTitleId);
        public Employee GetEmployeeById(int id);
        public Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);



        Employee AddEmployee(Employee employee);

    }
}
