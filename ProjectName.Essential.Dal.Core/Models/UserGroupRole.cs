using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class UserGroupRole : IEntity
    {
        public int Id { get; set; }
        public int? UserGroupId { get; set; }
        public int? UserRoleId { get; set; }

        [ForeignKey("UserGroupId")]
        [InverseProperty("UserGroupRole")]
        public virtual UserGroup UserGroup { get; set; }
        [ForeignKey("UserRoleId")]
        [InverseProperty("UserGroupRole")]
        public virtual UserRole UserRole { get; set; }
    }
}
