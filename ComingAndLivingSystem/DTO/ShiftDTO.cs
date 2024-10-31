namespace ComingAndLivingSystem.DTO
{
    public class ShiftDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CountOfHours { get; set; }
        public int EmployeeId { get; set; }
    }
}
