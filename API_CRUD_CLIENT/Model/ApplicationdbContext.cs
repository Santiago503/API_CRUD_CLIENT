using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APICrudClient.Model
{
    public class ApplicationdbContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public ApplicationdbContext(DbContextOptions<ApplicationdbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientAddress> ClientAddress { get; set; }
    }
}
