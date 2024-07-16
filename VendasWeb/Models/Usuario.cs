using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasWeb.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required, MaxLength(128)]
        public string Nome { get; set; }

        [Required, MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(128)]
        public string Senha { get; set; }

        [ReadOnly(true)]
        public DateTime? DataCadastro { get; }
    }
}
