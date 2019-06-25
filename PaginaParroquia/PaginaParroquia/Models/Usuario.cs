namespace PaginaParroquia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Column("usuario")]
        [Required]
        [StringLength(25)]
        [DisplayName("Usuario")]
        public string usuario { get; set; }

        [Required]
        [StringLength(2500)]
        [DisplayName("Password")]
        public string password { get; set; }

        [DisplayName("Rol")]
        public int rol { get; set; }

        [StringLength(10)]
        public string PpMensaje { get; set; }

        public virtual Role Role { get; set; }
    }
}
