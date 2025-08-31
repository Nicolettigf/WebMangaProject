<!-- src/components/MediaOnPage.vue -->
<template>
  <div>
    <!-- Banner -->
    <div class="mop-banner" :style="{ backgroundImage: `url('${bannerImage}')` }"></div>

    <!-- Header -->
    <div class="mop-header">
      <div class="mop-container">
        <div class="cover-wrap overlap-banner">
          <div class="cover-wrap-inner">
            <img class="mop-poster" :src="item.posterImageLarge || item.webpLargeImageUrl" />

            <div v-if="userAuthenticated" class="mop-actions">
              <div class="mop-list">
                <div class="addList" @click="$emit('openModal')">Add to List</div>
                <div class="dropdown el-dropdown">
                  <span class="el-dropdown-link el-dropdown-selfdefine">
                    <i class="uil uil-angle-down"></i>
                  </span>
                </div>
              </div>
              <div class="mop-favorite">
                <i class="uil uil-heart-alt"></i>
              </div>
            </div>
          </div>
        </div>

        <!-- Description -->
        <div class="mop-desc-content">
          <h1>{{ item.title }}</h1>
          <p class="mop-description" :class="{ active: showFull }">{{ shortSynopsis }}</p>
          <p class="mop-full-description" :class="{ active: showFull }">{{ item.synopsis }}</p>
          <span v-if="item.synopsis?.length > 700" class="mop-read-more" @click="toggleFull" v-show="!showFull">Read More</span>
          <span v-if="item.synopsis?.length > 700" class="mop-read-less" @click="toggleFull" v-show="showFull">Read Less</span>
        </div>
      </div>
    </div>

    <!-- Info -->
    <div class="mop-div-info">
      <div class="div-media-info">
        <div v-for="(value, key) in infoFields" :key="key" class="anime-info-items">
          <div class="info-type">{{ key }}:</div>
          <div class="info-result">{{ value }}</div>
        </div>

        <div class="anime-info-items" v-if="item.genres?.length">
          <div class="info-type">Genres:</div>
          <div class="info-result">
            <ul>
              <li v-for="genre in item.genres" :key="genre.id">{{ genre.name }}</li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'

const props = defineProps({
  item: Object,
  userAuthenticated: Boolean
})

const showFull = ref(false)

const bannerImage = computed(() => props.item.coverImageLarge || props.item.jpgLargeImageUrl || '/images/default-banner.jpg')
const shortSynopsis = computed(() => props.item.synopsis?.length > 700 ? props.item.synopsis.substring(0, 700) + '...' : props.item.synopsis)

// Campos de informação que vão aparecer dinamicamente
const infoFields = computed(() => ({
  Format: props.item.type,
  Episodes: props.item.episodes,
  Status: props.item.status,
  'Start Date': props.item.from,
  'End Date': props.item.to ?? 'Not Finished',
  'Average Score': props.item.rating,
  Popularity: props.item.popularity,
  Favorites: props.item.favorites
}))

function toggleFull() {
  showFull.value = !showFull.value
}
</script>


<style scoped>
@import "/css/site.css";
</style>
