using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using static ScreenSound.API.Requests.RequestMusica;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndpointMusicas(this WebApplication app)
        {
            #region Endpoint Músicas
            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                var musicaList = dal.Listar();
                if (musicaList is null)
                {
                    return Results.NotFound();
                }
                var musicaListResponse = EntityListToResponseList(musicaList);
                return Results.Ok(musicaListResponse);
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(EntityToResponse(musica));

            });

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromServices] DAL<Genero> dalGenero, [FromBody] RequestMusica reqMusica) =>
            {
                var musica = new Musica(reqMusica.nome)
                {
                    ArtistaId = reqMusica.ArtistaId,
                    AnoLancamento = reqMusica.anoLancamento,
                    Generos = reqMusica.Generos is not null ? GeneroRequestConverter(reqMusica.Generos, dalGenero) :
                    new List<Genero>()

                };
                dal.Adicionar(musica);
                return Results.Ok();
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) => {
                var musica = dal.RecuperarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(musica);
                return Results.NoContent();

            });

            app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] EditMusicaRequest editMusicaRequest) => {
                var musicaAAtualizar = dal.RecuperarPor(a => a.Id == editMusicaRequest.Id);
                if (musicaAAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaAAtualizar.Nome = editMusicaRequest.nome;
                musicaAAtualizar.AnoLancamento = editMusicaRequest.anoLancamento;

                dal.Atualizar(musicaAAtualizar);
                return Results.Ok();
            });
            #endregion

        }

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
        {
            var listaDeGeneros = new List<Genero>();
            foreach (var item in generos)
            {
                var entity = RequestToEntity(item);
                var genero = dalGenero.RecuperarPor(g => g.Nome.ToUpper().Equals(item.Nome.ToUpper()));
                if (genero is not null)
                {
                    listaDeGeneros.Add(genero);
                }
                else
                {
                    listaDeGeneros.Add(entity);
                }
            }

            return listaDeGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista?.Id ?? 0, musica.Artista.Nome ?? "Artista não cadastrado");
        }



    }
    }

            