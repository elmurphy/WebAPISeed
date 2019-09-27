using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(128)]
        public string NameSurname { get; set; }
    }
}
