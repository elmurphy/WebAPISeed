using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.VirtualModels
{
    public class RegisterModel
    {
        [MaxLength(256)]
        public string NameSurname { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string LocationDirections { get; set; }
        [MaxLength(256)]
        public string OrderDetails { get; set; }

        [MaxLength(256)]
        public string VergiDairesi { get; set; }
        [MaxLength(11)]
        public string VergiNo { get; set; }

        #region Location
        public Guid? NeighborhoodId { get; set; }
        [ForeignKey("NeighborhoodId")]
        public Neighborhood Neighborhood { get; set; }
        #endregion
    }
}
