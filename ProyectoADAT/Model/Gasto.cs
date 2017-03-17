using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    public class Gasto : PropertyValidateModel
    {
        public int GastoId { get; set; }
        [Required]
        public string nombreGasto { get; set; }
        [Required]
        public string tipoGasto { get; set; }
        [Required]
        public decimal cuantia { get; set; }
        [Required]
        public DateTime fechaOperacion { get; set; }
        [Required]
        public DateTime fechaValor { get; set; }

        public virtual CuentaBancaria CuentaBancaria { get; set; }
    }
}
