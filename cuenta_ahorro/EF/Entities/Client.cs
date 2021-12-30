using System.ComponentModel.DataAnnotations;

namespace cuenta_ahorro.EF.Entities
{
    // Cliente
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "Nombre completo es requerido", AllowEmptyStrings = false)]
        public string FullName { get; set; }
    }
}
