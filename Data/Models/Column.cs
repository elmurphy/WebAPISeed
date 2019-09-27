using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Data.BaseModels.Interfaces;
using Models.BaseModels;

namespace Data.Models
{
    public class Column : IdBase, Sortable
    {
        [MaxLength(256)]
        [Column(TypeName = "nvarchar(256)")]
        public string Name { get; set; }


        [MaxLength(512)]
        [Column(TypeName = "nvarchar(512)")]
        public string Description { get; set; }

        public ColumnType ColumnType { get; set; }
        public int? Order { get; set; }

        public bool GeneralColumn { get; set; }
    }
    [Flags]
    public enum ColumnType
    {
        //Basit Tipler
        Int = 0,
        String = 1,
        //Yazı Tipleri
        Html = 2,
        LongText = 3,
        //Veri Depolama Tipleri
        [Display(Name = "ForeignKey-Deactive")]
        ForeignKey = 4,
        [Display(Name = "Json-Deactive")]
        Json = 5,
        Image = 6,
        //Extralar
        DateTime = 7,
        Map = 8,
        Email = 9,
        Phone = 10,
        Bool = 11,
        Decimal = 12,
    }
}
