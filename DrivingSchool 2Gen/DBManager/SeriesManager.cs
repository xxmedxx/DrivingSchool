using DrivingSchoolDB;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DBManager
{
    public class SeriesManager
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
        public Serie GetSerie(int id)
        {
            return _Db.Series.FirstOrDefault<Serie>(s => s.Id == id);
        }

        public void AddNew(Serie s)
        {
            var rc = _Db.Series.Add(s);
            _Db.SaveChanges();
        }

        public void Update(Serie s)
        {
            var rc = GetSerie(s.Id);
            if (rc != null)
            {
                rc = s;
                _Db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var rc = GetSerie(id);
            if (rc != null)
            {
                _Db.Series.Remove(rc);
                _Db.SaveChanges();
            }
        }

    }
}
