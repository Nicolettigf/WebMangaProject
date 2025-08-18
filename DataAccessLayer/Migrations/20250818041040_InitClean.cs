using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class InitClean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeRatingFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    _1 = table.Column<int>(type: "int", nullable: true),
                    _2 = table.Column<int>(type: "int", nullable: true),
                    _3 = table.Column<int>(type: "int", nullable: true),
                    _4 = table.Column<int>(type: "int", nullable: true),
                    _5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeRatingFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeSTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    En_jp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ja_jp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangasRatingFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    _1 = table.Column<int>(type: "int", nullable: true),
                    _2 = table.Column<int>(type: "int", nullable: true),
                    _3 = table.Column<int>(type: "int", nullable: true),
                    _4 = table.Column<int>(type: "int", nullable: true),
                    _5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangasRatingFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    En_jp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ja_jp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FavoritesCount = table.Column<int>(type: "int", nullable: false),
                    ReviewsCount = table.Column<int>(type: "int", nullable: false),
                    AvatarImageFileLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageFileLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeepLogged = table.Column<bool>(type: "bit", nullable: false),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Anime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Episodes = table.Column<int>(type: "int", nullable: true),
                    Airing = table.Column<bool>(type: "bit", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeRatingFrequenciesId = table.Column<int>(type: "int", nullable: true),
                    AnimeTitlesId = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    canonicalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    averageRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userCount = table.Column<int>(type: "int", nullable: true),
                    favoritesCount = table.Column<int>(type: "int", nullable: true),
                    popularityRank = table.Column<int>(type: "int", nullable: true),
                    startDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ratingRank = table.Column<int>(type: "int", nullable: true),
                    ageRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ageRatingGuide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimePosterImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeCoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    episodeCount = table.Column<int>(type: "int", nullable: true),
                    episodeLength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    youtubeVideoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    showType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BroadcastDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BroadcastTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BroadcastTimezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Youtube_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Youtubeurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Youtubeembed_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Themetype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Themename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Themeurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleJapanese = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<double>(type: "float", nullable: true),
                    ScoredBy = table.Column<int>(type: "int", nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: true),
                    Popularity = table.Column<int>(type: "int", nullable: true),
                    Members = table.Column<int>(type: "int", nullable: true),
                    Favorites = table.Column<int>(type: "int", nullable: true),
                    Synopsis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anime_AnimeRatingFrequencies_AnimeRatingFrequenciesId",
                        column: x => x.AnimeRatingFrequenciesId,
                        principalTable: "AnimeRatingFrequencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Anime_AnimeSTitles_AnimeTitlesId",
                        column: x => x.AnimeTitlesId,
                        principalTable: "AnimeSTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Chapters = table.Column<int>(type: "int", nullable: true),
                    Volumes = table.Column<int>(type: "int", nullable: true),
                    Publishing = table.Column<bool>(type: "bit", nullable: false),
                    PublishedFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RatingFrequenciesId = table.Column<int>(type: "int", nullable: true),
                    CanonicalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitlesId = table.Column<int>(type: "int", nullable: true),
                    Synopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingRank = table.Column<int>(type: "int", nullable: true),
                    PopularityRank = table.Column<int>(type: "int", nullable: true),
                    UserCount = table.Column<int>(type: "int", nullable: true),
                    FavoritesCount = table.Column<int>(type: "int", nullable: true),
                    VolumeCount = table.Column<int>(type: "int", nullable: true),
                    Serialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosterImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChapterCount = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleJapanese = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<double>(type: "float", nullable: true),
                    ScoredBy = table.Column<int>(type: "int", nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: true),
                    Popularity = table.Column<int>(type: "int", nullable: true),
                    Members = table.Column<int>(type: "int", nullable: true),
                    Favorites = table.Column<int>(type: "int", nullable: true),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mangas_MangasRatingFrequencies_RatingFrequenciesId",
                        column: x => x.RatingFrequenciesId,
                        principalTable: "MangasRatingFrequencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mangas_MangaTitles_TitlesId",
                        column: x => x.TitlesId,
                        principalTable: "MangaTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnimeComentary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comentary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataComentary = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeComentary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeComentary_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeComentary_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeThemeSong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeThemeSong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeThemeSong_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "External",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_External", x => x.Id);
                    table.ForeignKey(
                        name: "FK_External_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licensor_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producer_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    relation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relation_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Streaming",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streaming", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streaming_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studio_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnimeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    TotalRewatches = table.Column<int>(type: "int", nullable: true),
                    Episode = table.Column<int>(type: "int", nullable: true),
                    PrivateNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimeItem", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Animeuser",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimeItem_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Demographic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demographic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demographic_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Demographic_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExplicitGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExplicitGenre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExplicitGenre_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExplicitGenre_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genre_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Genre_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JpgImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JpgSmallImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JpgLargeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebpImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebpSmallImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebpLargeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MangaComentary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comentary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataComentary = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaComentary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaComentary_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaComentary_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Serialization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serialization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serialization_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theme_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Theme_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserMangaItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    TotalRereads = table.Column<int>(type: "int", nullable: true),
                    Chapter = table.Column<int>(type: "int", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: true),
                    PrivateNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMangaItem", x => x.Id);
                    table.ForeignKey(
                        name: "fk_mangauser",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMangaItem_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    relationID = table.Column<int>(type: "int", nullable: true),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Entry_Relation_relationID",
                        column: x => x.relationID,
                        principalTable: "Relation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anime_AnimeRatingFrequenciesId",
                table: "Anime",
                column: "AnimeRatingFrequenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Anime_AnimeTitlesId",
                table: "Anime",
                column: "AnimeTitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeComentary_AnimeId",
                table: "AnimeComentary",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeComentary_UserId",
                table: "AnimeComentary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeThemeSong_AnimeId",
                table: "AnimeThemeSong",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_MangaId",
                table: "Author",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Demographic_AnimeId",
                table: "Demographic",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Demographic_MangaId",
                table: "Demographic",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_relationID",
                table: "Entry",
                column: "relationID");

            migrationBuilder.CreateIndex(
                name: "IX_ExplicitGenre_AnimeId",
                table: "ExplicitGenre",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExplicitGenre_MangaId",
                table: "ExplicitGenre",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_External_AnimeId",
                table: "External",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_AnimeId",
                table: "Genre",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MangaId",
                table: "Genre",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnimeId",
                table: "Images",
                column: "AnimeId",
                unique: true,
                filter: "[AnimeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MangaId",
                table: "Images",
                column: "MangaId",
                unique: true,
                filter: "[MangaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Licensor_AnimeId",
                table: "Licensor",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaComentary_MangaId",
                table: "MangaComentary",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaComentary_UserId",
                table: "MangaComentary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_RatingFrequenciesId",
                table: "Mangas",
                column: "RatingFrequenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_TitlesId",
                table: "Mangas",
                column: "TitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_Producer_AnimeId",
                table: "Producer",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Relation_AnimeId",
                table: "Relation",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Serialization_MangaId",
                table: "Serialization",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Streaming_AnimeId",
                table: "Streaming",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Studio_AnimeId",
                table: "Studio",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Theme_AnimeId",
                table: "Theme",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Theme_MangaId",
                table: "Theme",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeItem_AnimeId",
                table: "UserAnimeItem",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeItem_UserId",
                table: "UserAnimeItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMangaItem_MangaId",
                table: "UserMangaItem",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMangaItem_UserId",
                table: "UserMangaItem",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeComentary");

            migrationBuilder.DropTable(
                name: "AnimeThemeSong");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Demographic");

            migrationBuilder.DropTable(
                name: "Entry");

            migrationBuilder.DropTable(
                name: "ExplicitGenre");

            migrationBuilder.DropTable(
                name: "External");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Licensor");

            migrationBuilder.DropTable(
                name: "MangaComentary");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropTable(
                name: "Serialization");

            migrationBuilder.DropTable(
                name: "Streaming");

            migrationBuilder.DropTable(
                name: "Studio");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "UserAnimeItem");

            migrationBuilder.DropTable(
                name: "UserMangaItem");

            migrationBuilder.DropTable(
                name: "Relation");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Mangas");

            migrationBuilder.DropTable(
                name: "Anime");

            migrationBuilder.DropTable(
                name: "MangasRatingFrequencies");

            migrationBuilder.DropTable(
                name: "MangaTitles");

            migrationBuilder.DropTable(
                name: "AnimeRatingFrequencies");

            migrationBuilder.DropTable(
                name: "AnimeSTitles");
        }
    }
}
