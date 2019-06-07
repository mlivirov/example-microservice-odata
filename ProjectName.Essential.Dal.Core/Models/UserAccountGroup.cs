using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class UserAccountGroup : IEntity
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public int? UserGroupId { get; set; }

        [ForeignKey("UserAccountId")]
        [InverseProperty("UserAccountGroup")]
        public virtual UserAccount UserAccount { get; set; }
        [ForeignKey("UserGroupId")]
        [InverseProperty("UserAccountGroup")]
        public virtual UserGroup UserGroup { get; set; }
    }
}
