const itemQuantityInputs = document.querySelectorAll('.cart-item-quantity-input')
const itemDeleteButtons = document.querySelectorAll('.cart-item-remove-btn')

itemQuantityInputs.forEach(input => {
    input.addEventListener('change', () => {
        const productId = parseInt(input.getAttribute('data-id'))
        const quantity = parseInt(input.value)
        const product = {
            Id: productId,
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
                console.log(data)
                let toast;
                if (data.success == true) {
                    toast = {
                        title: "Success",
                        message: "Product has been successfully removed from the cart.",
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