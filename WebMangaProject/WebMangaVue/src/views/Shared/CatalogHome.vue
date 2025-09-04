<template>
  <div class="catalog-home">
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
          :to="`/${entityType}OnPage/${item.id}`"
        >
          <img :src="item.webpLargeImageUrl" :alt="item.canonicalTitle" />
          <div class="card-content">
            <h5>{{ item.canonicalTitle }}</h5>
          </div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, onMounted, computed } from "vue";
import { AnimeService } from "../../services/AnimeService";
import { MangaService } from "../../services/MangaService";

const props = defineProps({
  entityType: { type: String, required: true } // "anime" ou "manga"
});

const token = localStorage.getItem("token") || "";
const itemsFavorites = ref([]);
const itemsByCount = ref([]);
const itemsByRating = ref([]);

// função que devolve o service correto
function getService() {
  return props.entityType === "anime"
    ? new AnimeService(token)
    : new MangaService(token);
}

// sections dinâmicas
const sections = computed(() => {
  return [
    {
      title: "All Time Favorites",
      viewAllLink: `/${props.entityType}/favorites`,
      items: itemsFavorites.value
    },
    {
      title: "Most Popular",
      viewAllLink: `/${props.entityType}/popularity`,
      items: itemsByCount.value
    },
    {
      title: "All Time By Score",
      viewAllLink: `/${props.entityType}/score`,
      items: itemsByRating.value
    }
  ];
});

// função de carregamento
async function load() {
  itemsFavorites.value = [];
  itemsByCount.value = [];
  itemsByRating.value = [];

  const service = getService();
  const favorites = await service.getByFavorites();
  const count = await service.getByUserCount();
  const rating = await service.getByRating();

  itemsFavorites.value = favorites.data;
  itemsByCount.value = count.data;
  itemsByRating.value = rating.data;
}

onMounted(load);

// sempre que trocar de anime → manga (ou o contrário), recarrega
watch(() => props.entityType, () => {
  load();
});
</script>

<style scoped>
@import "/css/site.css";
</style>
