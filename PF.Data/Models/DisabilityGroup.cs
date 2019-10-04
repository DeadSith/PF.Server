using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PF.Data.Models
{
    public class DisabilityGroup
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Range(1, 3)]
        public int Group { get; set; }

        public double? Coefficient { get; set; }

        public double? FixedPayment { get; set; }

        [JsonIgnore]
        public virtual ICollection<Person> People { get; set; }

    }
}
