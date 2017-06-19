using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("cuentabancaria")]
    public class CuentaBancaria : PropertyValidateModel
    {
        
        public CuentaBancaria()
        {
            //Ingresos = new HashSet<Ingreso>();
            Operaciones = new HashSet<Operacion>();
           // Acciones = new HashSet<Accion>();
            //Depositos = new HashSet<Deposito>();
        }
        public int cuentabancariaid { get; set; }
        [Required]
        [RegularExpression("[A-Z]{2}[0-9]{18}",ErrorMessage ="La cadena debe seguir el siguiente patron: 2 letras mayusculas y 2 numeros (IBAN), seguido de 4 numeros (Entidad Financiera), seguido de otros 4 numeros (BIC), seguido de 2 numeros (codigo de control) y por ultimo seguido de 10 digitos (numero de cuenta interno)")]
        public string numerocuenta { get; set; }
        [Required]
        [StringLength(200,MinimumLength =10, ErrorMessage ="El titular de la cuenta tiene un minimo de 10 caracteres y un maximo de 200")]
        public string titularcuenta { get; set; }
        [Required]
        public string entidadcuenta { get; set; }
        [Required]
        public string tipocuenta { get; set; }
        [Required]
        public string paisdomiciliacion { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9]{8}",ErrorMessage ="El formato es incorrecto, debe seguir el siguiente patron: 4 caracteres (código del banco), 2 caracteres (ISO del pais) y seguido de 2 caracteres (localidad)")]
        public string bic { get; set; }
        [Required]
        public decimal saldo { get; set; }
        public virtual Usuario Usuario { get; set; }
       // public virtual ICollection<Ingreso> Ingresos { get; set; }
        public virtual ICollection<Operacion> Operaciones { get; set; }
        //public virtual ICollection<Accion> Acciones { get; set; }
        //public virtual ICollection<Deposito> Depositos { get; set; }
    }
}
