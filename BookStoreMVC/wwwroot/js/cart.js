const shoppingCartBtns = document.querySelectorAll('.shopping-cart-btn')
const shoppingCartItemsAmount = document.querySelector('#cart-amount')


shoppingCartBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        const productId = parseInt(btn.getAttribute('data-id'))
        const productQuantity = 1
        const product = {
            productId: productId,
            quantity: productQuantity
        }
        fetch(`/Customer/ShoppingCart/AddToShoppingCart/${productId}`, {
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
                        message: "Product was added to cart!",
                        status: TOAST_STATUS.SUCCESS,
                        timeout: 5000
                    }
                }
                else {

                    toast = {
                        title: "Error",
                        message: "There was an error with adding product to cart.",
                        status: TOAST_STATUS.DANGER,
                        timeout: 5000
                    }
                }
                Toast.create(toast);
                refreshCartProductAmount()
        })
    })
})

function refreshCartProductAmount() {
    fetch('/Customer/ShoppingCart/GetCartAmount', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(res => {
            console.log(res)
            if (!res.ok) {
                return;
            }
            return res.json()
            
        })
        .then(data => {
            if (data) {
                shoppingCartItemsAmount.innerHTML = `(${data.cartAmount})`
            }
            
        })
}

refreshCartProductAmount()