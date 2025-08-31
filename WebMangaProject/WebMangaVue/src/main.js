import { createApp } from 'vue';
import App from './App.vue';           // raiz do app
import router from './RouterConfig'; // seu arquivo de rotas

import './style.css'; // CSS global

const app = createApp(App);

// usar o router
app.use(router);

app.mount('#app');
