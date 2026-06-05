using Microsoft.EntityFrameworkCore;
using ModuleReview.Data;
using ModuleReview.Data.Models;

namespace ModuleReview.Services
{
    public class StaffService
    {
            IDbContextFactory<ApplicationDbContext> contextFactory;
            public StaffService(IDbContextFactory<ApplicationDbContext> contextFactory)
            {
                this.contextFactory = contextFactory;
            }
            public void Add(Staff record)
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    context.Staffing.Add(record);
                    context.SaveChanges();
                }
            }
            public Staff? GetByID(int id)
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    var record = context.Staffing.SingleOrDefault(m => m.Id == id);
                    return record;
                }
            }
            public Staff GetByTitle(string title)
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    var record = context.Staffing.SingleOrDefault(m => m.Title == title);
                    return record;
                }
            }
            public void Update(int id, string title, string forename, string surname)
            {
                var record = GetByID(id);
                if (record == null)
                {
                    throw new Exception("Record does not exist. Nothing to update");
                }

                record.Title = title;
                record.Forename = forename;
                record.Surname = surname;

                using (var context = contextFactory.CreateDbContext())
                {
                    context.Update(record);
                    context.SaveChanges();
                }
            }
            public void Delete(int id)
            {
                var record = GetByID(id);
                if (record == null)
                {
                    throw new Exception("Record does not exist. Nothig to Delete");
                }
                using (var context = contextFactory.CreateDbContext())
                {
                    context.Remove(record);
                    context.SaveChanges();
                }
            }

            public List<Staff> GetList()
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    var records = context.Staffing.ToList();
                    return records;
                }
            }

            public List<Staff> GetFilteredList(string searchText)
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    var records = context.Staffing.Where(r => r.Title.ToLower().Contains(searchText.ToLower()) || r.Forename.ToLower().Contains(searchText.ToLower())).ToList();
                    return records;
                }
            }
        
    }

}

