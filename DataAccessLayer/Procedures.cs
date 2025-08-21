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

            SELECT Id, Title AS canonicalTitle, WebpLargeImageUrl, ''AnimeByRank'' AS ListType
            FROM Anime
            ORDER BY Rank DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            SELECT Id, Title AS canonicalTitle, WebpLargeImageUrl, ''AnimeByMembers'' AS ListType
            FROM Anime
            ORDER BY Members DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            SELECT Id, Title AS canonicalTitle, WebpLargeImageUrl, ''AnimeByFavorites'' AS ListType
            FROM Anime
            ORDER BY Favorites DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, ''MangaByRank'' AS ListType
            FROM Mangas
            ORDER BY Rank DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, ''MangaByMembers'' AS ListType
            FROM Mangas
            ORDER BY Members DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, ''MangaByFavorites'' AS ListType
            FROM Mangas
            ORDER BY Favorites DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            SELECT Id, Title AS canonicalTitle, WebpLargeImageUrl, ''AnimeLastAdded'' AS ListType
            FROM Anime
            ORDER BY Id DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
        END
        ')
    END
    ";
}
