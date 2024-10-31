using ComingAndLivingSystem.Models;

namespace ComingAndLivingSystem.Interfaces
{
    public interface IShiftRepository
    {
        void StartShift(int employeeId, DateTime startTime);
        void EndShift(int employeeId, DateTime endTime);
        Shift GetCurrentShift(int employeeId);
    }
}
