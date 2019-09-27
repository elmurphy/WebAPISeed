using Data.BaseModels.Interfaces;
using Data.Models.DefaultModels;
using Data.VirtualModels;
using Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class Row : IdBase, Sortable
    {
        public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();
        public int? Order { get; set; }

        public Guid? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
    }
}
