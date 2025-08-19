using BusinessLogicalLayer.ApiConsumer.MangaApi;
using Entities;
using Entities.AnimeS;

namespace BusinessLogicalLayer.ApiConsumer.Converter
{
    public class AnimeConverter
    {
        public static Anime ConvertDTOToAnime(MediaDto item)
        {
            if (item == null) return null;

            var anime = new Anime
            {
                #region Anime Properties
                Source = item.source,
                Episodes = item.episodes,
                Airing = item.airing,
                Duration = item.duration,
                Rating = item.rating,
                Season = item.season,
                #endregion

                #region MediaBase Properties
                Id = Convert.ToInt32(item.mal_id),
                MalId = Convert.ToInt32(item.mal_id),
                Title = item.title,
                TitleEnglish = item.title_english,
                TitleJapanese = item.title_japanese,
                Url = item.url,
                Approved = item.approved,
                Synopsis = item.synopsis,
                Background = item.background,
                Score = item.score,
                ScoredBy = item.scored_by,
                Rank = item.rank,
                Popularity = item.popularity,
                Members = item.members,
                Favorites = item.favorites,
                Status = item.status,
                From = item.aired?.from,
                To = item.aired?.to,

                JpgImageUrl = item.GetImageUrl("jpg", "default"),
                JpgSmallImageUrl = item.GetImageUrl( "jpg", "small"),
                JpgLargeImageUrl = item.GetImageUrl("jpg", "large"),
                WebpImageUrl = item.GetImageUrl("webp", "default"),
                WebpSmallImageUrl = item.GetImageUrl("webp", "small"),
                WebpLargeImageUrl = item.GetImageUrl("webp", "large"),

                Themename = item.theme?.name,
                Themetype = item.theme?.type,
                Themeurl = item.theme?.url,

                Youtubeembed_url = item.trailer?.embed_url,
                Youtubeurl = item.trailer?.url,
                Youtube_id = item.trailer?.youtube_id,

                BroadcastDay = item.broadcast?.day,
                BroadcastTime = item.broadcast?.time,
                BroadcastTimezone = item.broadcast?.timezone,
                #endregion
            };

            #region Anime Collections
            anime.ThemeSongs = MapThemeSongs(item, anime);
            anime.external = item.MapList(item.external, e => new Entities.AnimeS.External
            {
                AnimeId = anime.MalId,
                Anime = anime,
                name = e.name,
                url = e.url
            });
            anime.studios = item.MapList(item.studios, s => new Entities.AnimeS.Studio
            {
                AnimeId = anime.MalId,
                Anime = anime,
                mal_id = s.mal_id,
                type = s.type,
                name = s.name,
                url = s.url
            });
            anime.licensors = item.MapList(item.licensors, l => new Entities.AnimeS.Licensor
            {
                AnimeId = anime.MalId,
                Anime = anime,
                mal_id = l.mal_id,
                type = l.type,
                name = l.name,
                url = l.url
            });
            anime.producers = item.MapList(item.producers, p => new Entities.AnimeS.Producer
            {
                AnimeId = anime.MalId,
                Anime = anime,
                mal_id = p.mal_id,
                type = p.type,
                name = p.name,
                url = p.url
            });
            anime.relations = item.MapList(item.relations, r => new Entities.AnimeS.Relation
            {
                AnimeId = anime.MalId,
                Anime = anime,
                relation = r.relation,
                entry = item.MapList(r.entry, e => new Entities.AnimeS.Entry
                {
                    mal_id = e.mal_id,
                    type = e.type,
                    name = e.name,
                    url = e.url
                })
            });
            anime.streaming = item.MapList(item.streaming, s => new Entities.AnimeS.Streaming
            {
                AnimeId = anime.MalId,
                Anime = anime,
                name = s.name,
                url = s.url
            });
            #endregion

            #region MediaBase Collections
            anime.Themes = item.MapList(item.themes, t => new MediaBase.Theme
            {
                AnimeId = anime.MalId,
                Anime = anime,
                MalId = t.mal_id,
                Type = t.type,
                Name = t.name,
                Url = t.url
            });
            anime.Genres = item.MapList(item.genres, g => new MediaBase.Genre
            {
                AnimeId = anime.MalId,
                Anime = anime,
                MalId = g.mal_id,
                Type = g.type,
                Name = g.name
            });
            anime.ExplicitGenres = item.MapList(item.explicit_genres, e => new MediaBase.ExplicitGenre
            {
                AnimeId = anime.MalId,
                Anime = anime,
                MalId = e.mal_id,
                Type = e.type,
                Name = e.name
            });
            anime.Demographics = item.MapList(item.demographics, d => new MediaBase.Demographic
            {
                AnimeId = anime.MalId,
                Anime = anime,
                MalId = d.mal_id,
                Type = d.type,
                Name = d.name
            });
            #endregion

            return anime;
        }
        private static List<AnimeThemeSong> MapThemeSongs(dynamic item, Anime anime)
        {
            var songs = new List<AnimeThemeSong>();

            if (item.theme2?.openings != null)
                songs.AddRange(((IEnumerable<string>)item.theme2.openings).Select(o => new AnimeThemeSong
                {
                    AnimeId = anime.MalId,
                    Anime = anime,
                    type = "opening",
                    url = o
                }));

            if (item.theme2?.endings != null)
                songs.AddRange(((IEnumerable<string>)item.theme2.endings).Select(e => new AnimeThemeSong
                {
                    AnimeId = anime.MalId,
                    Anime = anime,
                    type = "ending",
                    url = e
                }));

            return songs;
        }
    }
}
