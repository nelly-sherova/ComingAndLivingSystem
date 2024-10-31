namespace ComingAndLivingSystem.Models
{
    public class Employee
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }  
        public ICollection<Shift> Shifts { get; set; }
        public JobTitle JobTitle { get; set; }

    }
}
