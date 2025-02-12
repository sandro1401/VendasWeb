﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VendasWeb.Areas.Identity.Data;
using VendasWeb.Models;

namespace VendasWeb.Data;

public class VendasWebContext : IdentityDbContext<VendasWebUser>
{
    public VendasWebContext(DbContextOptions<VendasWebContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Cliente>()
               .OwnsMany(c => c.Enderecos, e =>
               {
                   e.WithOwner().HasForeignKey("IdUsuario");
                   e.HasKey("IdUsuario", "IdEndereco");
               });
        builder.Entity<Pedido>().
            OwnsOne(p => p.EnderecoEntrega, e =>
            {
                e.Ignore(e => e.IdEndereco);
                e.Ignore(e => e.Selecionado);
                e.ToTable("Pedido");

            });
        builder.Entity<ItemPedido>().HasKey(p => new { p.IdPedido, p.IdProduto });
        
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
       
    }

public DbSet<VendasWeb.Models.Categoria> Categoria { get; set; } = default!;

public DbSet<VendasWeb.Models.Usuario> Usuario { get; set; } = default!;

public DbSet<VendasWeb.Models.Cliente> Cliente { get; set; } = default!;

public DbSet<VendasWeb.Models.Endereco> Endereco { get; set; } = default!;

public DbSet<VendasWeb.Models.ItemPedido> ItemPedido { get; set; } = default!;

public DbSet<VendasWeb.Models.NotaFiscal> NotaFiscal { get; set; } = default!;

public DbSet<VendasWeb.Models.Pedido> Pedido { get; set; } = default!;

public DbSet<VendasWeb.Models.Produto> Produto { get; set; } = default!;
}
