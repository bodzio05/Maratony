namespace Maratony.Data
{
    using System.Data.Entity;

    public class ZawodyDbContext : DbContext
    {
        public DbSet<ZawodyEF> Zawody { get; set; }
        public DbSet<BiegaczEF> Biegacze { get; set; }

        public ZawodyDbContext()
            : base()
        {
        }
    }
}