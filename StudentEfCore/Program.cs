using StudentEfCore.Models;
using StudentEfCore.DAL;
using Microsoft.EntityFrameworkCore;
using StudentEfCore.Services;
namespace StudentEfCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext dbContext = new AppDbContext();
            StudentService studentService = new StudentService(dbContext);

            studentService.AddStudent(new Student { Name = "Esmer", Age = 20 });
            studentService.AddStudent(new Student { Name = "Nergiz", Age = 20 });
            studentService.AddStudent(new Student { Name = "Aysu", Age = 20 });
            studentService.SaveChanges();

            Student? existingStudent = studentService.IfExists(11);
            if (existingStudent != null)
            {
                studentService.DeleteStudent(existingStudent);
            }
            else
            {
                Console.WriteLine("Telebe yoxdur");
            }
            studentService.SaveChanges();
            studentService.GetAllStudents();
            
           
        }
        
    }
}
