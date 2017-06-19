using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("operacion")]
    public class Operacion : PropertyValidateModel
    {
        public int operacionid { get; set; }
        [Required]
        public string nombreoperacion { get; set; }
        [Required]
        public string tipooperacion { get; set; }
        [Required]
        public decimal cuantia { get; set; }
        [Required]
        public DateTime fechaoperacion { get; set; }
        [Required]
        public DateTime fechavalor { get; set; }

        public virtual CuentaBancaria CuentaBancaria { get; set; }
    }
}
