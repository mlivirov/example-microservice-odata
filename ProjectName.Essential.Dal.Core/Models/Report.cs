using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class Report : IEntity
    {
        public int Id { get; set; }
        [Column("createdAt", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("fileInfoRecordId")]
        public int FileInfoRecordId { get; set; }
        public int ReportTemplateId { get; set; }

        [ForeignKey("FileInfoRecordId")]
        [InverseProperty("Report")]
        public virtual FileInfoRecord FileInfoRecord { get; set; }
        [ForeignKey("ReportTemplateId")]
        [InverseProperty("Report")]
        public virtual ReportTemplate ReportTemplate { get; set; }
    }
}
