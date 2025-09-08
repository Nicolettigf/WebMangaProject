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

    public const string GetHomeMedia = @"
        IF NOT EXISTS (SELECT * FROM sys.objects 
                       WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetHomeMedia]') 
                       AND type = 'P')
        BEGIN
            EXEC('
            CREATE PROCEDURE [dbo].[sp_GetHomeMedia]
                @TableName NVARCHAR(50),
                @Skip INT,
                @Take INT
            AS
            BEGIN
        SET NOCOUNT ON;

        DECLARE @sql NVARCHAR(MAX);

        SET @sql = ''
        -- Top by Rank
        SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''''TopByRank'''' AS ListType
        FROM '' + QUOTENAME(@TableName) + '' 
        WHERE Rank IS NOT NULL AND Rank > 0
        ORDER BY Rank ASC
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

        -- Top by Members
        SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''''TopByMembers'''' AS ListType
        FROM '' + QUOTENAME(@TableName) + '' 
        ORDER BY Members DESC
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

        -- Top by Favorites
        SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''''TopByFavorites'''' AS ListType
        FROM '' + QUOTENAME(@TableName) + '' 
        ORDER BY Favorites DESC
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

        -- Top by Score
        SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''''TopByScore'''' AS ListType
        FROM '' + QUOTENAME(@TableName) + '' 
        WHERE Score IS NOT NULL AND Score != 0
        ORDER BY Score DESC
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

        -- Top by ScoredBy
        SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''''TopByScoredBy'''' AS ListType
        FROM '' + QUOTENAME(@TableName) + '' 
        WHERE ScoredBy IS NOT NULL
        ORDER BY ScoredBy DESC
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

        -- Top by Popularity
        SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''''TopByPopularity'''' AS ListType
        FROM '' + QUOTENAME(@TableName) + '' 
        WHERE Popularity IS NOT NULL AND Popularity != 0
        ORDER BY Popularity
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

        -- Latest by Id
        SELECT Id, Title AS CanonicalTitle, WebpLargeImageUrl, CoverImageLarge, PosterImageLarge, ''''Latest'''' AS ListType
        FROM '' + QUOTENAME(@TableName) + '' 
        ORDER BY Id DESC
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
        '';

        EXEC sp_executesql @sql, N''@Skip INT, @Take INT'', @Skip=@Skip, @Take=@Take;
    END
    ')
END
";


}
