using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cuenta_ahorro.EF.Entities
{
    // cuentas de ahorro
    public class OpeningSavingAccount
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Numero de cuenta")]
        [Required(ErrorMessage = "Numero de cuenta es requerido", AllowEmptyStrings = false)]
        public string AccountNumber { get; set; }
        [Display(Name = "Saldo")]
        [Required(ErrorMessage = "Saldo es requerido")]
        public double Balance  { get; set; }
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Cliente es requerido")]
        public int IdClient { get; set; }
    }

}
