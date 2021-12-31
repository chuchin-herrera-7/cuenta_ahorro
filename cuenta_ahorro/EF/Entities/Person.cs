using System.ComponentModel.DataAnnotations;

namespace cuenta_ahorro.EF.Entities
{
    // Persona
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "Nombre completo es requerido", AllowEmptyStrings = false)]
        public string FullName { get; set; }
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Correo es requerido", AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Contraseña es requerido", AllowEmptyStrings = false)]
        public string Password { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Tipo es requerido", AllowEmptyStrings = false)]
        public int Type { get; set; } // 1.Cliente - 0.Sistema
    }
}
