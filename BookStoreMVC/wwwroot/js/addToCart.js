
shoppingCartBtns = document.querySelectorAll('.shopping-cart-btn')
const shoppingCartItemsAmount = document.querySelector('#cart-amount')

shoppingCartBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        const productId = parseInt(btn.getAttribute('data-id'))
        const productQuantity = 1
        const product = {
            ProductId: productId,
            Quantity: productQuantity
        }
        fetch(`/api/user/cart/${productId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: product
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
    fetch('/api/user/cart/', {
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
                shoppingCartItemsAmount.innerHTML = `(${data.itemsQuantity})`
            }            
        })
}

refreshCartProductAmount()