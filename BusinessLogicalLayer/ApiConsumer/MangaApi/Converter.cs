using Entities;
using Entities.AnimeS;
using Entities.Enums;
using Entities.MangaS;

namespace BusinessLogicalLayer.ApiConsumer.MangaApi
{
    public class ConverterToCategory
    {
        public static Manga ConvertDTOToManga(Datum item)
        {
            if (item == null) return null;

            // Criando Manga
            Manga manga = new()
            {
                #region deletar
                Id = Convert.ToInt32(item.mal_id),
                Synopsis = item.synopsis,
                Titles = new MangaTitles
                {
                    En = item.title_english,
                    En_jp = item.titles?.FirstOrDefault(t => t.type == "en_jp")?.title,
                    Ja_jp = item.title_japanese
                },
                CanonicalTitle = item.title,
                AverageRating = item.score.ToString(),
                RatingFrequencies = new RatingFrequencies { Id = Convert.ToInt32(item.mal_id) },
                RatingRank = item.rank,
                PopularityRank = item.popularity,
                UserCount = item.members,
                FavoritesCount = item.favorites,
                StartDate = item.published?.from.ToString(),
                EndDate = item.published?.to?.ToString(),
                Status = item.status,
                VolumeCount = item.volumes,
                Serialization = item.serializations?.FirstOrDefault()?.name,
                PosterImageLink = item.images?.jpg?.image_url,
                CoverImageLink = item.images?.jpg?.large_image_url,
                Subtype = item.type,
                ChapterCount = item.chapters,
                #endregion

                Chapters = item.chapters,
                Volumes = item.volumes,
                Publishing = item.publishing,
                PublishedFrom = item.published?.from != null ? DateTime.Parse(item.published.from.ToString()) : DateTime.MinValue,
                PublishedTo = item.published?.to != null ? DateTime.Parse(item.published.to.ToString()) : (DateTime?)null
            };

            #region mangaReference
                // Authors
                manga.Authors = item.authors?.Select(a => new Author
                {
                    MangaId = manga.MalId,
                    Manga = manga,
                    mal_id = a.mal_id,
                    type = a.type,
                    name = a.name,
                    url = a.url
                }).ToList() ?? new List<Author>();

                // Serializations
                manga.Serializations = item.serializations?.Select(s => new Serialization
                {
                    MangaId = manga.MalId,
                    Manga = manga,
                    mal_id = s.mal_id,
                    type = s.type,
                    name = s.name,
                    url = s.url
                }).ToList() ?? new List<Serialization>();
            #endregion

            #region MediaBasereference

                // Themes
                manga.Themes = item.themes?.Select(t => new Entities.MediaBase.Theme
                {
                    MangaId = manga.MalId,
                    Manga = manga,
                    MalId = t.mal_id,
                    type = t.type,
                    name = t.name,
                    url = t.url
                }).ToList() ?? new List<Entities.MediaBase.Theme>();

                // Imagens herdadas do MediaBase
                manga.Imagens = item.images != null
                    ? new Entities.MediaBase.Images
                    {
                        MangaId = manga.MalId,
                        Manga = manga,
                        JpgImageUrl = item.images.jpg?.image_url,
                        JpgSmallImageUrl = item.images.jpg?.small_image_url,
                        JpgLargeImageUrl = item.images.jpg?.large_image_url,
                        WebpImageUrl = item.images.webp?.image_url,
                        WebpSmallImageUrl = item.images.webp?.small_image_url,
                        WebpLargeImageUrl = item.images.webp?.large_image_url
                    }
                    : null;

                manga.ExplicitGenres = item.explicit_genres?.Select(e => new Entities.MediaBase.ExplicitGenre
                {
                    MangaId = manga.MalId,
                    Manga = manga,
                    MalId = e.mal_id,
                    type = e.type,
                    name = e.name
                }).ToList() ?? new List<Entities.MediaBase.ExplicitGenre>();

                manga.Demographics = item.demographics?.Select(d => new Entities.MediaBase.Demographic
                {
                    MangaId = manga.MalId,
                    Manga = manga,
                    MalId = d.mal_id,
                    type = d.type,
                    name = d.name
                }).ToList() ?? new List<Entities.MediaBase.Demographic>();

                manga.Genres = item.genres?.Select(g => new Entities.MediaBase.Genre
                {
                    MangaId = manga.MalId,
                    Manga = manga,
                    MalId = g.mal_id,
                    type = g.type,
                    name = g.name
                }).ToList() ?? new List<Entities.MediaBase.Genre>();

            #endregion




            return manga;
        }
    }
}
