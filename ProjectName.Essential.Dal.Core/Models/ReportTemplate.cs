using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class ReportTemplate : IEntity
    {
        public ReportTemplate()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        [Column("fileInfoRecordId")]
        public int FileInfoRecordId { get; set; }

        [ForeignKey("FileInfoRecordId")]
        [InverseProperty("ReportTemplate")]
        public virtual FileInfoRecord FileInfoRecord { get; set; }
        [InverseProperty("ReportTemplate")]
        public virtual ICollection<Report> Report { get; set; }
    }
}
