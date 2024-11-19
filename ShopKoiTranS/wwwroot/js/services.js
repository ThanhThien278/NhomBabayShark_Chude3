function toggleForm() {
    var serviceSelect = document.getElementById('service-select');
    var shippingForm = document.getElementById('shipping-form');
    var consultationForm = document.getElementById('consultation-form');


    if (serviceSelect.value === 'shipping') {
        shippingForm.style.display = 'block';
        consultationForm.style.display = 'none';
    } else if (serviceSelect.value === 'consultation') {
        shippingForm.style.display = 'none';
        consultationForm.style.display = 'block';
    }
}


document.addEventListener('DOMContentLoaded', toggleForm);
};