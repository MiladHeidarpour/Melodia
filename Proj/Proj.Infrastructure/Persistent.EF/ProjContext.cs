using Microsoft.EntityFrameworkCore;
using Proj.Domain.ArtistAgg;
using Proj.Domain.CategoryAgg;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAlbumAgg;
using Proj.Domain.RoleAgg;
using Proj.Domain.UserAgg;
using Proj.Domain.VerificationAgg;

namespace Proj.Infrastructure.Persistent.EF;

public class ProjContext : DbContext
{
    public ProjContext(DbContextOptions<ProjContext> options) : base(options)
    {

    }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Music> Musics { get; set; }
    public DbSet<MusicAlbum> MusicAlbums { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Verification> Verifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}