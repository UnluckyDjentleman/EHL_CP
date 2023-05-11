namespace WpfApp1.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLAYERS
    {
        public int teamID { get; set; }

        public int Number { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }

        [StringLength(50)]
        public string PositionID { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [Key]
        public Guid PlayerId { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public virtual TEAMS TEAMS { get; set; }
    }
}
