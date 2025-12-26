<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import apiClient from './api';
import { validateCredit } from './Validators/CreditValidator';

const router = useRouter();
const isLoading = ref(false);
const errorMessage = ref(null);
const fieldErrors=ref({});
const successMessage = ref(null);

const initialFormState = ({
  number: '',
  amount: 0,
  termValue: 0,
  interestValue: 0
});
const form = ref({ ...initialFormState });


const handleSubmit = async () => {
  try {
    fieldErrors.value = {};
    errorMessage.value = null;
     successMessage.value = null;


    const { isValid, errors } = validateCredit(form.value);


    if (!isValid) {
      fieldErrors.value = errors;
      return;
    }

    isLoading.value = true;

    await apiClient.post('/Credits', {
      number: form.value.number,
      amount: Number(form.value.amount),
      termValue: Number(form.value.termValue),
      interestValue: Number(form.value.interestValue)
    });
  successMessage.value = 'Заявка успешно создана!';
   form.value = { ...initialFormState };
   setTimeout(() => {
        successMessage.value = null;
    }, 3000);

  } catch (error) {
    console.error('Ошибка при создании:', error);
    errorMessage.value = 'Не удалось создать заявку. Проверьте данные.';
  } finally {
    isLoading.value = false;
  }
};
</script>

<template>
  <div class="page-container">
    <div class="form-card">
     <h1 style="margin-top: 0; margin-bottom: 24px;">Новая заявка на займ</h1>

     <div v-if="successMessage" class="success-banner">
      {{ successMessage }}
    </div>

    <div v-if="errorMessage" class="error">{{ errorMessage }}</div>

    <form @submit.prevent="handleSubmit" class="credit-form">
      <div class="form-group">
        <label>Номер заявки:</label>
        <input v-model="form.number" type="text" required placeholder="001-А"
        :class="{'input-error': fieldErrors.number}">
        <span v-if="fieldErrors.number" class="error-text">{{ fieldErrors.number }}</span>
      </div>

      <div class="form-group">
        <label>Сумма:</label>
        <input v-model="form.amount" type="number" step="10" required
         :class="{'input-error': fieldErrors.amount}">
        <span v-if="fieldErrors.amount" class="error-text">{{ fieldErrors.amount }}</span>
      </div>

      <div class="form-group">
        <label>Срок займа (мес.):</label>
        <input v-model="form.termValue" type="number" required
        :class="{'input-error': fieldErrors.termValue}">
        <span v-if="fieldErrors.termValue" class="error-text">{{ fieldErrors.termValue }}</span>
      </div>

      <div class="form-group">
        <label>Процентная ставка (%):</label>
        <input v-model="form.interestValue" type="number" step="0.1" required
         :class="{'input-error': fieldErrors.interestValue}">
        <span v-if="fieldErrors.interestValue" class="error-text">{{ fieldErrors.interestValue }}</span>
      </div>

       <div class="form-actions">
        <button type="submit" class="btn btn-green" :disabled="isLoading">
          {{ isLoading ? 'Создание...' : 'Создать заявку' }}
        </button>
        <button type="button" @click="router.push('/')" class="btn btn-gray">Отмена</button>
      </div>
    </form>
  </div>
  </div>
</template>

<style scoped>

</style>
