using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
        public record RequestMusica
        ([Required] string nome, [Required] int ArtistaId, int anoLancamento,ICollection<GeneroRequest> Generos=null);
    }
