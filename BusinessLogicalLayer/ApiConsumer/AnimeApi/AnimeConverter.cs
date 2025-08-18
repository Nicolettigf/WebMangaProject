using Entities.AnimeS;
using Entities.Enums;

namespace BusinessLogicalLayer.ApiConsumer.NovaPasta
{
    public class AnimeConverter
    {
        public static Anime ConvertDTOToAnime(RootAni animeRootDTO)
        {
            var item = animeRootDTO.data;

            if (item == null)
                return null;

            // Criando Anime
            Anime anime = new()
            {
                #region Anime
                Source = item.source,
                Episodes = item.episodes,
                Airing = item.airing,
                Duration = item.duration,
                Rating = item.rating,
                Season = item.season,
                #endregion

                #region mediabase
                Id = Convert.ToInt32(item.mal_id),
                Synopsis = item.synopsis,
                MalId = Convert.ToInt32(item.mal_id),
                Url = item.url,
                Approved = item.approved,
                TitleEnglish = item.title_english,
                TitleJapanese = item.title_japanese,
                Type = item.type,
                Score = item.score,
                Status = item.status,
                Title = item.title,
                ScoredBy = item.scored_by,
                Rank = item.rank,
                Popularity = item.popularity,
                Members = item.members,
                Favorites = item.favorites,
                Background = item.background,
                From = item.aired?.from,
                To = item.aired?.to,
                JpgImageUrl = item.images.jpg?.image_url,
                JpgSmallImageUrl = item.images.jpg?.small_image_url,
                JpgLargeImageUrl = item.images.jpg?.large_image_url,
                WebpImageUrl = item.images.webp?.image_url,
                WebpSmallImageUrl = item.images.webp?.small_image_url,
                WebpLargeImageUrl = item.images.webp?.large_image_url,

                #endregion

                //theme
                Themename = item.theme?.name,
                Themetype = item.theme?.type,
                Themeurl = item.theme?.url,

                //Youtube
                Youtubeembed_url = item.trailer?.embed_url,
                 Youtubeurl = item.trailer?.url,
                 Youtube_id = item.trailer?.youtube_id,

                // Broadcast
                BroadcastDay = item.broadcast?.day,
                BroadcastTime = item.broadcast?.time,
                BroadcastTimezone = item.broadcast?.timezone,
            };

            #region Anime Collections

            anime.ThemeSongs = new List<AnimeThemeSong>();
            if (item.theme2 != null)
            {
                if (item.theme2.openings != null)
                {
                    anime.ThemeSongs.AddRange(
                        item.theme2.openings.Select(o => new AnimeThemeSong
                        {
                            AnimeId = Convert.ToInt32(item.mal_id),
                            Anime = anime,
                            type = "opening",
                            url = o,
                        })
                    );
                }

                if (item.theme2.endings != null)
                {
                    anime.ThemeSongs.AddRange(
                        item.theme2.endings.Select(e => new AnimeThemeSong
                        {
                            AnimeId = Convert.ToInt32(item.mal_id),
                            Anime = anime,
                            type = "ending",
                            url = e
                        })
                    );
                }
            }



            anime.external = item.external?.Select(e => new Entities.AnimeS.External
            {
                AnimeId = anime.MalId,
                Anime = anime,
                name = e.name, 
                url = e.url
            }).ToList() ?? new List<Entities.AnimeS.External>();

            // Studios
            anime.studios = item.studios?.Select(s => new Entities.AnimeS.Studio
            {
                //Id = Convert.ToInt32(s.mal_id),
                AnimeId = anime.MalId,
                Anime = anime,
                mal_id = s.mal_id,
                type = s.type,
                name = s.name,
                url = s.url
            }).ToList() ?? new List<Entities.AnimeS.Studio>();

            // Licensors
            anime.licensors = item.licensors?.Select(l => new Entities.AnimeS.Licensor
            {
                //Id = Convert.ToInt32(l.mal_id),
                AnimeId = anime.MalId,
                Anime = anime,
                mal_id = l.mal_id,
                type = l.type,
                name = l.name,
                url = l.url
            }).ToList() ?? new List<Entities.AnimeS.Licensor>();

            // Producers
            anime.producers = item.producers?.Select(p => new Entities.AnimeS.Producer
            {
                //Id = Convert.ToInt32(p.mal_id),
                AnimeId = anime.MalId,
                Anime = anime,
                mal_id = p.mal_id,
                type = p.type,
                name = p.name,
                url = p.url
            }).ToList() ?? new List<Entities.AnimeS.Producer>();

            // Relations
            anime.relations = item.relations?.Select(r => new Entities.AnimeS.Relation
            {
                //Id = Convert.ToInt32(item.mal_id),
                AnimeId = anime.MalId,
                Anime = anime,
                relation = r.relation,
                entry = r.entry?.Select(e => new Entities.AnimeS.Entry
                {
                    mal_id = e.mal_id,
                    type = e.type,
                    name = e.name,
                    url = e.url
                }).ToList()
            }).ToList() ?? new List<Entities.AnimeS.Relation>();

            // Streaming
            anime.streaming = item.streaming?.Select(s => new Entities.AnimeS.Streaming
            {
                AnimeId = anime.MalId,
                Anime = anime,
                name = s.name,
                url = s.url
            }).ToList() ?? new List<Entities.AnimeS.Streaming>();

            #endregion

            #region MediaBase Collections

            // Themes
            anime.Themes = item.themes?.Select(t => new Entities.MediaBase.Theme
            {
                //Id = Convert.ToInt32(t.mal_id),
                AnimeId = anime.MalId,
                Anime = anime,
                MalId = t.mal_id,
                Type = t.type,
                Name = t.name,
                Url = t.url
            }).ToList() ?? new List<Entities.MediaBase.Theme>();

           

            anime.ExplicitGenres = item.explicit_genres?.Select(e => new Entities.MediaBase.ExplicitGenre
            {
                //Id = Convert.ToInt32(e.mal_id),
                AnimeId = anime.MalId,
                Anime = anime,
                MalId = e.mal_id,
                Type = e.type,
                Name = e.name
            }).ToList() ?? new List<Entities.MediaBase.ExplicitGenre>();

            anime.Demographics = item.demographics?.Select(d => new Entities.MediaBase.Demographic
            {
                //Id = Convert.ToInt32(d.mal_id),
                AnimeId = anime.MalId,
                Anime = anime,
                MalId = d.mal_id,
                Type = d.type,
                Name = d.name
            }).ToList() ?? new List<Entities.MediaBase.Demographic>();

            anime.Genres = item.genres?.Select(g => new Entities.MediaBase.Genre
            {
                Id = g.mal_id,
                AnimeId = anime.MalId,
                Anime = anime,
                MalId = g.mal_id,
                Type = g.type,
                Name = g.name
            }).ToList() ?? new List<Entities.MediaBase.Genre>();
            #endregion

            return anime;
        }
    }
}
