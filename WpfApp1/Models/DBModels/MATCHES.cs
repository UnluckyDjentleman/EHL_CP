namespace WpfApp1.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MATCHES
    {
        public int? Team1 { get; set; }

        public string Logo1 { get; set; }

        public int? Goals1 { get; set; }

        public int? Goals2 { get; set; }

        public string Logo2 { get; set; }

        public int? Team2 { get; set; }

        public DateTime? GAME_DATE { get; set; }

        [Key]
        public int matchid { get; set; }

        public virtual TEAMS TEAMS { get; set; }

        public virtual TEAMS TEAMS1 { get; set; }
    }
}
