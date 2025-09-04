<template>
  <section class="catalog" id="catalog">
    <h2 class="section-title">{{ pageTitle }}</h2>
    <div class="card-wrapper">
      <div class="card-item" v-for="item in items" :key="item.id">
        <router-link
          class="custom-link"
          :to="`/${entityType}OnPage/${item.id}`"
        >
          <img :src="item.webpLargeImageUrl" :alt="item.canonicalTitle" />
          <div class="card-content">
            <h5>{{ item.canonicalTitle }}</h5>
          </div>
        </router-link>
      </div>
    </div>
  </section>
</template>

<script setup>
import { ref, onMounted, watch, computed } from "vue";
import { AnimeService } from "../../services/AnimeService";
import { MangaService } from "../../services/MangaService";

const props = defineProps({
  type: { type: String, required: true },         // favorites, score, usercount, popularity
  entityType: { type: String, required: true }    // "anime" ou "manga"
});

const items = ref([]);
const token = localStorage.getItem("token") || "";

// retorna serviço correto
const getService = () => props.entityType === "anime" ? new AnimeService(token) : new MangaService(token);

// título dinâmico
const pageTitle = computed(() => {
  const name = props.entityType === "anime" ? "Animes" : "Mangas";
  switch (props.type) {
    case "favorites": return `All Time Favorite ${name}`;
    case "score": return `All Time By Score ${name}`;
    case "usercount": return `Most Followed ${name}`;
    case "popularity": return `Most Popular ${name}`;
    default: return `All ${name}`;
  }
});

const load = async () => {
  debugger
  items.value = []; // limpa enquanto carrega
  const service = getService();

  // passa a prop "type" como catalogName para a API
  items.value = (await service.getByCatalog(props.type)).data;
};

// monta e observa mudanças
onMounted(load);
watch(() => [props.type, props.entityType], load);
</script>

<style scoped>
@import "/css/site.css";

</style>
