
<script setup>
import { ref, onMounted } from 'vue';
import apiClient from './api';

const credits = ref([]);
const isLoading = ref(true);
const errorMessage = ref(null);

const STATUS_PUBLISHED = 0;
const STATUS_UNPUBLISHED = 1;
const STATUS_LABELS = {
  [STATUS_PUBLISHED]: 'Опубликована',
  [STATUS_UNPUBLISHED]: 'Снята с публикации'
};
const getStatusLabel = (status) => {
  return STATUS_LABELS[status] ?? `Неизвестный статус (${status})`;
};
const filters = ref({
  status: '',
  minAmount: null,
  maxAmount: null,
  minTerm: null,
  maxTerm: null
});

const fetchCredits = async () => {
  try {
    isLoading.value = true;
    errorMessage.value = null;
     const params = {};

    if (filters.value.status !== '') params.status = filters.value.status;
    if (filters.value.minAmount) params.minAmount = filters.value.minAmount;
    if (filters.value.maxAmount) params.maxAmount = filters.value.maxAmount;
    if (filters.value.minTerm) params.minTerm = filters.value.minTerm;
    if (filters.value.maxTerm) params.maxTerm = filters.value.maxTerm;

    const response = await apiClient.get('/Credits', {params});
    credits.value = response.data;
  } catch (error) {
    console.error('Ошибка при получении кредитов:', error);
    errorMessage.value = 'Не удалось загрузить данные. Проверьте соединение с бэкендом.';
  } finally {
    isLoading.value = false;
  }
};

const resetFilters = () => {
  filters.value = {
    status: '',
    minAmount: null,
    maxAmount: null,
    minTerm: null,
    maxTerm: null
  };
  fetchCredits();
};

const toggleStatus = async (credit) => {
  const newStatus = credit.status === STATUS_PUBLISHED ? STATUS_UNPUBLISHED : STATUS_PUBLISHED;

  try {

    await apiClient.put(`/Credits/${credit.number}`, {status: newStatus});


    credit.status = newStatus;
  } catch (error) {
    console.error('Ошибка при обновлении статуса:', error);
    alert('Не удалось изменить статус. Проверьте логи сервера.');
  }
};

onMounted(() => {
  fetchCredits();
});
</script>

<template>
  <div class="page-container" style="padding: 20px;">

    <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
      <h1>Список кредитов</h1>
      <button @click="$router.push('/create')" class="btn btn-green">+ Создать заявку</button>
    </div>

  <div class="filter-bar">
      <div class="filter-group">
        <label>Статус:</label>
        <select v-model="filters.status" @change="fetchCredits">
          <option value="">Все</option>
          <option :value="STATUS_PUBLISHED">Опубликована</option>
          <option :value="STATUS_UNPUBLISHED">Снята с публикации</option>
        </select>
      </div>

      <div class="filter-group">
        <label>Сумма (от/до):</label>
        <div class="input-range">
          <input type="number" v-model.number="filters.minAmount" placeholder="Мин." @input="fetchCredits">
          <input type="number" v-model.number="filters.maxAmount" placeholder="Макс." @input="fetchCredits">
        </div>
      </div>

      <div class="filter-group">
        <label>Срок (от/до):</label>
        <div class="input-range">
          <input type="number" v-model.number="filters.minTerm" placeholder="Мин." @input="fetchCredits">
          <input type="number" v-model.number="filters.maxTerm" placeholder="Макс." @input="fetchCredits">
        </div>
      </div>

      <button @click="resetFilters" class="btn btn-gray">Сбросить</button>
    </div>

    <div v-if="isLoading" class="status">Загрузка данных...</div>
    <div v-else-if="errorMessage" class="error">{{ errorMessage }}</div>

    <div v-else>
      <table v-if="credits.length > 0">
        <thead>
          <tr>
            <th>Номер</th>
            <th>Сумма</th>
            <th>Срок</th>
            <th>Процентная ставка</th>
            <th>Статус</th>
            <th>Дата создания</th>
            <th>Изменение статуса</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="credit in credits" :key="credit.number">
            <td>{{ credit.number }}</td>
            <td>{{ credit.amount }}</td>
            <td>{{ credit.termValue }}</td>
            <td>{{ credit.interestValue }}</td>
             <td :class="`status-v-${credit.status}`">
              {{ getStatusLabel(credit.status) }}
            </td>
            <td>{{ new Date(credit.createdAt).toLocaleDateString() }}</td>
             <td>

              <button
                @click="toggleStatus(credit)"
                :class="['btn', credit.status === STATUS_PUBLISHED ? 'btn-red' : 'btn-green']"
              >
                {{ credit.status === STATUS_PUBLISHED ? 'Снять с публикации' : 'Опубликовать' }}
              </button>
            </td>
          </tr>
        </tbody>
      </table>
      <p v-else>Кредиты не найдены.</p>
    </div>
  </div>

</template>

<style scoped>

</style>
