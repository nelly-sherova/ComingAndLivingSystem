using System.Text.Json.Serialization;

namespace ComingAndLivingSystem.Models
{
    public class JobTitle
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        [JsonIgnore] 

        public ICollection<Employee> Employees { get; set; }    
    }
}
