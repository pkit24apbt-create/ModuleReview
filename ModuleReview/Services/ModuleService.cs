using Microsoft.EntityFrameworkCore;
using ModuleReview.Data;
using ModuleReview.Data.Models;
namespace ModuleReview.Services
{
    public class ModuleService
    {
        IDbContextFactory<ApplicationDbContext> contextFactory;

        public ModuleService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Add(Module module)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                context.Modules.Add(module);

                context.SaveChanges();
            }
        }
    }
}
