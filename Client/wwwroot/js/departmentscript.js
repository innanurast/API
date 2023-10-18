var button = `
<button type="button" class="btn btn-primary" data-toggle="modal" data-tooltip="tooltip" data-placement="bottom" title="Edit Employee"  data-target="#exampleEdit">
<i class="fas fa-edit"></i>
</button>
<!--button delete-->
<button type="button" class="btn btn-danger" data-toggle="modal" data-tooltip="tooltip" data-placement="bottom" title="Delete Employee"  data-target="#exampleDelete">
<i class="fas fa-trash"></i>
 </button>
`;
$(document).ready(function () {
    $("#example1").DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": true,
        "responsive": true,
        "order": [[1, 'asc']],
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "ajax": {
            url: "https://localhost:7098/api/Employees/GetAllEmp",
            type: "GET",
            "datatype": "json",
            "dataSrc": "data",
            //success: function (result) {
            //    console.log(result)
            //}
        },
        "columns": [
            { "data": null },
            { "data": "fullName" },
            { "data": "email" },
            { "data": "gpa" },
            { "data": "university_name" },
            { "data": null }
        ],
        columnDefs: [
            {
                orderable: false,
                targets: [0], render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                orderable: false,
                targets: [5], render: function (data) {
                    return button;
                }
            },
        ]
    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
})

function save() {
    var employee = new Object();
    employee.firstName = $('#firstname').val();
    employee.lastName = $('#lastname').val();
    employee.birthDate = $('#birthdate').val();
    employee.salary = $('#salary').val();
    employee.email = $('#email').val();
    employee.gender = parseInt($('#gender').val());
    employee.password = $('#password').val();
    employee.degree = parseInt($('#degree').val());
    employee.gpa = $('#gpa').val();
    employee.university_id = parseInt($('#universitas').val());
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7098/api/Employees/register',
        data: JSON.stringify(employee),
        contentType: "application/json; charset=utf-8",
        debugger:
    }).then((result) => {
        if (result.status == 200) {
            alert(result.message)
        } else {
            alert("Data Gagal Ditambahkan")
        }
    })
}


