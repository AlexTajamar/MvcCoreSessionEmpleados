using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreSessionEmpleados.Models
{
    [Table("EMP")]
    public class Empleado
    {
        [Key]
        [Column("EMP_NO")]
        public int emp_no { get; set; }
        [Column("APELLIDO")]
        public string apellido { get; set; }
        [Column("OFICIO")]
        public string oficio { get; set; }
        [Column("SALARIO")]
        public int? salario { get; set; }
        [Column("DEPT_NO")]
        public int? dept_no { get; set; }
    }
}
