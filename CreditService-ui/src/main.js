import './assets/main.css'

import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import CreditList from './Credit.vue'
import CreateCredit from './CreateCredit.vue'
import App from './App.vue'

const routes = [
  { path: '/', component: CreditList },
  { path: '/create', component: CreateCredit }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

const app = createApp(App)
app.use(router)
app.mount('#app')

