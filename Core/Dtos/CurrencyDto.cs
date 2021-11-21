using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class CurrencyDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
        [Required, MaxLength(3, ErrorMessage = "Numericode execeeds limits")]
        public string NumericCode { get; set; }
        [Required, MaxLength(3, ErrorMessage = "Numericode execeeds limits")]
        public string CurrencyCode { get; set; }
    }
}
