namespace ComingAndLivingSystem.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CountOfHours { get; set; }   
        public Employee Employee { get; set; }
    } 
}
