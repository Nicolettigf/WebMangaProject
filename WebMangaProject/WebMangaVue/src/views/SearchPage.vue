<template>
  <div
    class="search-page"
    :class="{ 'has-results': searchQuery.length >= 3 && (filteredMangas.length || filteredAnimes.length) }"
  >
    <h2 v-if="searchQuery.length >= 3">Resultados da busca para: "{{ searchQuery }}"</h2>
    <h2 v-else>Digite pelo menos 3 letras para buscar</h2>

    <!-- Dual Column -->
    <transition name="slide-fade">
      <div
        v-if="filteredMangas.length && filteredAnimes.length && searchQuery.length >= 3"
        class="dual-column"
      >
        <div class="search-column">
          <h3>Mangás</h3>
          <div class="card-wrapper">
            <router-link
              v-for="item in filteredMangas"
              :key="item.id"
              class="card-item custom-link"
              :to="`/MangaOnPage/${item.id}`"
            >
              <img :src="item.image" />
              <div class="card-content"><h5>{{ item.name }}</h5></div>
            </router-link>
          </div>
        </div>

        <div class="search-column">
          <h3>Animes</h3>
          <div class="card-wrapper">
            <router-link
              v-for="item in filteredAnimes"
              :key="item.id"
              class="card-item custom-link"
              :to="`/AnimeOnPage/${item.id}`"
            >
              <img :src="item.image" />
              <div class="card-content"><h5>{{ item.name }}</h5></div>
            </router-link>
          </div>
        </div>
      </div>
    </transition>

    <!-- Single Column -->
    <transition name="slide-fade">
      <div
        v-if="(filteredMangas.length || filteredAnimes.length) && searchQuery.length >= 3 && !(filteredMangas.length && filteredAnimes.length)"
        class="single-column"
      >
        <div class="search-column">
          <h3>{{ filteredMangas.length ? 'Mangás' : 'Animes' }}</h3>
          <div class="card-wrapper">
            <router-link
              v-for="item in filteredMangas.length ? filteredMangas : filteredAnimes"
              :key="item.id"
              class="card-item custom-link"
              :to="item.type === 'manga' ? `/MangaOnPage/${item.id}` : `/AnimeOnPage/${item.id}`"
            >
              <img :src="item.image" />
              <div class="card-content"><h5>{{ item.name }}</h5></div>
            </router-link>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>


<script>
import { ref, watch, onMounted, computed } from "vue";
import { useRoute, useRouter } from "vue-router";
import { HomeService } from "../services/HomeService";

export default {
  name: "SearchPage",
  setup() {
    const route = useRoute();
    const router = useRouter();
    const searchQuery = ref(route.query.q || "");
    const searchResults = ref({ animes: [], mangas: [] });
    const isLoading = ref(false);
    const message = ref("Digite pelo menos 3 letras para buscar");

    const homeService = new HomeService();

    const filteredMangas = computed(() => searchResults.value.mangas || []);
    const filteredAnimes = computed(() => searchResults.value.animes || []);

    const fetchSearchResults = async (query) => {
      if (!query || query.length < 3) {
        searchResults.value = { animes: [], mangas: [] };
        message.value = "Digite pelo menos 3 letras para buscar";
        return;
      }

      isLoading.value = true;
      message.value = "";

      try {
        const data = await homeService.GetByName(query);
        if (data?.hasSuccess) {
          searchResults.value = data.item;
          if (!searchResults.value.animes.length && !searchResults.value.mangas.length) {
            message.value = "Nenhum resultado encontrado";
          }
        } else {
          message.value = "Erro ao buscar dados";
        }
      } catch (error) {
        console.error("Erro na busca:", error);
        message.value = "Erro ao buscar dados";
      } finally {
        isLoading.value = false;
      }
    };

    // 🔹 Debounce de 1 segundo
    let debounceTimer;
    watch(searchQuery, (newQuery) => {
      clearTimeout(debounceTimer);
      debounceTimer = setTimeout(() => {
        if (newQuery.length >= 3) {
          fetchSearchResults(newQuery);
          router.replace({ query: { q: newQuery } }); // atualiza URL
        } else {
          searchResults.value = { animes: [], mangas: [] };
          message.value = "Digite pelo menos 3 letras para buscar";
        }
      }, 1000);
    });

    const handleEsc = (event) => {
      if (event.key === "Escape") {
        searchQuery.value = "";
        router.push({ path: "/" });
      }
    };

    onMounted(() => {
      window.addEventListener("keydown", handleEsc);

      // 🔹 Busca inicial apenas se houver query
      if (searchQuery.value.length >= 3) {
        fetchSearchResults(searchQuery.value);
      }
    });

    return {
      searchQuery,
      searchResults,
      isLoading,
      message,
      filteredMangas,
      filteredAnimes
    };
  },
};
</script>




<style scoped>

@import "/css/site.css";

.search-page {
  padding: 2rem;
  min-height: 100%;
  background-color: #171522;
  color: #fff;
  display: flex;
  flex-direction: column;

  /* Transição suave */
  transition: max-height 1.5s ease, 
  background-color 0.5s ease;
  max-height: 200px; 
}

/* Quando há resultados */
.search-page.has-results {
  max-height: 2000px; /* valor grande suficiente para conter os resultados */
  background-color: #1a1830; /* muda a cor, se você quiser efeito de fade */
}

.search-page h2 {
  margin-bottom: 2rem;
  font-size: 1.8rem;
  font-weight: 600;
  text-align: center;
}

/* Container para dual-column */
.dual-column {
  display: flex;
  gap: 2rem;
  justify-content: center;
  flex-wrap: wrap;
}

/* Cada coluna (Mangá ou Anime) */
.search-column {
  flex: 1;
  min-width: 320px;
}

.search-column h3 {
  margin-bottom: 1rem;
  font-size: 1.5rem;
  font-weight: 600;
  text-align: center;
}

/* Reaproveita grid da HomePage */
.card-wrapper {
  display: flex;
  flex-wrap: wrap;
  justify-content: flex-start;
  gap: 28px 30px;
}

/* Centraliza se houver apenas uma coluna */
.single-column .card-wrapper {
  justify-content: center;
}
.single-column .search-column h3 {
  text-align: center;
}



/* Transição de slide e fade */
.slide-fade-enter-active,
.slide-fade-leave-active {
  transition: all 0.4s ease; /* duração da animação */
}

.slide-fade-enter-from,
.slide-fade-leave-to {
  opacity: 0;
  transform: translateY(-20px); /* começa levemente acima e vai descendo */
}

.slide-fade-enter-to,
.slide-fade-leave-from {
  opacity: 1;
  transform: translateY(0);
}


</style>
