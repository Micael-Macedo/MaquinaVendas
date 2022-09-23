using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MAQUINAVENDAS.Models
{
    public class Item
    {
        [Key]
        [Display(Name = "Código")]
        public int Code { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Display(Name = "Preço")]
        public double Price { get; set; }
        [Display(Name = "Data de Validade")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "Descrição")]
        public string? Description { get; set; }
        [ForeignKey("Category")]
        public int FkCategory { get; set; }
        public virtual Category? Category { get; set; }
    
    }
}