const itemQuantityInputs = document.querySelectorAll('.cart-item-quantity-input')

itemQuantityInputs.forEach(input => {
    input.addEventListener('change', () => {
        const productId = input.getAttribute('data-id')
        const quantity = input.value
        const product = {
            productId: productId,
            quantity: quantity
        }
        fetch(`/Customer/ShoppingCart/UpdateItemQuantity/${productId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(product)
        })
            .then(res => res.json())
            .then(data => {
                let toast;
                if (data.success == true) {
                    toast = {
                        title: "Success",
                        message: "Product quantity was updated!",
                        status: TOAST_STATUS.SUCCESS,
                        timeout: 5000
                    }
                }
                else {

                    toast = {
                        title: "Error",
                        message: "There was an error with updating product quantity.",
                        status: TOAST_STATUS.DANGER,
                        timeout: 5000
                    }
                }
                Toast.create(toast);
                refreshCartProductAmount()
            })
    })
})