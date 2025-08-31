<template>
  <MediaOnPage :item="manga" :userAuthenticated="userAuthenticated" @openModal="openModal" />
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { MangaService } from '../../services/MangaService'
import MediaOnPage from '../MediaOnPage.vue'

const route = useRoute()
const mangaId = route.params.id

const token = localStorage.getItem('token') || ''
const mangaService = new MangaService(token)

const manga = ref({})
const userAuthenticated = ref(false)

async function fetchManga() {
  const data = await mangaService.getComplete(mangaId)
  manga.value = data.item
  userAuthenticated.value = data.userAuthenticated
}

onMounted(() => fetchManga())

function openModal() {
  console.log('Abrir modal de lista')
}
</script>
