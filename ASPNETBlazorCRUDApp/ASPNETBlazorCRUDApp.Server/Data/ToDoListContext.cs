using ASPNETBlazorCRUDApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace ASPNETBlazorCRUDApp.Server.Data
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) { }

        public DbSet<ToDoListItem> ToDoListItems { get; set; }
    }
}
