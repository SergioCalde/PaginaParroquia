namespace PaginaParroquia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuario
    {
        [Key]
        [Column(Order = 0)]
        public int idUsuario { get; set; }

        [Key]
        [Column("usuario", Order = 1)]
        [StringLength(25)]
        public string usuario { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2500)]
        public string password { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(15)]
        public string rol { get; set; }

        [StringLength(10)]
        public string PpMensaje { get; set; }
    }
}
