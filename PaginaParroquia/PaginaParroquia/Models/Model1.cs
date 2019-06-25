namespace PaginaParroquia.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=UsuariosModel")
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .Property(e => e.rol)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.usuario)
                .IsFixedLength();

            modelBuilder.Entity<Usuario>()
                .Property(e => e.PpMensaje)
                .IsFixedLength();
        }
    }
}
