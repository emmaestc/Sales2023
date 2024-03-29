﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.shared.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display (Name = "Pais")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede contener mas de {1} caracteres")]
        public string Name { get; set; } = null!;

        public ICollection<State>? States { get; set; }

        public int StatesNumber => States == null ? 0 : States.Count;
    }
}
