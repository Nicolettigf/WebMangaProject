public static class Procedures
{
    public const string GetTopAnimeManga = @"
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetTopAnimeManga]') AND type = 'P')
    BEGIN
        EXEC('
        CREATE PROCEDURE [dbo].[sp_GetTopAnimeManga]
            @Skip INT,
            @Take INT
        AS
        BEGIN
            SET NOCOUNT ON;

            -- AnimeByRank
            SELECT Id, Title AS canonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''AnimeByRank'' AS ListType
            FROM Anime
            WHERE Rank IS NOT NULL AND Rank > 0
            ORDER BY Rank ASC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            -- AnimeByMembers
            SELECT Id, Title AS canonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''AnimeByMembers'' AS ListType
            FROM Anime
            ORDER BY Members DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            -- AnimeByFavorites
            SELECT Id, Title AS canonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''AnimeByFavorites'' AS ListType
            FROM Anime
            ORDER BY Favorites DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            -- MangaByRank
            SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''MangaByRank'' AS ListType
            FROM Mangas
            WHERE Rank IS NOT NULL AND Rank > 0
            ORDER BY Rank ASC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            -- MangaByMembers
            SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''MangaByMembers'' AS ListType
            FROM Mangas
            ORDER BY Members DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            -- MangaByFavorites
            SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''MangaByFavorites'' AS ListType
            FROM Mangas
            ORDER BY Favorites DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            -- AnimeLastAdded
            SELECT Id, Title AS canonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''AnimeLastAdded'' AS ListType
            FROM Anime
            ORDER BY Id DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
        END
        ')
    END
    ";
}
