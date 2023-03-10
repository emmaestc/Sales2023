﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.shared.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display (Name = "Categoria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede contener mas de {1} caracteres")]
        public string Name { get; set; } = null!;

        //public ICollection<Idcagor>? States { get; set; }

        //public int StatesNumber => States == null ? 0 : States.Count;

        //public ICollection<Category>? Categories { get; set; }

//        public int StatesNumber => Categories == null ? 0 : Categories.Count;
    }
}
