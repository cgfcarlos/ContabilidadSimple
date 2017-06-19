using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("usuario")]
    public class Usuario : PropertyValidateModel
    {
        public Usuario()
        {
            CuentasBancarias = new HashSet<CuentaBancaria>();
        }

        public int usuarioid { get; set; }
        [Required]
        [RegularExpression(@"^(([A-Z])|\d)\d{7}(?(2)\d|[A-Z])$",ErrorMessage ="Introduzca un DNI correcto")]
        public string dniusuario { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Nombre limitado entre 3 y 50 caracteres")]
        public string nombreusuario { get; set; }
       [Required]
        public string apellidosusuario { get; set; }
        [Required]
        [StringLength(25,MinimumLength =4,ErrorMessage ="Nick limitado entre 4 y 25 caracteres")]
        public string nickusuario { get; set; }
        [Required]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener al entre 8 y 16 caracteres, al menos un dígito, al menos una minúscula y al menos una mayúscula. Puede tener otros símbolos.")]
        [StringLength(16, MinimumLength = 8)]
        public string passwordusuario { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage ="Introduzca un email valido")]
        public string emailusuario { get; set; }
        [Required]
        [RegularExpression(@"^[+-]?\d+(\.\d+)?$",ErrorMessage ="Introduzca un teléfono valido")]
        public string tlfusuario { get; set; }

        public virtual ICollection<CuentaBancaria> CuentasBancarias { get; set; }
    }
}
