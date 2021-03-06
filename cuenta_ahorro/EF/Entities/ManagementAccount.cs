using System.ComponentModel.DataAnnotations;

namespace cuenta_ahorro.EF.Entities
{
    // Administrar cuenta de ahorro
    public class ManagementAccount
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Monto de transacción")]
        [Required(ErrorMessage = "Monto de transación es requerido")]
        public double Amount { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Tipo")]
        public int Type { get; set; }
        [Display(Name = "Cuenta ahorro")]
        [Required(ErrorMessage = "Cuenta ahorro")]
        public int IdOpeningSavingAccount { get; set; }
        public int Status { get; set; }
    }
}
