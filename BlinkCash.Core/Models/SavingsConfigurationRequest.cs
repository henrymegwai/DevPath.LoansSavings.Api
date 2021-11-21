using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models
{
    public class SavingsConfigurationRequest: Model
    {
        [Required]
        public string ConfigSettings { get; set; }
    }
    public class LoanConfigurationRequest : Model
    {
        [Required]
        public string ConfigSettings { get; set; }
    }
}
