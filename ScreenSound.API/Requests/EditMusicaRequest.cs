using static ScreenSound.API.Requests.RequestMusica;

namespace ScreenSound.API.Requests
{
    public record EditMusicaRequest(int Id, string nome, int ArtistaId, int anoLancamento)
    : RequestMusica(nome, ArtistaId, anoLancamento);
}
