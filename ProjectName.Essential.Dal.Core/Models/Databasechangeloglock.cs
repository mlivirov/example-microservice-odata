using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    [Table("DATABASECHANGELOGLOCK")]
    public partial class Databasechangeloglock : IEntity
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("LOCKED")]
        public bool Locked { get; set; }
        [Column("LOCKGRANTED", TypeName = "datetime2(3)")]
        public DateTime? Lockgranted { get; set; }
        [Column("LOCKEDBY")]
        [StringLength(255)]
        public string Lockedby { get; set; }
    }
}
