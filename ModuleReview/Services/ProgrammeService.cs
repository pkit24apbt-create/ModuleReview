using Microsoft.EntityFrameworkCore;
using ModuleReview.Data;
using ModuleReview.Data.Models;

namespace ModuleReview.Services
{
    public class ProgrammeService
    {
        IDbContextFactory<ApplicationDbContext> contextFactory;
        public ProgrammeService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public void Add(Programme record)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                context.Programmes.Add(record);
                context.SaveChanges();
            }
        }
        public Programme GetById(int id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var record = context.Programmes.SingleOrDefault(m => m.Id == id);
                return record;
            }
        }
        public Programme GetByCode(string code)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var record = context.Programmes.SingleOrDefault(m => m.Code == code);
                return record;
            }
        }
        public void Update(int id, string code, string title, int credits)
        {
            var record = GetById(id);
            if (record == null)
            {
                throw new Exception("Record does not exist. Nothing to update");
            }

            record.Code = code;
            record.Title = title;
            record.Credits = credits;

            using (var context = contextFactory.CreateDbContext())
            {
                context.Update(record);
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var record = GetById(id);
            if(record == null)
            {
                throw new Exception("Record does not exist. Nothig to Delete");
            }
            using (var context = contextFactory.CreateDbContext())
            {
                context.Remove(record);
                context.SaveChanges();
            }
        }

        public List<Programme> GetList()
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var records = context.Programmes.ToList();
                return records;
            }
        }

        public List<Programme> GetFilteredList(string searchText)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var records = context.Programmes.Where(r => r.Code.ToLower().Contains(searchText.ToLower()) || r.Title.ToLower().Contains(searchText.ToLower())).ToList();
                return records;
                 }
        }
        public List<Programme> GetListByProgrammeLeaderid(int programmeLeaderid)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var records = context.Programmes.Where(r => r.ProgrammeLeaderId.Equals(programmeLeaderid)).ToList();
                return records;
            }
        }
    }
}
