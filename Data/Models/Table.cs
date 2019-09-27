using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Models.BaseModels;

namespace Data.Models
{
    public class Table : IdBase
    {
        [MaxLength(256)]
        [Column(TypeName = "nvarchar(256)")]
        public string Name { get; set; }

        [MaxLength(512)]
        [Column(TypeName = "nvarchar(512)")]
        public string Description { get; set; }

        public virtual ICollection<Column> Columns { get; set; }
        public virtual ICollection<Row> Rows { get; set; }
    }
}
