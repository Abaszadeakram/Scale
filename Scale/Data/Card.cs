using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScaleManagment.Data
{
    [Table("cards")]
    public class Card
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("cards")]
        public string Cards { get; set; }

        [Column("drivername")]
        public string DriverName { get; set; }

        [Column("driverlast")]
        public string DriverLast { get; set; }

        [Column("carnumber")]
        public string carnumber { get; set; }

        [Column("carmodel")]
        public string carmodel { get; set; }

        [Column("carcompany")]
        public string carcompany { get; set; }

        [Column("carstatus")]
        public string carstatus { get; set; }

        [Column("desk")]
        public string desk { get; set; }

        [Column("upd")]
        public string upd { get; set; }
    }
}
