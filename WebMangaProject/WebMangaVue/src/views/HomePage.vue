<template>
  <div class="homepage-div">
    <div class="alh-div">
      <div
        v-for="section in sections"
        :key="section.title"
        class="catalog alh-grid"
      >
        <div class="idiv-config">
          <h3 class="section-title">{{ section.title }}</h3>
          <div class="va-div">
            <router-link class="custom-link va-width" :to="section.viewAllLink">
              View All
            </router-link>
          </div>
        </div>
        <hr />
        <div class="card-wrapper">
          <router-link
            v-for="item in section.items"
            :key="item.id"
            class="card-item custom-link"
            :to="section.type === 'anime' ? `/AnimeOnPage/${item.id}` : `/MangaOnPage/${item.id}`"
          > 
              <img :src="item.posterImageLarge || item.webpLargeImageUrl" />
              <div class="card-content">
                <h5>{{ item.canonicalTitle }}</h5>
              </div>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { HomeService } from "../services/HomeService";

export default {
  name: "HomePage",
  data() {
    return {
      mangasFavorites: [],
      mangasByCount: [],
      animesFavorites: [],
      animesByCount: [],
      animeByRank : [],
      mangaByRank : [],
      latestAnimes : [],
      token: localStorage.getItem("token") || ""
    };
  },
  computed: {
    sections() {
      return [
        {
          title: "All Time Favorite Manga",
          type: "manga",
          viewAllLink: "/manga/favorites",
          items: this.mangasFavorites
        },
        {
          title: "Most Popular Manga",
          type: "manga",
          viewAllLink: "/manga/popularity",
          items: this.mangasByCount
        },
        {
          title: "All Time By Score Mangas",
          type: "manga",
          viewAllLink: "/manga/score",
          items: this.mangaByRank
        },
        {
          title: "All Time Favorite Animes",
          type: "anime",
          viewAllLink: "/anime/favorites",
          items: this.animesFavorites
        },
        {
          title: "Most Popular Animes",
          type: "anime",
          viewAllLink: "/anime/popularity",
          items: this.animesByCount
        },
        {
          title: "All Time By Score Animes",
          type: "anime",
          viewAllLink: "/anime/score",
          items: this.animeByRank
        },
        {
          title: "Latest Animes",
          type: "anime",
          viewAllLink: "/anime/usercount", // se quiser "latest" mesmo, precisa criar no router também
          items: this.latestAnimes
        }
      ];
    }
  },
  async mounted() {
    const homeService = new HomeService(this.token);
    const response = await homeService.getHomePageData(0, 7);

    if (response.hasSuccess) {
      this.mangasFavorites = response.item.topMangaByFavorites;
      this.mangasByCount = response.item.topMangaByMembers;
      this.animesFavorites = response.item.topAnimeByFavorites;
      this.animesByCount = response.item.topAnimeByMembers;
      this.animeByRank = response.item.topAnimeByRank;
      this.mangaByRank = response.item.topMangaByRank;
      this.latestAnimes = response.item.latestAnimes;

      console.log("Mangas favoritos:", this.mangasFavorites);
      console.log("Animes favoritos:", this.animesFavorites);
    }
  }
};
</script>

<style scoped>
@import "/css/site.css";

.homepage-div {
    width: 100%;
    display: flex;
    justify-content: center;
}
</style>
