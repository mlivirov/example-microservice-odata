using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectName.Sso.Application.Database
{
    public class User
    {
        [Required]
        public int UserId { get; set; }

        [StringLength(256)]
        [Required]
        public string Guid { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(256)]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(256)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(1024)]
        public string Password { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordSalt { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; }
        
        public ICollection<UserExternalProvider> UserExternalProviders { get; set; }
    }
}