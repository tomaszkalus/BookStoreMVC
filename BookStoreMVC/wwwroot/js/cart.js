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
            .then(res => {
                if (!res.ok) {
                    return;
                }
                return res.json()
            })
            .then(data => {
                const toast = {
                    title: "Success",
                    message: "Product quantity has been updated.",
                    status: TOAST_STATUS.SUCCESS,
                }

                if (!data || data.success != true) {
                    toast.title = "Error"
                    toast.message = "There was an error with updating product quantity."
                    toast.status = TOAST_STATUS.DANGER
                }
                Toast.create(toast);
                refreshItemQuantityAndPrice(productId)
                updateTotalPrices()
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
                }

                if (data && data.success) {
                    document.querySelector(`#cart-item-${productId}`).remove()
                }
                else {
                    toast.title = "Error"
                    toast.message = "There was an error with removing product from cart."
                    toast.status = TOAST_STATUS.DANGER
                }
                
                Toast.create(toast);
                refreshCartProductAmount()
                updateTotalPrices()
            })
    })
})

function refreshItemQuantityAndPrice(productId) {
    fetch(`/Customer/ShoppingCart/ItemTotalPrice/${productId}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(res => {
            if (!res.ok) {
                return;
            }
            return res.json()
        })
        .then(data => {
            if (data) {
                const itemTotalPrice = document.querySelector(`#total-price-${productId}`)
                const itemQuantityInput = document.querySelector(`#quantity-${productId}-input`)
                const itemQuantity = document.querySelector(`#quantity-${productId}`)
                itemTotalPrice.textContent = data.totalPrice
                itemQuantity.textContent = itemQuantityInput.value
            }
        })
}

function updateTotalPrices() {
    fetch(`/Customer/ShoppingCart/TotalPrices`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(res => {
            if (!res.ok) {
                return;
            }
            return res.json()
        })
        .then(data => {
            if (data) {
                document.querySelector('#total').textContent = data.total;
                document.querySelector('#subtotal').textContent = data.subtotal;
                document.querySelector('#vat').textContent = data.vat;
                document.querySelector('#shipping').textContent = data.shipping;
            }
        })
}

//updateTotalPrices()