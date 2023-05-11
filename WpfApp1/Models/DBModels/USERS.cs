namespace WpfApp1.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USERS()
        {
            orders = new HashSet<orders>();
        }

        public Guid id { get; set; }

        [StringLength(50)]
        public string NAME { get; set; }

        [StringLength(50)]
        public string SURNAME { get; set; }

        [StringLength(50)]
        public string PASSWORD { get; set; }

        public int? ROLE { get; set; }

        [StringLength(13)]
        public string TELEPHONE { get; set; }

        [Column("Favourite team")]
        public int? Favourite_team { get; set; }

        public virtual TEAMS TEAMS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orders> orders { get; set; }
    }
}
