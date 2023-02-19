using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletApi.Models;

namespace WalletApi.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Wallet>? Wallets { get; set; }
        public DbSet<Operation>? Operations { get; set; }
    }
}
