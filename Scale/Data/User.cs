using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScaleManagment.Data
{
    [Table("users")]
    public class User
    {
        [Column("firstname")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        [Column("lastname")]
        public string Surname { get; set; }
    }
}
