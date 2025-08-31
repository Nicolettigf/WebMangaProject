<template>
  <div class="profile-container">
    <div class="profile-images">
      <img :src="`/covers/${user.CoverImageFileLocation}`" class="cover-photo" />
      <img :src="`/avatars/${user.AvatarImageFileLocation}`" class="profile-icon" />
    </div>

    <div class="profile-info">
      <div class="profile-name">{{ user.Nickname }}</div>
      <div class="profile-about">
        <h5>About me:</h5>
        <hr />
        <p>{{ user.About }}</p>
      </div>
    </div>

    <div class="profile-favorites">
      <div class="btn-config">
        <button @click="showFavorites('manga')" :class="{ active: activeFav === 'manga' }">Manga</button>
        <button @click="showFavorites('anime')" :class="{ active: activeFav === 'anime' }">Anime</button>
      </div>
      <ul v-if="activeFav === 'manga'">
        <li v-for="item in user.MangaList.filter(i => i.Favorite)" :key="item.Id">
          <a :href="`/Manga/MangaOnPage/${item.Id}`">
            <img :src="item.Manga.WebpLargeImageUrl" />
            <span>{{ item.Manga.Title }}</span>
          </a>
        </li>
      </ul>
      <ul v-if="activeFav === 'anime'">
        <li v-for="item in user.AnimeList.filter(i => i.Favorite)" :key="item.Id">
          <a :href="`/Anime/AnimeOnPage/${item.Id}`">
            <img :src="item.Anime.WebpLargeImageUrl" />
            <span>{{ item.Anime.Title }}</span>
          </a>
        </li>
      </ul>
    </div>

    <div class="profile-geral-list">
      <div class="btn-config">
        <button @click="activeList='All'">All</button>
        <button @click="activeList='Reading'">Reading</button>
        <button @click="activeList='PlanToRead'">Plan to Read / Watch</button>
        <button @click="activeList='Completed'">Completed</button>
        <button @click="activeList='Rereading'">Rereading / Rewatching</button>
        <button @click="activeList='Paused'">Paused</button>
        <button @click="activeList='Dropped'">Dropped</button>
      </div>

      <div class="display-list">
        <div v-for="item in filteredList" :key="item.Id">
          <a :href="itemType(item)">
            <li>
              <img :src="itemImage(item)" />
              <span>{{ itemTitle(item) }}</span>
              <span v-if="item.Score > 0"> | <span v-for="n in item.Score" :key="n">&#11088;</span></span>
              <span v-if="item.Favorite"> | &#10084;</span>
            </li>
          </a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "UserProfile",
  props: {
    user: Object
  },
  data() {
    return {
      activeFav: 'manga',
      activeList: 'All'
    }
  },
  computed: {
    filteredList() {
      const mangaFiltered = this.user.MangaList.filter(i => this.activeList === 'All' || i.Status === this.activeList);
      const animeFiltered = this.user.AnimeList.filter(i => this.activeList === 'All' || i.Status === this.activeList.replace('PlanToRead','PlanToWatch').replace('Rereading','ReWatching'));
      return [...mangaFiltered, ...animeFiltered];
    }
  },
  methods: {
    showFavorites(type) {
      this.activeFav = type;
    },
    itemImage(item) {
      return item.Manga ? item.Manga.WebpLargeImageUrl : item.Anime.WebpLargeImageUrl;
    },
    itemTitle(item) {
      return item.Manga ? item.Manga.Title : item.Anime.Title;
    },
    itemType(item) {
      return item.Manga ? `/Manga/MangaOnPage/${item.Manga.Id}` : `/Anime/AnimeOnPage/${item.Anime.Id}`;
    }
  }
}
</script>

<style scoped>
/* Aqui você porta os estilos CSS da página Razor */
</style>
