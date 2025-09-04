<template>
  <div class="mangahome-div">
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
            :to="`/MangaOnPage/${item.id}`"
          >
            <img :src="item.webpLargeImageUrl" :alt="item.canonicalTitle" />
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
import { MangaService } from "../../services/MangaService";

export default {
  name: "MangaHome",
  data() {
    return {
      mangasFavorites: [],
      mangasByCount: [],
      mangasByRating: [],
      token: localStorage.getItem("token") || ""
    };
  },
  computed: {
    sections() {
      return [
        {
          title: "All Time Favorites",
          viewAllLink: "/manga/favorites",
          items: this.mangasFavorites
        },
        {
          title: "Most Popular",
          viewAllLink: "/manga/popularity",
          items: this.mangasByCount
        },
        {
          title: "Rating",
          viewAllLink: "/manga/score",
          items: this.mangasByRating
        }
      ];
    }
  },
  async mounted() {
    const mangaService = new MangaService(this.token);

    const favorites = await mangaService.getByFavorites();
    const count = await mangaService.getByUserCount();
    const rating = await mangaService.getByRating();

    this.mangasFavorites = favorites.data;
    this.mangasByCount = count.data;
    this.mangasByRating = rating.data;
  }
};
</script>

<style scoped>
@import "/css/site.css";
</style>
