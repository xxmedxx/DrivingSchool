using DrivingSchoolDB;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DBManager
{
    public class QuestionsManager
    {
        DrivingSchoolDbContext _Db;
        public QuestionsManager(DrivingSchoolDbContext db)
        {
            _Db = db;
        }

        public List<Question> GetAll()
        {
            return _Db.Questions.ToList();
        }
        public Task<List<Question>> GetAllAsync()
        {
            return _Db.Questions.ToListAsync();
        }

        public Question GetQuestion(int id)
        {
            return _Db.Questions.FirstOrDefault(q => q.Id == id);
        }
        public Task<Question> GetQuestionAsync(int id, int SerieNum)
        {
            return _Db.Questions.FirstOrDefaultAsync(m => m.Id == id && m.SerieId == SerieNum);
        }

        public IEnumerable<Question> GetQuestionBySerie(int id)
        {
            return (_Db.Questions.ToListAsync()).Result.Where(q => q.SerieId == id);
        }

        public Task<EntityEntry<Question>> AddNewAsync(Question question)
        {
           return _Db.Questions.AddAsync(question);
        }
        public EntityEntry<Question> AddNew(Question question)
        {
            return _Db.Questions.Add(question);
        }
        public void AddNewAsynk(Question question)
        {
            _Db.Questions.AddAsync(question);
        }

        public void Delete(Question Q)
        {
            _Db.Remove(Q);
        }

        public void Update(Question Q)
        {
            if (Q != null)
            {
                _Db.Questions.Update(Q);
            }
        }

        public int Save()
        {
            return _Db.SaveChanges();
        }
        public Task<int> SaveAsync()
        {
            return _Db.SaveChangesAsync();
        }
    }
}
