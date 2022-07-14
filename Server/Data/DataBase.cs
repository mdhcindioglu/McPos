using Duende.IdentityServer.EntityFramework.Options;
using McPos.Server.Data.Models;
using McPos.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace McPos.Server.Data
{
    public class DataBase : ApiAuthorizationDbContext<User>
    {

        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Group>? Groups { get; set; }
        public DbSet<Customer>? Customers { get; set; }

        public DataBase(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }
    }
}