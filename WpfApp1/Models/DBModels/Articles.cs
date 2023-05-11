namespace WpfApp1.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Articles
    {
        public Guid id { get; set; }

        [StringLength(50)]
        public string Header { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        [Column("Creation Date", TypeName = "date")]
        public DateTime? Creation_Date { get; set; }
    }
}
