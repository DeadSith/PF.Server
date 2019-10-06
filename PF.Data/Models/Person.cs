using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PF.Data.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(1)]
        public string Sex { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public Guid? DisabilityGroupId { get; set; }

        public virtual PensionModifier DisabilityGroup { get; set; }

        [JsonIgnore]
        public virtual ICollection<Experience> Experiences { get; set; }
    }
}
