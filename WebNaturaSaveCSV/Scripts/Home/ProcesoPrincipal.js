// Proceso Principal de la APP

function generarDataTables() {

    $('#tablaProcesos thead th').each(function () {
        var title = $('#tablaProcesos tfoot th').eq($(this).index()).text();
        $(this).html("<input type='text' placeholder='Buscar " + title + "' />");
    });

    var tablaDataTable = $("#tablaProcesos").DataTable({
        //"dom": 'Bfrtip',
        "buttons": [
            /*'copy',*/ 'csv', 'excel', 'pdf', 'print'
        ],
        "language": {
            "processing": "Procesando...",
            "lengthMenu": "Mostrar _MENU_ registros",
            "zeroRecords": "No se encontraron resultados",
            "emptyTable": "Ningún dato disponible en esta tabla",
            "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "infoPostFix": "",
            "search": "Buscar:",
            "url": "",
            "infoThousands": ",",
            "loadingRecords": "Cargando...",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        "fixedHeader": {
            header: true,
            footer: false
        },
        "search": {
            "smart": true
        },
        "columns": [
            { data: "Archivo_Procesado", sortable: true },
            { data: "Ruta_Archivo" },
            { data: "Fecha_Incial" },
            { data: "Ultima_Modificacion" },
            { data: "Descripcion", width: "50%" },
            { data: "Estado_Final" },
            {
                data: "Estado_escarga_FTP",
                render: function (data, type, row) {
                    if (data === 'True') {
                        return "<button class='btn btn-success' disabled><i class='fa fa-download'></i></button >";
                    } else {
                        return "<button class='btn btn-danger' disabled><i class='fa fa-exclamation-triangle'></i></button >";
                    }
                    return data;
                },
                className: "dt-body-center",
                width: "10%"
            },
            {
                data: "Estado_Codificacion",
                render: function (data, type, row) {
                    if (data === 'True') {
                        return "<button class='btn btn-info' disabled><i class='fa fa-code'></i></button >";
                    } else {
                        return "<button class='btn btn-danger' disabled><i class='fa fa-exclamation-triangle'></i></button >";
                    }
                    return data;
                },
                className: "dt-body-center"
            },
            {
                data: "Estado_BulkSQL",
                render: function (data, type, row) {
                    if (data === 'True') {
                        return "<button class='btn btn-warning' disabled><i class='fa fa-database'></i></button >";
                    } else {
                        return "<button class='btn btn-danger' disabled><i class='fa fa-exclamation-triangle'></i></button >";
                    }
                    return data;
                },
                className: "dt-body-center"
            }
        ],
        "pageLength": 10,
        "destroy": true,
        "stateSave": false,
        "initComplete": function () {
            var r = $('#tablaProcesos tfoot tr');
            r.find('th').each(function () {
                $(this).css('padding', 8);
            });
            $('#tablaProcesos thead').append(r);
            $('#search_0').css('text-align', 'center');
        },
        "autoWidth": true,
        "pagingType": "full_numbers"
    });

    tablaDataTable.columns().eq(0).each(function (colIdx) {
        $('input', tablaDataTable.column(colIdx).header()).on('keyup change', function () {
            tablaDataTable.column(colIdx).search(this.value).draw();
        });
    });

}

function mostrarDetalleErrorHTTP() {
    var textAreaError = document.getElementById('txaMensajeErrorCompleto');
    if (textAreaError.style.display === 'none') {
        textAreaError.style.display = "block";
    } else {
        textAreaError.style.display = 'none';
    }
}

function mostrarTXT(logSolicitado) {

    var iframeSolicitado;

    if (logSolicitado == 'ERROR') {
        iframeSolicitado = '<iframe src="../LogsProcesos/LogError.txt" width="1000" height="500"></iframe>';
    } else if (logSolicitado == "PRINCIPAL") {
        iframeSolicitado = '<iframe src="../LogsProcesos/LogProcesoPrincipal.txt"  width="1000" height="500"></iframe>';
    } else if (logSolicitado == "BULK") {
        iframeSolicitado = '<iframe src="../LogsProcesos/LogBulksSQL.txt" width="1000" height="500"></iframe>';
    }

    swal({
        title: 'Log - ' + logSolicitado,
        type: 'info',
        text: iframeSolicitado,
        html: true,
        closeOnCancel: false,
        closeOnConfirm: false,
        showconfirmbuton: true,
        customClass: 'swal-wide',
    }, function (isConfirm) {
        if (isConfirm) {
            location.reload();
        }
    });
}