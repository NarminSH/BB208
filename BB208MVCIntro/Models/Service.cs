using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Security;

namespace BB208MVCIntro.Models
{
    public class Service
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
     
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
