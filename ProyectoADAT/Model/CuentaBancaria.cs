using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    public class CuentaBancaria : PropertyValidateModel
    {
        public CuentaBancaria()
        {
            Ingresos = new HashSet<Ingreso>();
            Gastos = new HashSet<Gasto>();
            Acciones = new HashSet<Accion>();
            Depositos = new HashSet<Deposito>();
        }
        public int CuentaBancariaId { get; set; }
        [Required]
        [RegularExpression("[A-Z]{2}[0-9]{18}",ErrorMessage ="La cadena debe seguir el siguiente patron: 2 letras mayusculas y 2 numeros (IBAN), seguido de 4 numeros (Entidad Financiera), seguido de otros 4 numeros (BIC), seguido de 2 numeros (codigo de control) y por ultimo seguido de 10 digitos (numero de cuenta interno)")]
        public string numeroCuenta { get; set; }
        [Required]
        [StringLength(200,MinimumLength =10, ErrorMessage ="El titular de la cuenta tiene un minimo de 10 caracteres y un maximo de 200")]
        public string titularCuenta { get; set; }
        [Required]
        public string entidadCuenta { get; set; }
        [Required]
        public string tipoCuenta { get; set; }
        [Required]
        public string paisDomiciliacion { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9]{8}",ErrorMessage ="El formato es incorrecto, debe seguir el siguiente patron: 4 caracteres (código del banco), 2 caracteres (ISO del pais) y seguido de 2 caracteres (localidad)")]
        public string BIC { get; set; }
        [Required]
        public decimal saldo { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Ingreso> Ingresos { get; set; }
        public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<Accion> Acciones { get; set; }
        public virtual ICollection<Deposito> Depositos { get; set; }

        public override string ToString()
        {
            return titularCuenta;
        }
    }
}
