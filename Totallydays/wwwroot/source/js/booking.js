import { showResult } from "./site.js";

export default class BookingClass {

        constructor() {
            this.showModal();
            this.submitComment();
        }
        showModal(){

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

        submitComment(){
            $("#form-booking-comment").on("submit", (e) => {
                e.preventDefault();
                let data = $(e.currentTarget).serialize();

                $.ajax({
                    type: "POST",
                    url: urlCommentSubmit,
                    data: data,
                    dataType: "json",
                    success: (result) => {
                        showResult(result);
                        if (result.status == "success") {
                            $("#submit-comment-" + $("#BookingId").val()).remove();
                        }
                    },
                    error: (err) => {

                    },
                    complete: () => {
                        $("#booking_comment_modal_lg").modal("hide");
                    }
                })
            })
        }

    }
