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
        </div>
        <hr />
        <div class="card-wrapper">
          <router-link
            v-for="item in section.items"
            :key="item.id"
            class="card-item custom-link"
            :to="section.type === 'anime' ? `/Anime/${item.id}` : `/Manga/${item.id}`"
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
        items: this.mangasFavorites
      },
      {
        title: "Most Popular Manga",
        type: "manga",
        items: this.mangasByCount
      },
      {
        title: "All Time By Rank Mangas",
        type: "manga",
        items: this.mangaByRank
      },
      {
        title: "All Time By Rank Animes",
        type: "anime",
        items: this.animeByRank
      },
      {
        title: "All Time Favorite Animes",
        type: "anime",
        items: this.animesFavorites
      },
      {
        title: "Most Popular Animes",
        type: "anime",
        items: this.animesByCount
      },
      {
        title: "Latest Animes",
        type: "anime",
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

.idiv-config {
    display: flex;
    justify-content: center;
    flex-direction: column;
}

.section-title {
    display: flex;
    justify-content: center;
    padding-top: 1%;
}
</style>
