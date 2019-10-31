namespace PaginaParroquia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Matrimonio")]
    public partial class Matrimonio
    {
        [Key]
        public int IDMatrimonio { get; set; }

        public int IDEsposo { get; set; }

        public int IDEsposa { get; set; }

        [Required]
        [StringLength(25)]
        public string Parroquia { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(25)]
        public string Presbitero { get; set; }

        public int IDBautismoEsposo { get; set; }

        public int IDBautismoEsposa { get; set; }

        public int IDConfirmaEsposo { get; set; }

        public int IDConfirmaEsposa { get; set; }

        [Required]
        [StringLength(25)]
        public string Testigo1 { get; set; }

        [Required]
        [StringLength(25)]
        public string EstadoCivil_T1 { get; set; }

        [Required]
        [StringLength(25)]
        public string Profesion_T1 { get; set; }

        [Required]
        [StringLength(25)]
        public string Cedula_T1 { get; set; }

        [Required]
        [StringLength(25)]
        public string Residencia_T1 { get; set; }

        [Required]
        [StringLength(25)]
        public string Testigo2 { get; set; }

        [Required]
        [StringLength(25)]
        public string EstadoCivil_T2 { get; set; }

        [Required]
        [StringLength(25)]
        public string Profesion_T2 { get; set; }

        [Required]
        [StringLength(25)]
        public string Cedula_T2 { get; set; }

        [Required]
        [StringLength(25)]
        public string Residencia_T2 { get; set; }

        public bool Conyuges { get; set; }

        public int Libro { get; set; }

        public int Folio { get; set; }

        public int Asiento { get; set; }

        public virtual Bautismo Bautismo { get; set; }

        public virtual Bautismo Bautismo1 { get; set; }

        public virtual Confirma Confirma { get; set; }

        public virtual Confirma Confirma1 { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual Persona Persona1 { get; set; }
    }
}

public enum BuscarPorM
{
    Persona,
    Fecha,
    Libro
}