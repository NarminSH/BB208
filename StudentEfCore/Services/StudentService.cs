using StudentEfCore.DAL;
using StudentEfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCore.Services
{
    public class StudentService
    {
        private readonly AppDbContext _dbContext;
        public StudentService(AppDbContext context)
        {
            _dbContext = context;
        }

        public void AddStudent(Student student)
        {
            _dbContext.Students.Add(student);
            
        }
        public void DeleteStudent(Student student)
        {

            _dbContext.Students.Remove(student);
            
        }
        public void UpdateStudent(Student student)
        {
           
           
            if (student != null)
            {
                Console.WriteLine("Yeni ad daxil et:");
                student.Name = Console.ReadLine();
                Console.WriteLine("Yeni yash daxil et");
                student.Age = Convert.ToInt32(Console.ReadLine());
               
            }
            else
            {
                Console.WriteLine("Telebe tapilmadi");
            }
        }
        public void GetAllStudents()
        {
            
            List<Student> students = _dbContext.Students.ToList();
            foreach (Student item in students)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Age);
            }
        }
        public Student? IfExists(int Id)
        {
            
            Student? student = _dbContext.Students.Find(Id);
            return student;

        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }
}
