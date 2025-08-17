using Entities.AnimeS;
using Entities.Enums;

namespace BusinessLogicalLayer.ApiConsumer.NovaPasta
{
    public class AnimeConverter
    {
        public static Anime ConvertDTOToAnime(RootAni animeRootDTO)
        {
            var item = animeRootDTO.data;

            // Títulos
            AnimeSTitles titles = new()
            {
                En_jp = item.title_english,
                Ja_jp = item.title_japanese,
            };

            // Rating
            AnimeRatingFrequencies rating = new()
            {
                Id = Convert.ToInt32(item.mal_id)
            };

            // Broadcast
            Entities.AnimeS.Broadcast? broadcast = null;
            if (item.broadcast != null)
            {
                broadcast = new Entities.AnimeS.Broadcast
                {
                    Day = item.broadcast.day,
                    Time = item.broadcast.time,
                    Timezone = item.broadcast.timezone,
                    AnimeId = Convert.ToInt32(item.mal_id) // FK
                };
            }

            // Images
            Entities.AnimeS.Images? images = null;
            if (item.images != null)
            {
                images = new Entities.AnimeS.Images
                {
                    AnimeId = Convert.ToInt32(item.mal_id),
                    Jpg = new Entities.AnimeS.Jpg
                    {
                        ImageUrl = item.images.jpg.image_url,
                        SmallImageUrl = item.images.jpg.small_image_url,
                        LargeImageUrl = item.images.jpg.large_image_url
                    },
                    Webp = new Entities.AnimeS.Webp
                    {
                        ImageUrl = item.images.webp.image_url,
                        SmallImageUrl = item.images.webp.small_image_url,
                        LargeImageUrl = item.images.webp.large_image_url
                    }
                };
            }
            // Studios
            List<Entities.AnimeS.Studio> studios = new();
            if (item.studios != null)
            {
                studios = item.studios.Select(s => new Entities.AnimeS.Studio
                {
                    AnimeId = Convert.ToInt32(item.mal_id),
                    mal_id = s.mal_id,
                    type = s.type,
                    name = s.name,
                    url = s.url
                }).ToList();
            }

            // Themes
            List<Entities.AnimeS.Theme> themes = new();
            if (item.themes != null)
            {
                themes = item.themes.Select(t => new Entities.AnimeS.Theme
                {
                    AnimeId = Convert.ToInt32(item.mal_id),
                    mal_id = t.mal_id,
                    type = t.type,
                    name = t.name,
                    url = t.url
                }).ToList();
            }

            // Licensors
            List<Entities.AnimeS.Licensor> licensors = new();
            if (item.licensors != null)
            {
                licensors = item.licensors.Select(l => new Entities.AnimeS.Licensor
                {
                    AnimeId = Convert.ToInt32(item.mal_id),
                    mal_id = l.mal_id,
                    type = l.type,
                    name = l.name,
                    url = l.url
                }).ToList();
            }

            // Producers
            List<Entities.AnimeS.Producer> producers = new();
            if (item.producers != null)
            {
                producers = item.producers.Select(p => new Entities.AnimeS.Producer
                {
                    AnimeId = Convert.ToInt32(item.mal_id),
                    mal_id = p.mal_id,
                    type = p.type,
                    name = p.name,
                    url = p.url
                }).ToList();
            }

            // Relations
            List<Entities.AnimeS.Relation> relations = new();
            if (item.relations != null)
            {
                relations = item.relations.Select(r => new Entities.AnimeS.Relation
                {
                    AnimeId = Convert.ToInt32(item.mal_id),
                    relation = r.relation,
                    entry = r.entry?.Select(e => new Entities.AnimeS.Entry
                    {
                        mal_id = e.mal_id,
                        type = e.type,
                        name = e.name,
                        url = e.url
                    }).ToList()
                }).ToList();
            }
            // Streaming
            List<Entities.AnimeS.Streaming> streaming = new();
            if (item.streaming != null)
            {
                streaming = item.streaming.Select(s => new Entities.AnimeS.Streaming
                {
                    AnimeId = Convert.ToInt32(item.mal_id),
                    name = s.name,
                    url = s.url
                }).ToList();
            }

            


            // Criando Anime
            Anime anime = new()
            {
                Id = Convert.ToInt32(item.mal_id),
                name = item.title,
                description = item.background,
                synopsis = item.synopsis,
                AnimeTitles = titles,
                canonicalTitle = item.title,
                averageRating = item.score?.ToString(),
                AnimeRatingFrequencies = rating,
                userCount = item.members,
                favoritesCount = item.favorites,
                popularityRank = item.popularity,
                startDate = item.aired?.from,
                endDate = item.aired?.to,
                ratingRank = item.rank,
                ageRating = item.rating,
                ageRatingGuide = item.rating,
                subtype = item.type,
                AnimePosterImage = item.images?.jpg?.large_image_url,
                AnimeCoverImage = item.trailer?.embed_url,
                episodeCount = item.episodes,
                episodeLength = item.duration,
                youtubeVideoId = item.trailer?.youtube_id,
                showType = item.type,
                status = item.status,
                broadcast = broadcast,
                images = images

                
            };
            // Finalmente atribuindo ao Anime
            anime.studios = studios;
            anime.themes = themes;
            anime.licensors = licensors;
            anime.producers = producers;
            anime.relations = relations;
            anime.streaming = streaming;

            return anime;
        }
    }
}
