using StudentEfCore.DAL;
using StudentEfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCore.Services
{
    public class GenericService<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        public GenericService(AppDbContext context)
        {
            _dbContext = context;
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);

        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
