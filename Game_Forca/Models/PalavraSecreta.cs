using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace GameForca.Models
{
    public class PalavraSecreta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Palavras { get; set; } = null!;

        [Required]
        public int CategoriaId { get; set; }

        [ValidateNever]
        public Categoria Categoria { get; set; } = null!;
    }
}
