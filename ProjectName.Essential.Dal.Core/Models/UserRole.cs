using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class UserRole : IEntity
    {
        public UserRole()
        {
            UserGroupRole = new HashSet<UserGroupRole>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string RoleName { get; set; }

        [InverseProperty("UserRole")]
        public virtual ICollection<UserGroupRole> UserGroupRole { get; set; }
    }
}
