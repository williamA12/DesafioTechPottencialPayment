using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class AppDbContexto : DbContext
    {
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        public AppDbContexto(DbContextOptions<AppDbContexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Vendedor
            modelBuilder.Entity<Vendedor>()
                .ToTable("Vendedor")
                .HasKey(vendedor => vendedor.VendedorId);
            modelBuilder.Entity<Vendedor>()
                .Property(vendedor => vendedor.VendedorId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Vendedor>()
                .Property(vendedor => vendedor.Cpf)
                .HasMaxLength(Vendedor.TamanhoMaximoCpf)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Vendedor>()
                .Property(vendedor => vendedor.Email)
                .HasMaxLength(Vendedor.TamanhoMaximoEmail)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Vendedor>()
                .Property(vendedor => vendedor.Nome)
                .HasMaxLength(Vendedor.TamanhoMaximoNome)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Vendedor>()
                .Property(vendedor => vendedor.Telefone)
                .HasMaxLength(Vendedor.TamanhoMaximoTelefone)
                .HasColumnType("varchar")
                .IsRequired();

            // Venda
            modelBuilder.Entity<Venda>()
            .ToTable("Venda")
            .HasKey(venda => venda.VendaId);
            modelBuilder.Entity<Venda>()
                .Property(venda => venda.VendaId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Venda>()
                .Property(venda => venda.VendedorId)
                .HasColumnType("int")
                .IsRequired();
            modelBuilder.Entity<Venda>()
                .Property(venda => venda.Status)
                .IsRequired();
            modelBuilder.Entity<Venda>()
                .Property(venda => venda.DataVenda)
                .HasColumnType("datetime")
                .IsRequired();
            modelBuilder.Entity<Venda>()
                .HasOne<Vendedor>(venda => venda.Vendedor)
                .WithMany(vendedor => vendedor.Vendas)
                .HasForeignKey(venda => venda.VendedorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // ItemVenda
            modelBuilder.Entity<ItemVenda>()
                .ToTable("ItemVenda")
                .HasKey(itemVenda => itemVenda.ItemVendaId);
            modelBuilder.Entity<ItemVenda>()
                .Property(itemVenda => itemVenda.ItemVendaId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ItemVenda>()
                .Property(itemVenda => itemVenda.VendaId)
                .HasColumnType("int")
                .IsRequired();
            modelBuilder.Entity<ItemVenda>()
                .Property(itemVenda => itemVenda.Descricao)
                .HasMaxLength(ItemVenda.TamanhoMaximoDescricao)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<ItemVenda>()
                .Property(itemVenda => itemVenda.Preco)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            modelBuilder.Entity<ItemVenda>()
                .Property(itemVenda => itemVenda.Quantidade)
                .HasColumnType("int")
                .IsRequired();
            modelBuilder.Entity<ItemVenda>()
                .HasOne<Venda>(itemVenda => itemVenda.Venda)
                .WithMany(venda => venda.Itens)
                .HasForeignKey(itemVenda => itemVenda.VendaId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
