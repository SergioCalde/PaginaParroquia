namespace PaginaParroquia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Confirma")]
    public partial class Confirma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Confirma()
        {
            Matrimonios = new HashSet<Matrimonio>();
            Matrimonios1 = new HashSet<Matrimonio>();
        }

        [Key]
        public int IDConfirma { get; set; }

        public int IDPersona { get; set; }

        public int IDRelacion { get; set; }

        public int IDBautismo { get; set; }

        [Required]
        [StringLength(25)]
        public string Padrino { get; set; }

        [Required]
        [StringLength(25)]
        public string Obispo { get; set; }

        [Required]
        [StringLength(25)]
        public string Lugar_Confirma { get; set; }

        public DateTime Fecha { get; set; }

        public int Libro { get; set; }

        public int Folio { get; set; }

        public int Asiento { get; set; }

        public virtual Bautismo Bautismo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matrimonio> Matrimonios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matrimonio> Matrimonios1 { get; set; }

        public virtual RelacionFamiliar RelacionFamiliar { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
