using ComingAndLivingSystem.Models;
using System.Diagnostics.Metrics;

namespace ComingAndLivingSystem.Data
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Employees.Any())
            {
                // Создаем несколько объектов JobTitle
                var jobTitles = new List<JobTitle>
                {
                    new JobTitle { Name = "Менеджер" },
                    new JobTitle { Name = "Инженер" },
                    new JobTitle { Name = "Тестировщик свечей " }
                };
                dataContext.JobTitles.AddRange(jobTitles);
                dataContext.SaveChanges();
                var employees = new List<Employee>
        {
            new Employee
            {
                FirstName = "Иван",
                LastName = "Иванов",
                MiddleName = "Иванович",
                JobTitle = jobTitles[0] 
            },
            new Employee
            {
                FirstName = "Петр",
                LastName = "Петров",
                MiddleName = "Петрович",
                JobTitle = jobTitles[1] 
            },
            new Employee
            {
                FirstName = "Сидор",
                LastName = "Сидоров",
                MiddleName = "Сидорович",
                JobTitle = jobTitles[2] 
            }
        };

               
                dataContext.Employees.AddRange(employees);
                dataContext.SaveChanges(); 

                var shifts = new List<Shift>
        {
            new Shift
            {
                StartTime = new DateTime(2024, 10, 31, 9, 0, 0),
                EndTime = new DateTime(2024, 10, 31, 17, 0, 0),
                CountOfHours = 8,
                Employee = employees[0] 
            },
            new Shift
            {
                StartTime = new DateTime(2024, 10, 31, 10, 0, 0),
                EndTime = new DateTime(2024, 10, 31, 18, 0, 0),
                CountOfHours = 8,
                Employee = employees[1] 
            },
            new Shift
            {
                StartTime = new DateTime(2024, 10, 31, 11, 0, 0),
                EndTime = new DateTime(2024, 10, 31, 19, 0, 0),
                CountOfHours = 8,
                Employee = employees[2] 
            }
        };

                dataContext.Shifts.AddRange(shifts);
                dataContext.SaveChanges(); 
            }

            
        }
    }
}
