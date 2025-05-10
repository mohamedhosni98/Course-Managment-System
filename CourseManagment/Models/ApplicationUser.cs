using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required,MaxLength(200)]
        public string First_Name { get; set; }
        [Required, MaxLength(200)]
        public string Last_Name { get; set; }
        public string Address { get; set; }
    }
}
