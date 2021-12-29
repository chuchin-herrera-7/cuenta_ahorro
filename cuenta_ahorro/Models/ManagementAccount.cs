using System.ComponentModel.DataAnnotations;

namespace cuenta_ahorro.Models
{
    public class ManagementAccount
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
    }
}
