using DrivingSchoolDB;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DBManager
{
    public class SeriesManager : ISeriesManager
    {
        // DbContextOptions<DrivingSchoolDbContext> options;
        DrivingSchoolDbContext _Db;

        public SeriesManager(DrivingSchoolDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<Serie> GetAll()
        {
            return _Db.Series.ToList();
        }
        public Task<List<Serie>> GetAllAsync()
        {
            return _Db.Series.ToListAsync();
        }

        public Serie GetSerie(int id)
        {
            return _Db.Series.FirstOrDefault<Serie>(s => s.Id == id);
        }
        public async Task<Serie> GetSerieAsync(int? id)
        {
            if (id != null)
                return await _Db.Series.FirstOrDefaultAsync<Serie>(s => s.Id == id);
            else
                return null;
        }

        public void AddNew(Serie s)
        {
            var rc = _Db.Series.Add(s);
        }

        public void Update(Serie s)
        {
            if (s != null)
            {
                _Db.Series.Update(s);
            }
        }

        public void Delete(int id)
        {
            var rc = GetSerie(id);
            if (rc != null)
            {
                _Db.Series.Remove(rc);
            }
        }
        public void Delete(Serie s)
        {
            _Db.Series.Remove(s);
        }

        public Task<int> SaveAsync()
        {
            return _Db.SaveChangesAsync();
        }

        public int Save()
        {
            return _Db.SaveChanges();
        }
    }
}
