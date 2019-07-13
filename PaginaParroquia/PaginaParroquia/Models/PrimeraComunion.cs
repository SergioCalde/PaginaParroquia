namespace PaginaParroquia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PrimeraComunion")]
    public partial class PrimeraComunion
    {
        [Key]
        public int IDPrimeraComunion { get; set; }

        public int IDPersona { get; set; }

        public int IDBautismo { get; set; }

        [Required]
        [StringLength(30)]
        public string Lugar_Comunion { get; set; }

        public DateTime Fecha { get; set; }

        public int Libro { get; set; }

        public int Folio { get; set; }

        public int Asiento { get; set; }

        public virtual Bautismo Bautismo { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
