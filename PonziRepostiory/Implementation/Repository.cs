using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
   
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext Context)
        {
          
            this.Context = Context;
        }
        public bool Add(T Entry)
        {
            try
            {

                
                Context.Set<T>().Add(Entry);
                //int d=  Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddRange(IEnumerable<T> Entries)
        {
            try
            {
                Context.Set<T>().AddRange(Entries);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(T Entry)
        {
            Context.Set<T>().Remove(Entry);
            return true;
        }

        public IEnumerable<T> GetAll()
        {
          return Context.Set<T>().ToList();
            
        }

        public T GetSingle(int Id)
        {
            var instance = Context.Set<T>().Find(Id);
            return instance;
        }


        public bool Delete(int Id)
        {
            var ent = Context.Set<T>().Find(Id);
            Context.Set<T>().Remove(ent);
            return true;
        }

        public bool Edit(T Item, int Id)
        {
            try
            {
                var inst = Item;
                var ent = Context.Set<T>().Find(Id);

                Context.Entry(ent).State = System.Data.Entity.EntityState.Detached;
                ent = inst;

                Context.Entry(ent).State = System.Data.Entity.EntityState.Modified;

                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
