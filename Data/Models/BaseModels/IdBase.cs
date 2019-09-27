using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models.BaseModels
{
    public class IdBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;

        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; } = DateTime.Now;
        public DateTime? DeletedOn { get; set; } = null;

        public string CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual ApplicationUser CreatedBy { get; set; }
        //public ApplicationUser ModifiedBy { get; set; }
        //public ApplicationUser DeletedBy { get; set; }
    }
}
