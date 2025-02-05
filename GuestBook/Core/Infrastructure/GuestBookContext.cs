using Microsoft.EntityFrameworkCore;

namespace GuestBook.Core.Infrastructure;

public class GuestBookContext(DbContextOptions<GuestBookContext> options) : DbContext(options)
{
    public DbSet<Entities.GuestBook> GuestBooks { get; set; }
}