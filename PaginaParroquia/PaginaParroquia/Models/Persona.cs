namespace PaginaParroquia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Persona")]
    public partial class Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persona()
        {
            Bautismoes = new HashSet<Bautismo>();
            Confirmas = new HashSet<Confirma>();
            Matrimonios = new HashSet<Matrimonio>();
            Matrimonios1 = new HashSet<Matrimonio>();
            RelacionFamiliars = new HashSet<RelacionFamiliar>();
            RelacionFamiliars1 = new HashSet<RelacionFamiliar>();
            PrimeraComunions = new HashSet<PrimeraComunion>();
        }

        [Key]
        public int IDPersona { get; set; }

        [Required]
        [StringLength(15)]
        public string Cedula { get; set; }

        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(25)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(25)]
        public string Apellido2 { get; set; }

        public DateTime Fecha_Nacimiento { get; set; }

        [StringLength(25)]
        public string Lugar_Nacimiento { get; set; }

        [Required]
        [StringLength(25)]
        public string Nacionalidad { get; set; }

        [Required]
        [StringLength(25)]
        public string Estado_Civil { get; set; }

        [StringLength(100)]
        public string Lugar_Residencia { get; set; }

        [StringLength(25)]
        public string Profesion { get; set; }

        [StringLength(25)]
        public string Religion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bautismo> Bautismoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Confirma> Confirmas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matrimonio> Matrimonios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matrimonio> Matrimonios1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelacionFamiliar> RelacionFamiliars { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelacionFamiliar> RelacionFamiliars1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrimeraComunion> PrimeraComunions { get; set; }
    }
}
