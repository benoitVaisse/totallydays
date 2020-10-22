$(document).ready(() => {

    class BookingClass {

        showModal = () => {

            $(".booking-comment").on("click", (e) => {
                $("#booking_comment_modal_lg").modal("show");
            })
        }

    }

    let BookingObject = new BookingClass();
    BookingObject.showModal();
})