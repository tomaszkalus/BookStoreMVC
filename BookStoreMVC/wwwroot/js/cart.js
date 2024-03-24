const itemQuantityInputs = document.querySelectorAll('.cart-item-quantity-input')
const itemDeleteButtons = document.querySelectorAll('.cart-item-remove-btn')

itemQuantityInputs.forEach(input => {
    input.addEventListener('change', () => {
        const productId = parseInt(input.getAttribute('data-id'))
        const quantity = parseInt(input.value)

        fetch(`/api/user/cart/item/${productId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: quantity
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
                    timeout: 2000
                }

                if (!data || data.status != "success") {
                    toast.title = "Error"
                    toast.message = "There was an error with updating product quantity."
                    toast.status = TOAST_STATUS.DANGER
                }
                Toast.create(toast);
                refreshItemQuantity(productId)
                refreshProductPrice(productId, data.data.items)
                updateTotalPrices(data.data)
            })
    })
})

itemDeleteButtons.forEach(button => {
    button.addEventListener('click', () => {
        const productId = parseInt(button.getAttribute('data-id'))

        fetch(`/api/user/cart/item/${productId}`, {
            method: 'DELETE',
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
                    timeout: 2000
                }

                if (!data || data.status != "success") {
                    toast.title = "Error"
                    toast.message = "There was an error with removing product from cart."
                    toast.status = TOAST_STATUS.DANGER
                }

                Toast.create(toast);
                removeCartItem(productId)
                refreshCartProductAmount(data.data.itemsQuantity)
                updateTotalPrices(data.data)
                
            })
    })
})

function removeCartItem(productId) {
    const item = document.querySelector(`#cart-item-${productId}`)
    item.remove()
}

function refreshItemQuantity(productId) {
    const itemQuantityInput = document.querySelector(`#quantity-${productId}-input`)
    const itemQuantity = document.querySelector(`#quantity-${productId}`)
    itemQuantity.textContent = itemQuantityInput.value
}

function refreshProductPrice(productId, cartItems) {
    const itemTotalPrice = document.querySelector(`#total-price-${productId}`)
    const cartItem = cartItems.find(item => item.productId == productId)
    itemTotalPrice.textContent = cartItem.totalPrice.formatted
}

function updateTotalPrices(cart) {
    document.querySelector('#total').textContent = cart.total.formatted;
    document.querySelector('#subtotal').textContent = cart.subtotal.formatted;
    document.querySelector('#vat').textContent = cart.vat.formatted;
    document.querySelector('#shipping').textContent = cart.shipping.formatted;
}