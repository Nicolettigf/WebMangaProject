using Entities;
using Entities.AnimeS;
using Entities.MangaS;

namespace BusinessLogicalLayer.ApiConsumer
{
    public class Converter
    {
        public static T ConvertDTOToEntity<T>(MediaDto item) where T : MediaBase, new()
        {
            if (item == null) return null;

            T entity = new();

            // Mapeia os campos comuns
            item.MapToMediaBase(entity);

            switch (entity)
            {
                case Anime anime:
                    MapAnimeSpecific(anime, item);
                    break;

                case Manga manga:
                    MapMangaSpecific(manga, item);
                    break;
            }

            return entity;
        }

        private static void MapAnimeSpecific(Anime anime, MediaDto item)
        {
            anime.Source = item.source;
            anime.Episodes = item.episodes;
            anime.Airing = item.airing;
            anime.Duration = item.duration;
            anime.Rating = item.rating;
            anime.Season = item.season;

            anime.Themename = item.theme?.name;
            anime.Themetype = item.theme?.type;
            anime.Themeurl = item.theme?.url;

            anime.Youtubeembed_url = item.trailer?.embed_url;
            anime.Youtubeurl = item.trailer?.url;
            anime.Youtube_id = item.trailer?.youtube_id;

            anime.BroadcastDay = item.broadcast?.day;
            anime.BroadcastTime = item.broadcast?.time;
            anime.BroadcastTimezone = item.broadcast?.timezone;

            // Listas
            anime.ThemeSongs = MapThemeSongs(item, anime);
            anime.external = item.MapList(item.external, e => new Entities.AnimeS.External { AnimeId = anime.MalId, Anime = anime, name = e.name, url = e.url });
            anime.studios = item.MapList(item.studios, s => new Entities.AnimeS.Studio { AnimeId = anime.MalId, Anime = anime, mal_id = s.mal_id, type = s.type, name = s.name, url = s.url });
            anime.licensors = item.MapList(item.licensors, l => new Entities.AnimeS.Licensor { AnimeId = anime.MalId, Anime = anime, mal_id = l.mal_id, type = l.type, name = l.name, url = l.url });
            anime.producers = item.MapList(item.producers, p => new Entities.AnimeS.Producer { AnimeId = anime.MalId, Anime = anime, mal_id = p.mal_id, type = p.type, name = p.name, url = p.url });
            anime.relations = item.MapList(item.relations, r => new Entities.AnimeS.Relation
            {
                AnimeId = anime.MalId,
                Anime = anime,
                relation = r.relation,
                entry = item.MapList(r.entry, e => new Entities.AnimeS.Entry { mal_id = e.mal_id, type = e.type, name = e.name, url = e.url })
            });
            anime.streaming = item.MapList(item.streaming, s => new Entities.AnimeS.Streaming { AnimeId = anime.MalId, Anime = anime, name = s.name, url = s.url });
        }

        private static void MapMangaSpecific(Manga manga, MediaDto item)
        {
            manga.Chapters = item.chapters;
            manga.Volumes = item.volumes;
            manga.Publishing = item.publishing;
            manga.PublishedFrom = item.published?.from;
            manga.PublishedTo = item.published?.to;

            manga.Authors = item.MapList(item.authors, a => new Entities.MangaS.Author { MangaId = manga.MalId, Manga = manga, mal_id = a.mal_id, type = a.type, name = a.name, url = a.url });
            manga.Serializations = item.MapList(item.serializations, s => new Entities.MangaS.Serialization { MangaId = manga.MalId, Manga = manga, mal_id = s.mal_id, type = s.type, name = s.name, url = s.url });
        }

        private static List<AnimeThemeSong> MapThemeSongs(MediaDto item, Anime anime)
        {
            var songs = new List<AnimeThemeSong>();

            if (item.theme2?.openings != null)
                songs.AddRange(item.theme2.openings.Select(o => new AnimeThemeSong { AnimeId = anime.MalId, Anime = anime, type = "opening", url = o }));

            if (item.theme2?.endings != null)
                songs.AddRange(item.theme2.endings.Select(e => new AnimeThemeSong { AnimeId = anime.MalId, Anime = anime, type = "ending", url = e }));

            return songs;
        }
    }
}
