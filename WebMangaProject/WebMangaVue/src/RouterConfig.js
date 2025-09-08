import { createRouter, createWebHistory } from "vue-router";

// Layout
import MainLayout from "./views/Shared/MainLayout.vue";

//Shared
import AboutUs from "./views/AboutUs.vue";
import ErrorPage from "./views/Shared/ErrorPage.vue";

//Home
import HomePage from "./views/HomePage.vue";
import Catalog from "./views/Shared/Catalog.vue"
import CatalogHome from "./views/Shared/CatalogHome.vue"

// Anime
import AnimeOnPage from "./views/anime/AnimeOnPage.vue";

// Manga
import MangaOnPage from "./views/manga/MangaOnPage.vue";


// import GetSuggestionList from "../views/manga/GetSuggestionList.vue";

// // MangaDb
// import MangaDbIndex from "../views/mangadb/MangaDbIndex.vue";
// import MangaDbDetails from "../views/mangadb/MangaDbDetails.vue";
// import MangaDbCreate from "../views/mangadb/MangaDbCreate.vue";
// import MangaDbEdit from "../views/mangadb/MangaDbEdit.vue";
// import MangaDbDelete from "../views/mangadb/MangaDbDelete.vue";

// User
// import UserIndex from "../views/user/UserIndex.vue";
// import Login from "./views/user/UserLogin.vue";
import CreateUser from "./views/user/CreateUser.vue";
import EditUser from "./views/user/EditUser.vue";
//import DeleteUser from "./views/user/DeleteUser.vue";

// // AnimeComentary
// import InsertAnimeComentary from "../views/animecomentary/InsertAnimeComentary.vue";
// import UpdateAnimeComentary from "../views/animecomentary/UpdateAnimeComentary.vue";
// import GetAnimeComentary from "../views/animecomentary/GetAnimeComentary.vue";
// import DeleteAnimeComentary from "../views/animecomentary/DeleteAnimeComentary.vue";
// import GetAnimeComentaryByUser from "../views/animecomentary/GetAnimeComentaryByUser.vue";

// // MangaComentary
// import InsertMangaComentary from "../views/mangacomentary/InsertMangaComentary.vue";
// import UpdateMangaComentary from "../views/mangacomentary/UpdateMangaComentary.vue";
// import GetMangaComentary from "../views/mangacomentary/GetMangaComentary.vue";
// import DeleteMangaComentary from "../views/mangacomentary/DeleteMangaComentary.vue";
// import GetMangaComentaryByUser from "../views/mangacomentary/GetMangaComentaryByUser.vue";

// // UserItemAnime
// import InsertUserItemAnime from "../views/useritem/InsertUserItemAnime.vue";
// import UpdateUserItemAnime from "../views/useritem/UpdateUserItemAnime.vue";
// import GetUserItemAnime from "../views/useritem/GetUserItemAnime.vue";
// import DeleteUserItemAnime from "../views/useritem/DeleteUserItemAnime.vue";
// import GetUserItemAnimeByUser from "../views/useritem/GetUserItemAnimeByUser.vue";

// // UserItemManga
// import InsertUserItemManga from "../views/useritem/InsertUserItemManga.vue";
// import UpdateUserItemManga from "../views/useritem/UpdateUserItemManga.vue";
// import GetUserItemManga from "../views/useritem/GetUserItemManga.vue";
// import DeleteUserItemManga from "../views/useritem/DeleteUserItemManga.vue";
// import GetUserItemMangaByUser from "../views/useritem/GetUserItemMangaByUser.vue";

const routes = [
  {
    path: "/",
    component: MainLayout, // seu layout padrão
    children: [
      // HomeController
      { path: "", name: "HomePage", component: HomePage },
      { path: "about", name: "AboutUs", component: AboutUs },
      { path: "error", name: "Error", component: ErrorPage },

      // CatalogHome unificado
      { path: "anime/Home", name: "AnimeHome", component: CatalogHome, props: { entityType: "anime" } },
      { path: "manga/Home", name: "MangaHome", component: CatalogHome, props: { entityType: "manga" } },
      { path: '/search',    name: 'SearchPage',component: () => import('../src/views/SearchPage.vue') },

      // AnimeController
      { path: "anime/all", name: "AnimeAll", component: Catalog, props: { entityType: "anime", type: "all" } },
      { path: "anime/favorites", name: "AnimeAllByFavorites", component: Catalog, props: { entityType: "anime", type: "favorites" } },
      { path: "anime/popularity", name: "AnimeAllByPopularity", component: Catalog, props: { entityType: "anime", type: "popularity" } },
      { path: "anime/score", name: "AnimeAllByRating", component: Catalog, props: { entityType: "anime", type: "score" } },
      { path: "anime/usercount", name: "AnimeAllByUserCount", component: Catalog, props: { entityType: "anime", type: "usercount" } },
      { path: "animeOnPage/:id", name: "AnimeOnPage", component: AnimeOnPage },
      


      // MangaController
      { path: "manga/all", name: "MangaAll", component: Catalog, props: { entityType: "manga", type: "all" } },
      { path: "manga/favorites", name: "MangaAllByFavorites", component: Catalog, props: { entityType: "manga", type: "favorites" } },
      { path: "manga/popularity", name: "MangaAllByPopularity", component: Catalog, props: { entityType: "manga", type: "popularity" } },
      { path: "manga/score", name: "MangaAllByScore", component: Catalog, props: { entityType: "manga", type: "score" } },
      { path: "manga/usercount", name: "MangaAllByUserCount", component: Catalog, props: { entityType: "manga", type: "usercount" } },
      { path: "mangaOnPage/:id", name: "MangaOnPage", component: MangaOnPage }, // sempre por último


      // // MangaDbController
      // { path: "mangadb", name: "MangaDbIndex", component: MangaDbIndex },
      // { path: "mangadb/details/:id", name: "MangaDbDetails", component: MangaDbDetails },
      // { path: "mangadb/create", name: "MangaDbCreate", component: MangaDbCreate },
      // { path: "mangadb/edit/:id", name: "MangaDbEdit", component: MangaDbEdit },
      // { path: "mangadb/delete/:id", name: "MangaDbDelete", component: MangaDbDelete },

      // UserController
      //{ path: "user", name: "UserIndex", component: UserIndex },
      { path: "user/create", name: "CreateUser", component: CreateUser },
      { path: "user/edit/:id", name: "EditUser", component: EditUser },
      //{ path: "user/delete/:id", name: "DeleteUser", component: DeleteUser },

      // // AnimeComentaryController
      // { path: "animecomentary/insert", name: "InsertAnimeComentary", component: InsertAnimeComentary },
      // { path: "animecomentary/update", name: "UpdateAnimeComentary", component: UpdateAnimeComentary },
      // { path: "animecomentary/get/:id", name: "GetAnimeComentary", component: GetAnimeComentary },
      // { path: "animecomentary/delete/:id", name: "DeleteAnimeComentary", component: DeleteAnimeComentary },
      // { path: "animecomentary/getbyuser/:id", name: "GetAnimeComentaryByUser", component: GetAnimeComentaryByUser },

      // // MangaComentaryController
      // { path: "mangacomentary/insert", name: "InsertMangaComentary", component: InsertMangaComentary },
      // { path: "mangacomentary/update", name: "UpdateMangaComentary", component: UpdateMangaComentary },
      // { path: "mangacomentary/get/:id", name: "GetMangaComentary", component: GetMangaComentary },
      // { path: "mangacomentary/delete/:id", name: "DeleteMangaComentary", component: DeleteMangaComentary },
      // { path: "mangacomentary/getbyuser/:id", name: "GetMangaComentaryByUser", component: GetMangaComentaryByUser },

      // // UserItemAnimeController
      // { path: "useritem/anime/insert", name: "InsertUserItemAnime", component: InsertUserItemAnime },
      // { path: "useritem/anime/update", name: "UpdateUserItemAnime", component: UpdateUserItemAnime },
      // { path: "useritem/anime/get/:id", name: "GetUserItemAnime", component: GetUserItemAnime },
      // { path: "useritem/anime/delete/:id", name: "DeleteUserItemAnime", component: DeleteUserItemAnime },
      // { path: "useritem/anime/getbyuser/:id", name: "GetUserItemAnimeByUser", component: GetUserItemAnimeByUser },

      // // UserItemMangaController
      // { path: "useritem/manga/insert", name: "InsertUserItemManga", component: InsertUserItemManga },
      // { path: "useritem/manga/update", name: "UpdateUserItemManga", component: UpdateUserItemManga },
      // { path: "useritem/manga/get/:id", name: "GetUserItemManga", component: GetUserItemManga },
      // { path: "useritem/manga/delete/:id", name: "DeleteUserItemManga", component: DeleteUserItemManga },
      // { path: "useritem/manga/getbyuser/:id", name: "GetUserItemMangaByUser", component: GetUserItemMangaByUser }
    
    ]
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    // se houver posição salva (ex: voltar com o botão do navegador), usa ela
    if (savedPosition) {
      return savedPosition
    }
    // senão, sempre vai para o topo
    return { top: 0 }
  }
})

export default router
