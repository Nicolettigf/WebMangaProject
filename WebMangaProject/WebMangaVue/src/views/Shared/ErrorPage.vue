<template>
  <div class="error-page">
    <h1 class="text-danger">Error.</h1>
    <h2 class="text-danger">An error occurred while processing your request.</h2>

    <div v-if="requestId">
      <p>
        <strong>Request ID:</strong> <code>{{ requestId }}</code>
      </p>
    </div>

    <h3>Development Mode</h3>
    <p>
      Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
    </p>
    <p>
      <strong>The Development environment shouldn't be enabled for deployed applications.</strong>
      It can result in displaying sensitive information from exceptions to end users.
      For local debugging, enable the <strong>Development</strong> environment.
    </p>
  </div>
</template>

<script setup>
import { ref } from 'vue'

// Simula o RequestId (em um app real você pegaria via API ou router state)
const requestId = ref(null)
</script>

<style scoped>
.error-page {
  padding: 2rem;
}
.text-danger {
  color: #dc3545;
}
</style>
