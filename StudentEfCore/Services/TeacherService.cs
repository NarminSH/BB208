using StudentEfCore.DAL;
using StudentEfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCore.Services
{
    internal class TeacherService
    {
        private readonly AppDbContext _dbContext;
        public TeacherService(AppDbContext context)
        {
            _dbContext = context;
        }

        public void AddTeacher(Teacher teacher)
        {
            _dbContext.Teachers.Add(teacher);

        }
        

    }
}
