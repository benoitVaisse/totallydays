$(document).ready(() => {

    class BookingClass {

        showModal = () => {

            $(".booking-comment").on("click", (e) => {
                let $title = $(e.currentTarget).data("title");
                let $id = $(e.currentTarget).data("id");
                let $date = $(e.currentTarget).data("sdate") + " " + $(e.currentTarget).data("edate");

                $("#modal-booking-title").html($title);
                $("#modal-booking-date").html($date);
                $("input#BookingId").val($id);
                $("#booking_comment_modal_lg").modal("show");
            })
        }

    }

    let BookingObject = new BookingClass();
    BookingObject.showModal();
})