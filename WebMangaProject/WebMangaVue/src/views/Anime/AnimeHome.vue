<template>
  <div class="animehome-div">
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
            :to="`/AnimeOnPage/${item.id}`"
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
import { AnimeService } from "../../services/AnimeService";

export default {
  name: "AnimeHome",
  data() {
    return {
      animesFavorites: [],
      animesByCount: [],
      animesByRating: [],
      token: localStorage.getItem("token") || ""
    };
  },
  computed: {
    sections() {
      return [
        {
          title: "All Time Favorites",
          viewAllLink: "/anime/favorites",
          items: this.animesFavorites
        },
        {
          title: "Most Popular",
          viewAllLink: "/Anime/AllByUserCount",
          items: this.animesByCount
        },
        {
          title: "All Time By Score",
          viewAllLink: "/anime/score",
          items: this.animesByRating
        }
      ];
    }
  },
  async mounted() {
    const animeService = new AnimeService(this.token);

    const favorites = await animeService.getByFavorites();
    const count = await animeService.getByUserCount();
    const rating = await animeService.getByRating();

    this.animesFavorites = favorites.data;
    this.animesByCount = count.data;
    this.animesByRating = rating.data;
  }
};
</script>

<style scoped>
@import "/css/site.css";


</style>
