export const validateCredit = (form)=> {
  const errors = {};


if(!form.number || form.number.trim().length<3) {
  errors.number = 'Номер должен содержать минимум 3 символа';
}

if(form.amount <=0) {
  errors.amount= 'Сумма должна быть больше нуля';
}

if (!Number.isInteger(Number(form.termValue)) || form.termValue <=0) {
  errors.termValue = 'Срок должен быть не менне одного месяца';
}

if (form.interestValue <= 0 || form.interestValue>100) {
  errors.interestValue= 'Ставка должна быть от 0.1 до 100%';
}

 return {
    isValid: Object.keys(errors).length === 0,
    errors
  };
};
