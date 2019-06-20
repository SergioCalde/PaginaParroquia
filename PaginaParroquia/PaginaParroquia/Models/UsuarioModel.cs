namespace PaginaParroquia.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UsuarioModel : DbContext
    {
        public UsuarioModel()
            : base("name=UsuarioModel")
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(e => e.usuario)
                .IsFixedLength();

            modelBuilder.Entity<Usuario>()
                .Property(e => e.rol)
                .IsFixedLength();

            modelBuilder.Entity<Usuario>()
                .Property(e => e.PpMensaje)
                .IsFixedLength();
        }
    }
}
