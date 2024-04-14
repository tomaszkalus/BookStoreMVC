
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'author', "width": "15%" },
            { data: 'title', "width": "15%" },
            { data: 'isbn', "width": "10%" },
            { data: 'price', "width": "5%" },
            { data: 'price50', "width": "10%" },
            { data: 'price100', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "width": "25%",
                "render": function (data) {
                    return `<div class="d-flex justify-content-center">
                    <div class="w-75 btn-group" role="group">
                    <a href="/admin/product/upsert?id=${data}" class="btn btn-primary"><i class="bi bi-pencil-square"></i> Edit</a>
                    <button class="btn btn-danger delete-product-btn" data-id="${data}"><i class="bi bi-trash-fill"></i> Delete</button>
                    </div>
                    </div>
                    `
                }
            },
        ],
        "drawCallback": function (settings, json) {
            console.log("DataTables has finished its initialisation.");
            document.querySelectorAll('.delete-product-btn').forEach(item => {
                item.addEventListener('click', event => {
                    console.log("X")


                    id = item.getAttribute('data-id');
                    showDeleteModal(id);
                })
            })
        }
    })
};

function showDeleteModal(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            const isDeleted = deleteProduct(id);
            console.log(isDeleted)
            if (isDeleted) {
                Swal.fire("Deleted!", "Your file has been deleted.", "success");
                dataTable.ajax.reload();
            } else {
                Swal.fire("Error!", "Something went wrong.", "error");
            }
        }
    });
}

async function deleteProduct(id) {
    fetch(`/admin/product/delete/${id}`, { method: 'DELETE' })
        .then((response) => {
            response.json().then(data => {
                if (data.success) {
                    return true;
                    dataTable.ajax.reload();
                } else {
                    return false;
                }
            })
        })
}

loadDataTable();



