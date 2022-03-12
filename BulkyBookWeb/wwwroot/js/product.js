var DataTable;

$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    DataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data":"title", "width": "15%" },
            { "data":"isbn", "width": "15%" },
            { "data":"price", "width": "15%" },
            { "data":"author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { 
                "data": "productId",
                "render": function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a class="btn btn-primary" style="margin-right:10px;" href="/Admin/Product/Upsert?productId=${data}">
                                <i class="bi bi-pencil-square"></i>
                                Edit
                            </a>
                            <a onClick=Delete('/Admin/Product/Delete?productId=${data}') class="btn btn-danger">
                                <i class="bi bi-x-square"></i>
                                Delete
                            </a>
                        </div>
                        `
                },
                "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        DataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}