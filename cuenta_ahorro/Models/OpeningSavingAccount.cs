using System.ComponentModel.DataAnnotations;

namespace cuenta_ahorro.Models
{
    public class OpeningSavingAccount
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double Balance  { get; set; }
    }
}
