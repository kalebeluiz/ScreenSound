using static ScreenSound.Web.Requests.RequestMusica;

namespace ScreenSound.Web.Requests
{
    public record EditMusicaRequest(int Id, string nome, int ArtistaId, int anoLancamento)
    : RequestMusica(nome, ArtistaId, anoLancamento);
}
