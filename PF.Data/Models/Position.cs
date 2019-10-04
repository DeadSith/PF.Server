﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace PF.Data.Models
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Coefficient { get; set; }

        public bool IsState { get; set; }
    }
}
