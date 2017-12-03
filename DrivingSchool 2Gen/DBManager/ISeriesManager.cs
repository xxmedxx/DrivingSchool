using System.Collections.Generic;
using DrivingSchoolDB;
using System.Threading.Tasks;

namespace DBManager
{
    public interface ISeriesManager
    {
        void AddNew(Serie s);
        void Delete(int id);
        void Delete(Serie s);
        IEnumerable<Serie> GetAll();
        Serie GetSerie(int id);
        void Update(Serie s);
        Task<List<Serie>> GetAllAsync();
        Task<Serie> GetSerieAsync(int? id);
        Task<int> SaveAsync();
    }
}