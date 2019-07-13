namespace PaginaParroquia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RelacionFamiliar")]
    public partial class RelacionFamiliar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RelacionFamiliar()
        {
            Confirmas = new HashSet<Confirma>();
        }

        [Key]
        public int IDRelacion { get; set; }

        public int IDPersona1 { get; set; }

        public int IDPersona2 { get; set; }

        [Required]
        [StringLength(25)]
        public string Relacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Confirma> Confirmas { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual Persona Persona1 { get; set; }
    }
}
