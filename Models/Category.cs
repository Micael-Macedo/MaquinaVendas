using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MAQUINAVENDAS.Models
{
    public class Category
    {
        [Display (Name = "Código da Categoria")]
        public int Id { get; set; }
        [Required]
        [Display (Name = "Nome da Categoria")]
        public string Name { get; set; }
        public virtual ICollection<Item>? Items { get; set; }
    }
}