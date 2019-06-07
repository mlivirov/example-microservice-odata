using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class FileInfoRecord : IEntity
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public long? Size { get; set; }
        public Guid Uuid { get; set; }
        public bool IsShared { get; set; }
    }
}
