<template>
  <div class="catalog-home">
    <div
      v-for="section in sections"
      :key="section.title"
      class="catalog alh-grid"
    >
      <div class="idiv-config">
        <h3 class="section-title">{{ section.title }}</h3>
        <div class="va-div" v-if="section.viewAllLink">
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
import { ref, onMounted, computed, watch } from "vue";
import { HomeService } from "../../services/HomeService";

const props = defineProps({
  entityType: { type: String, required: true } // "anime" ou "manga"
});

const token = localStorage.getItem("token") || "";
const homeService = new HomeService(token);

// listas do retorno
const topByScoredBy = ref([]);
const topByPopularity = ref([]);
const topByFavorites = ref([]);
const topByMembers = ref([]);
const topByScore = ref([]);
const topByRank = ref([]);
const latest = ref([]);

// títulos dinâmicos baseados nas listas do backend
const sections = computed(() => [
  { title: "Top Popularity", viewAllLink: `/${props.entityType}/popularity`, items: topByPopularity.value },
  { title: "Top Favorites", viewAllLink: `/${props.entityType}/favorites`, items: topByFavorites.value },
  { title: "Top Members", viewAllLink: `/${props.entityType}/members`, items: topByMembers.value },
  { title: "Top Score", viewAllLink: `/${props.entityType}/score`, items: topByScore.value },
  { title: "Top Rank", viewAllLink: `/${props.entityType}/rank`, items: topByRank.value },
  { title: "Top ScoredBy", viewAllLink: `/${props.entityType}/scoredby`, items: topByScoredBy.value },
  { title: "Latest", viewAllLink: `/${props.entityType}/latest`, items: latest.value },
]);

// função de carregamento
async function load() {
  // reset
  
  topByScoredBy.value = [];
  topByPopularity.value = [];
  topByFavorites.value = [];
  topByMembers.value = [];
  topByScore.value = [];
  topByRank.value = [];
  latest.value = [];

  const data = await homeService.GetHomeMedia(0, 7, props.entityType);

  if (!data) return;

  // popular as listas do frontend com base no retorno do backend
  topByPopularity.value = data.item.topByPopularity || [];
  topByScoredBy.value = data.item.topByScoredBy || [];
  topByFavorites.value = data.item.topByFavorites || [];
  topByMembers.value = data.item.topByMembers || [];
  topByScore.value = data.item.topByScore || [];
  topByRank.value = data.item.topByRank || [];
  latest.value = data.item.latest || [];
}

// chama ao montar
onMounted(load);

// chama toda vez que a prop entityType mudar
watch(() => props.entityType, () => {
  load();
});
</script>

<style scoped>
@import "/css/site.css";
</style>
