using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class SigninToken
    {
        [Key]
        public Guid Token { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public TokenStatus TokenStatus { get; set; } = TokenStatus.Acceptable;
    }

    public enum TokenStatus
    {
        Acceptable,
        UnAcceptable
    }
}
