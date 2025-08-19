using Entities.MangaS;

namespace BusinessLogicalLayer.ApiConsumer.Converter
{
    public class MangaConverter
    {
        public static Manga ConvertDTOToManga(MediaDto item)
        {
            if (item == null) return null;

            Manga manga = new()
            {
                Id = Convert.ToInt32(item.mal_id),
                MalId = Convert.ToInt32(item.mal_id),
                Title = item.title,
                TitleEnglish = item.title_english,
                TitleJapanese = item.title_japanese,
                Url = item.url,
                Synopsis = item.synopsis,
                Background = item.background,
                Score = item.score,
                ScoredBy = item.scored_by,
                Rank = item.rank,
                Popularity = item.popularity,
                Members = item.members,
                Type = item.type,
                Chapters = item.chapters,
                Volumes = item.volumes,
                Publishing = item.publishing,
                PublishedFrom = item.published?.from,
                PublishedTo = item.published?.to,

                JpgImageUrl =       item.GetImageUrl("jpg", "default"),
                JpgSmallImageUrl =  item.GetImageUrl("jpg", "small"),
                JpgLargeImageUrl =  item.GetImageUrl("jpg", "large"),
                WebpImageUrl =      item.GetImageUrl("webp", "default"),
                WebpSmallImageUrl = item.GetImageUrl("webp", "small"),
                WebpLargeImageUrl = item.GetImageUrl("webp", "large")
            };

            manga.Authors = item.MapList(item.authors, a => new Entities.MangaS.Author
            {
                MangaId = manga.MalId,
                Manga = manga,
                mal_id = a.mal_id,
                type = a.type,
                name = a.name,
                url = a.url
            });

            manga.Serializations = item.MapList(item.serializations, s => new Entities.MangaS.Serialization
            {
                MangaId = manga.MalId,
                Manga = manga,
                mal_id = s.mal_id,
                type = s.type,
                name = s.name,
                url = s.url
            });

            MapMediaBaseReferences(item, manga);

            return manga;
        }


        private static void MapMediaBaseReferences(MediaDto item, Manga manga)
        {
            manga.Themes = item.MapList(item.themes, t => new Entities.MediaBase.Theme
            {
                MangaId = manga.MalId,
                Manga = manga,
                MalId = t.mal_id,
                Type = t.type,
                Name = t.name,
                Url = t.url
            });

            manga.Genres = item.MapList(item.genres, g => new Entities.MediaBase.Genre
            {
                MangaId = manga.MalId,
                Manga = manga,
                MalId = g.mal_id,
                Type = g.type,
                Name = g.name
            });

            manga.ExplicitGenres = item.MapList(item.explicit_genres, e => new Entities.MediaBase.ExplicitGenre
            {
                MangaId = manga.MalId,
                Manga = manga,
                MalId = e.mal_id,
                Type = e.type,
                Name = e.name
            });

            manga.Demographics = item.MapList(item.demographics, d => new Entities.MediaBase.Demographic
            {
                MangaId = manga.MalId,
                Manga = manga,
                MalId = d.mal_id,
                Type = d.type,
                Name = d.name
            });
        }
    }
}
