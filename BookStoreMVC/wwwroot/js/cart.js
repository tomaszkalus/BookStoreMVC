const itemQuantityInputs = document.querySelectorAll('.cart-item-quantity-input')
const itemDeleteButtons = document.querySelectorAll('.cart-item-remove-btn')

itemQuantityInputs.forEach(input => {
    input.addEventListener('change', () => {
        const productId = parseInt(input.getAttribute('data-id'))
        const quantity = parseInt(input.value)
        const product = {
            ProductId: productId,
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
                const toast = {
                    title: "Success",
                    message: "Product quantity has been updated.",
                    status: TOAST_STATUS.SUCCESS,
                    timeout: 5000
                }

                if (data.success != true) {
                    toast.title = "Error"
                    toast.message = "There was an error with updating product quantity."
                    toast.status = TOAST_STATUS.DANGER
                }

                Toast.create(toast);
                refreshCartProductAmount()
            })
    })
})

itemDeleteButtons.forEach(button => {
    button.addEventListener('click', () => {
        const productId = parseInt(button.getAttribute('data-id'))

        fetch(`/Customer/ShoppingCart/RemoveCartItem/${productId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(data => {
                const toast = {
                    title: "Success",
                    message: "Product has been successfully removed from the cart.",
                    status: TOAST_STATUS.SUCCESS,
                    timeout: 5000
                }

                if (data.success == true) {
                    document.querySelector(`#cart-item-${productId}`).remove()
                }
                else {
                    toast.title = "Error"
                    toast.message = "There was an error with removing product from cart."
                    toast.status = TOAST_STATUS.DANGER
                }
                Toast.create(toast);
                refreshCartProductAmount()
            })
    })
})
