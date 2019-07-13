namespace PaginaParroquia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bautismo")]
    public partial class Bautismo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bautismo()
        {
            Matrimonios = new HashSet<Matrimonio>();
            Matrimonios1 = new HashSet<Matrimonio>();
            Confirmas = new HashSet<Confirma>();
            PrimeraComunions = new HashSet<PrimeraComunion>();
        }

        [Key]
        public int IDBautismo { get; set; }

        public int IDPersona { get; set; }

        [Required]
        [StringLength(25)]
        public string Parroquia { get; set; }

        public DateTime Fecha_Bautismo { get; set; }

        [StringLength(25)]
        public string Presbitero { get; set; }

        [Required]
        [StringLength(25)]
        public string Parroco { get; set; }

        [StringLength(25)]
        public string Barrio { get; set; }

        [StringLength(25)]
        public string Distrito { get; set; }

        [StringLength(25)]
        public string Canton { get; set; }

        [Required]
        [StringLength(25)]
        public string Provincia { get; set; }

        [Required]
        [StringLength(25)]
        public string Padrinos { get; set; }

        [Required]
        [StringLength(25)]
        public string Declarante { get; set; }

        [Required]
        [StringLength(25)]
        public string Ced_Declarante { get; set; }

        public int Libro { get; set; }

        public int Folio { get; set; }

        public int Asiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matrimonio> Matrimonios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matrimonio> Matrimonios1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Confirma> Confirmas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrimeraComunion> PrimeraComunions { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
