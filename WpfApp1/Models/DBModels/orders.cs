namespace WpfApp1.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class orders
    {
        [Key]
        [Column(Order = 0)]
        public Guid id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid userid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(32)]
        public string STATUS { get; set; }

        public virtual bonus bonus { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
