using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp3AzureMarcio.Models;

namespace Negocio.Models
{
    public class DataContext: DbContext
    {
        public DataContext() : base("banco_marcio")
        {
        }

        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Amigo>()
                    .MapToStoredProcedures();

            //Relacionamento um para muitos: Pais X Estado onde um Pais poderá ter muitos Estados
            modelBuilder.Entity<Estado>()
               .HasRequired(s => s.Pais)
               .WithMany(c => c.Estados)
               .HasForeignKey(s => s.PaisId);

            //modelBuilder.Types().Configure(t => t.MapToStoredProcedures());

            //modelBuilder.Entity<Book>()
            //   .HasMany(s => s.Authors)
            //   .WithMany(c => c.Books)
            //   .Map(cs =>
            //   {
            //       cs.MapLeftKey("BookId");
            //       cs.MapRightKey("AuthorId");
            //       cs.ToTable("BookAuthor");
            //   });

            base.OnModelCreating(modelBuilder);
        }
    }
}
