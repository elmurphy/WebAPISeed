using Data.Models;
using Data.VirtualModels;
using Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Cell : IdBase
    {
        public Guid ColumnId { get; set; }
        [ForeignKey("ColumnId")]
        public Column Column { get; set; }

        public int? IntValue { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string StringValue { get; set; }
        public dynamic getValue()
        {
            switch (Column.ColumnType)
            {
                case ColumnType.Int:
                    return IntValue;
                case ColumnType.Json:
                    return StringValue;
                case ColumnType.Image:
                    return new VirtualImage(StringValue);
                default:
                    return StringValue;
            }
        }
    }
}
