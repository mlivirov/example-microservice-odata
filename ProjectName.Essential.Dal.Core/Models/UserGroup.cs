using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class UserGroup : IEntity
    {
        public UserGroup()
        {
            UserAccountGroup = new HashSet<UserAccountGroup>();
            UserGroupRole = new HashSet<UserGroupRole>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string GroupName { get; set; }

        [InverseProperty("UserGroup")]
        public virtual ICollection<UserAccountGroup> UserAccountGroup { get; set; }
        [InverseProperty("UserGroup")]
        public virtual ICollection<UserGroupRole> UserGroupRole { get; set; }
    }
}
