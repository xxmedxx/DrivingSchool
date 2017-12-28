// The following is a generic repository that accepts type T on all methods.  Since this is just a copy and paste of live code, you will need to change the context property and constructor parameter to use whichever DbContext you're using.  I highly recommend you also use dependency injection.

public class CompanyRepository 
    {
        public CompanyDbContext Context { get; private set; }

        public CompanyRepository(CompanyDbContext context)
        {
            Context = context;
        }

        public ICollection<T> GetAll<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> set = Context.Set<T>();
            return (includes.Aggregate(set, (current, include) => current.Include(include)).ToList() ?? default(ICollection<T>));
        }
        public async Task<ICollection<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> set = Context.Set<T>();
            return (await includes.Aggregate(set, (current, include) => current.Include(include)).ToListAsync() ?? default(ICollection<T>));
        }

        public T Get<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> set = Context.Set<T>().Where(predicate);
            return (includes.Aggregate(set, (current, include) => current.Include(include)).FirstOrDefault() ?? default(T));
        }
        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> set = Context.Set<T>().Where(predicate);
            return (await includes.Aggregate(set, (current, include) => current.Include(include)).FirstOrDefaultAsync() ?? default(T));
        }

        public ICollection<T> Find<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> set = Context.Set<T>().Where(predicate);
            return (includes.Aggregate(set, (current, include) => current.Include(include)).ToList() ?? default(ICollection<T>));
        }
        public async Task<ICollection<T>> FindAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> set = Context.Set<T>().Where(predicate);
            return (await includes.Aggregate(set, (current, include) => current.Include(include)).ToListAsync() ?? default(ICollection<T>));
        }

        public T Add<T>(T entity) where T : class
        {
            var added = Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return added.Entity;
        }
        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            var added = Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            return added.Entity;
        }

        public bool Delete<T>(T entity) where T : class
        {
            var state = Context.Set<T>().Remove(entity).State;
            Context.SaveChanges();
            return (state.Equals(EntityState.Detached) || state.Equals(EntityState.Deleted));
        }
        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            var state = Context.Set<T>().Remove(entity).State;
            await Context.SaveChangesAsync();
            return (state.Equals(EntityState.Detached) || state.Equals(EntityState.Deleted));
        }

        public T Edit<T>(T entity) where T : class
        {
            var edited = Context.Set<T>().Update(entity);
            Context.SaveChanges();
            return edited.Entity;
        }
        public async Task<T> EditAsync<T>(T entity) where T : class
        {
            var edited = Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
            return edited.Entity;
        }
    }

//     Some of you might be confused on how to use this repository, so here's how you can do a Get with some includes.

// repo.Get<Customer>(c => c.Id == 3, i => i.Contacts, i => i.Addresses);

// The first parameter is a lambda that is essentially a .FirstOrDefault on the collection.  The second and 3rd parameters are an array of includes. This example will get a customer with an Id of 3 and include the Contacts and Addresses navigation properties on that customer.  If you need to go a 3rd level down then you will want to use a select.

// repo.Get<Customer>(c => c.Id == 3, i => i.Contacts, i => i.Contacts.Select(s => s.Emails));

// This example will return a customer who's Id is 3 along with the Contacts on that customer, and the email addresses associated with the contacts on that customer.