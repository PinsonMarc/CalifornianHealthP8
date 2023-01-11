$(document).ready(function () {
    var selectedConsultant;
    $("#consultantSelect").change(function () {
        selectedConsultant = $(this).val();
        var optionsForSelected = window.dates.find(item => item.consultant.id == selectedConsultant);

        $('#dateSelect, #appointmentSelect').empty();
        $('#dateSelect').append($('<option>', {
            text: " - Please select -"
        }));
        $.each(optionsForSelected.dates, function (index, value) {
            $('#dateSelect').append($('<option>', {
                value: value,
                text: new Date(value).toLocaleDateString()
            }));
        });
    });
    $("#dateSelect").change(function () {
        var selectedOption = $(this).val();
        $.ajax({
            url: '/Booking/ConsultantAppointments',
            type: 'POST',
            data: {
                ConsultantId: selectedConsultant,
                Day: selectedOption
            },
            success: function (res) {
                var data = JSON.parse(res);
                $('#appointmentSelect').empty();
                data.forEach(function (value) {
                    var option = document.createElement("option");
                    option.value = value.id;
                    option.text = new Date(value.startDateTime).toLocaleTimeString();
                    document.getElementById("appointmentSelect").appendChild(option);
                });
            }
        });
    })

    $("#submit-appointment").click(function () {
        var selectedAppointment = $('#appointmentSelect').find(":selected").val();
        if (selectedAppointment == null) {
            $("#validation-text").html("Please select an appointment");
            return;
        }
        $.ajax({
            type: "POST",
            url: "/Booking/ConfirmAppointment",
            data: selectedAppointment,
            contentType: "application/json; charset=utf-8",
            success: function () {
                window.location = "/Booking/ConfirmAppointment";
            },
            error: function (error) {
                // Show an error message on error
                $("#validation-text").html(error.responseText);
            }
        })
    });
});
