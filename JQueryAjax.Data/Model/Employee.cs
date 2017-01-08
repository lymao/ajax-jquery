using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JQueryAjax.Data.Model
{
    [Table("Employees")]
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [StringLength(255)]
        public string Name { set; get; }
        public float Salary { set; get; }
        [Required]
        public DateTime CreatedDate { set; get; }
        public bool Status { set; get; }
    }
}
