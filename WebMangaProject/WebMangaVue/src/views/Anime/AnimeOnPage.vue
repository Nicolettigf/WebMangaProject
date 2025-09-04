<template>
  <MediaOnPage :item="anime" :userAuthenticated="userAuthenticated" @openModal="openModal" />
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AnimeService } from '../../services/AnimeService'
import MediaOnPage from '../MediaOnPage.vue'

const route = useRoute()
const animeId = route.params.id
const token = localStorage.getItem('token') || ''
const animeService = new AnimeService(token)

const anime = ref({})
const userAuthenticated = ref(false)

async function fetchAnime() {
  const data = await animeService.getAnimeOnPage(animeId)
  anime.value = data.item
  userAuthenticated.value = data.userAuthenticated
}

onMounted(() => fetchAnime())

function openModal() {
  console.log('Abrir modal de lista')
}
</script>
