<template>
  <section class="catalog" id="catalog">
    <div class="card-wrapper">
      <div class="card-item" v-for="anime in animes" :key="anime.Id">
        <router-link :to="`/Anime/AnimeOnPage/${anime.Id}`" class="custom-link">
          <img :src="anime.WebpLargeImageUrl" :alt="anime.canonicalTitle" />
          <div class="card-content">
            <h5>{{ anime.canonicalTitle }}</h5>
          </div>
        </router-link>
      </div>
    </div>
  </section>
</template>

<script setup>
defineProps({
  animes: {
    type: Array,
    required: true
  }
})
</script>

<style scoped>
/* Você pode reaproveitar os estilos de homeStyles.css e site.css */
</style>
