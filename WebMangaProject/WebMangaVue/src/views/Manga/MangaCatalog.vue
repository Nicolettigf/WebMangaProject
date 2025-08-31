<template>
  <section class="catalog" id="catalog">
    <div class="card-wrapper">
      <div v-for="item in mangas" :key="item.id" class="card-item">
        <router-link class="custom-link" :to="`/Manga/MangaOnPage/${item.id}`">
          <img :src="item.webpLargeImageUrl" :alt="item.canonicalTitle" />
          <div class="card-content">
            <h5>{{ item.canonicalTitle }}</h5>
          </div>
        </router-link>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: "AllMangas",
  data() {
    return {
      mangas: [],
    };
  },
  created() {
    // Chamada à API que retorna a lista de mangas
    fetch("/api/mangas") // ajuste a rota conforme sua API
      .then((res) => res.json())
      .then((data) => {
        this.mangas = data;
      });
  },
};
</script>

<style scoped>
/* Adapte o CSS do homeStyles.css e site.css aqui, se necessário */
</style>
