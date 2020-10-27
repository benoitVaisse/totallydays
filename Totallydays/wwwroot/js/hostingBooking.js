$(document).ready(() => {

    class BookingClass {

        constructor() {
            this.showModal();
            this.submitStatus();
            this.tableBookingDatatable();
        }
        showModal = () => {

            $(".btn-hosting-booking").on("click", (e) => {
                let $id = $(e.currentTarget).data("id");
                let $date = $(e.currentTarget).data("sdate") + " " + $(e.currentTarget).data("edate");

                $("#modal-booking-date").html($date);
                $("input#BookingId").val($id);
                $("#hosting-booking-validation-lg").modal("show");
            })
        }

        submitStatus = () => {
            $("#form-hosting-booking-validated").on("submit", (e) => {
                e.preventDefault();
                let data = $(e.currentTarget).serialize();

                $.ajax({
                    type: "POST",
                    url: urlHostingBooking,
                    data: data,
                    dataType: "json",
                    success: (result) => {
                    },
                    error: (err) => {

                    },
                    complete: () => {
                        $("#booking_comment_modal_lg").modal("hide");
                    }
                })
            })
        }

        tableBookingDatatable = () => {
            $("#hosting-all-booking").DataTable({
                "language": {
                    "lengthMenu": "Afficher _MENU_ Réservations par page",
                    "zeroRecords": "Aucune données trouvées",
                    "info": "Page _PAGE_ sur _PAGES_",
                    "infoEmpty": "Aucune",
                    "infoFiltered": "(Filtrer sur _MAX_ Réservations)",
                    "paginate": {
                        "next": "Suivante",
                        "previous": "Précédente",
                    }
                }
            })
        }

    }

    let BookingObject = new BookingClass();
})