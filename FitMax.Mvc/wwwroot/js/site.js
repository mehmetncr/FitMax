document.addEventListener('DOMContentLoaded', function () {
    // Ürün toplamlarını hesapla ve görüntüle
    function updateProductTotal() {
        const productRows = document.querySelectorAll('tbody tr');
        let cartTotal = 0;

        productRows.forEach(function (row) {
            const price = parseFloat(row.cells[1].textContent.replace('$', ''));
            const quantity = parseInt(row.querySelector('.quantity-input').value);
            const total = price * quantity;
            row.querySelector('.product-total').textContent = `$${total.toFixed(2)}`;
            cartTotal += total;
        });

        document.getElementById('cart-total').textContent = `$${cartTotal.toFixed(2)}`;
    }

    // Ürün eklemeyi işle
    const addButtons = document.querySelectorAll('.add-button');
    addButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            const input = button.parentElement.parentElement.querySelector('.quantity-input');
            input.value = parseInt(input.value) + 1;
            updateProductTotal();
        });
    });

    // Ürün çıkarmayı işle
    const removeButtons = document.querySelectorAll('.remove-button');
    removeButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            const input = button.parentElement.parentElement.querySelector('.quantity-input');
            if (parseInt(input.value) > 1) {
                input.value = parseInt(input.value) - 1;
                updateProductTotal();
            }
        });
    });

    // Ürün adeti değiştiğinde işle
    const quantityInputs = document.querySelectorAll('.quantity-input');
    quantityInputs.forEach(function (input) {
        input.addEventListener('change', function () {
            if (parseInt(input.value) < 1) {
                input.value = 1;
            }
            updateProductTotal();
        });
    });

    // Sayfa yüklendiğinde toplamı güncelle
    updateProductTotal();
});


