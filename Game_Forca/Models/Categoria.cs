using System.ComponentModel.DataAnnotations;

namespace GameForca.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Descricao { get; set; } = null!;



    }
}
