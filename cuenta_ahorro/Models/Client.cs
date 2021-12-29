using System.ComponentModel.DataAnnotations;

namespace cuenta_ahorro.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
