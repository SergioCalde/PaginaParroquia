namespace PaginaParroquia.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SacramentosModel : DbContext
    {
        public SacramentosModel()
            : base("name=SacramentosModel")
        {
        }

        public virtual DbSet<Bautismo> Bautismoes { get; set; }
        public virtual DbSet<Confirma> Confirmas { get; set; }
        public virtual DbSet<Matrimonio> Matrimonios { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<PrimeraComunion> PrimeraComunions { get; set; }
        public virtual DbSet<RelacionFamiliar> RelacionFamiliars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Parroquia)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Presbitero)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Parroco)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Barrio)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Distrito)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Canton)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Provincia)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Padrinos)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Declarante)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .Property(e => e.Ced_Declarante)
                .IsFixedLength();

            modelBuilder.Entity<Bautismo>()
                .HasMany(e => e.Matrimonios)
                .WithRequired(e => e.Bautismo)
                .HasForeignKey(e => e.IDBautismoEsposa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bautismo>()
                .HasMany(e => e.Matrimonios1)
                .WithRequired(e => e.Bautismo1)
                .HasForeignKey(e => e.IDBautismoEsposo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bautismo>()
                .HasMany(e => e.Confirmas)
                .WithRequired(e => e.Bautismo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bautismo>()
                .HasMany(e => e.PrimeraComunions)
                .WithRequired(e => e.Bautismo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Confirma>()
                .Property(e => e.Padrino)
                .IsFixedLength();

            modelBuilder.Entity<Confirma>()
                .Property(e => e.Obispo)
                .IsFixedLength();

            modelBuilder.Entity<Confirma>()
                .Property(e => e.Lugar_Confirma)
                .IsFixedLength();

            modelBuilder.Entity<Confirma>()
                .HasMany(e => e.Matrimonios)
                .WithRequired(e => e.Confirma)
                .HasForeignKey(e => e.IDConfirmaEsposa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Confirma>()
                .HasMany(e => e.Matrimonios1)
                .WithRequired(e => e.Confirma1)
                .HasForeignKey(e => e.IDConfirmaEsposo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Parroquia)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Presbitero)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Testigo1)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.EstadoCivil_T1)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Profesion_T1)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Cedula_T1)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Residencia_T1)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Testigo2)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.EstadoCivil_T2)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Profesion_T2)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Cedula_T2)
                .IsFixedLength();

            modelBuilder.Entity<Matrimonio>()
                .Property(e => e.Residencia_T2)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Nombre)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Apellido)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Apellido2)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Lugar_Nacimiento)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Nacionalidad)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Estado_Civil)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Lugar_Residencia)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Profesion)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .Property(e => e.Religion)
                .IsFixedLength();

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Bautismoes)
                .WithRequired(e => e.Persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Confirmas)
                .WithRequired(e => e.Persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Matrimonios)
                .WithRequired(e => e.Persona)
                .HasForeignKey(e => e.IDEsposa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Matrimonios1)
                .WithRequired(e => e.Persona1)
                .HasForeignKey(e => e.IDEsposo)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Persona>()
            //    .HasMany(e => e.RelacionFamiliars)
            //    .WithRequired(e => e.Persona)
            //    .HasForeignKey(e => e.IDPersona1)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Persona>()
            //    .HasMany(e => e.RelacionFamiliars1)
            //    .WithRequired(e => e.Persona1)
            //    .HasForeignKey(e => e.IDPersona2)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.PrimeraComunions)
                .WithRequired(e => e.Persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrimeraComunion>()
                .Property(e => e.Lugar_Comunion)
                .IsFixedLength();

            modelBuilder.Entity<RelacionFamiliar>()
                .Property(e => e.Relacion)
                .IsFixedLength();

            //modelBuilder.Entity<RelacionFamiliar>()
            //    .HasMany(e => e.Confirmas)
            //    .WithRequired(e => e.RelacionFamiliar)
            //    .WillCascadeOnDelete(false);
        }
    }
}
