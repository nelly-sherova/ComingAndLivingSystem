using ComingAndLivingSystem.Data;
using ComingAndLivingSystem.Interfaces;
using ComingAndLivingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ComingAndLivingSystem.Repository
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly DataContext _context;

        public ShiftRepository(DataContext context)
        {
            _context = context;
        }
        public void EndShift(int employeeId, DateTime endTime)
        {
            var employee = _context.Employees.Include(e => e.Shifts).FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден.");
            }

            var currentShift = employee.Shifts.LastOrDefault(s => s.EndTime == default(DateTime));
            if (currentShift == null)
            {
                throw new Exception("Смена не была начата. Сначала отметьте начало смены.");
            }

            currentShift.EndTime = endTime;
            currentShift.CountOfHours = (int)(endTime - currentShift.StartTime).TotalHours;

            _context.SaveChanges();
        }

        public Shift GetCurrentShift(int employeeId)
        {
            return _context.Shifts
            .Where(s => s.Employee.Id == employeeId && s.EndTime == default(DateTime))
            .FirstOrDefault();
        }

        public void StartShift(int employeeId, DateTime startTime)
        {
            var employee = _context.Employees.Include(e => e.Shifts).FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден.");
            }

            if (employee.Shifts.Any(s => s.EndTime == default(DateTime)))
            {
                throw new Exception("Смена уже начата. Сначала закройте предыдущую смену.");
            }

            var shift = new Shift
            {
                StartTime = startTime,
                EndTime = default(DateTime),
                CountOfHours = 0,
                Employee = employee
            };

            _context.Shifts.Add(shift);
            _context.SaveChanges();
        }
    }
}
