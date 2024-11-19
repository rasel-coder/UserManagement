using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Data;

public class IdentityContext : IdentityDbContext<ApplicationUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //foreach (var foreignKey in builder.Model.GetEntityTypes()
        //    .SelectMany(x => x.GetForeignKeys()))
        //{
        //    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        //}
    }
}
