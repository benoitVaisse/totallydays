import { transformDate } from './site.js';
export class DateBooking {

    constructor(dates,amount) {
        this.jsDateUnavailable = dates
        this.amount = amount
        if (this.jsDateUnavailable) {
            this.setCalendarUnavalableDate(this.jsDateUnavailable);
            this.addEventDatePicke();
            this.showModal();
            this.deleteUnavailableDate();

        }
    }
           

    setCalendarUnavalableDate(dates){

        $("#booking_startDate, #booking_endDate").datepicker({
            format: "dd/mm/yyyy",
            datesDisabled: dates,
            startDate: new Date()
        });
    }

    addEventDatePicke(){

        $("#booking_startDate, #booking_endDate").on("changeDate",function (e) {

            const DAYS_TIME = 24 * 60 * 60 * 1000;
            const startDate = $("#booking_startDate td.day.active").data("date");
            const endDate = $("#booking_endDate td.day.active").data("date");
            let DateStart = new Date(startDate);
            let Dateend = new Date(endDate);
            if (DateStart instanceof Date) {
                $("input#Start_date").val(transformDate(DateStart));
            }
            if (Dateend instanceof Date) {
                $("input#End_date").val(transformDate(Dateend));
            }
            let days = (endDate - startDate) / DAYS_TIME;
            amount = days * this.amount;
            if (startDate && endDate && startDate < endDate) {
                $("#days").text(days);
                $("#amount").text(amount);
                $("#error").hide();
                $("#make_booking").removeAttr("disabled");
            }
            else if(!startDate < endDate && startDate && endDate) {
                $("#days").text("...");
                $("#amount").text("0");
                $("#error").show();
                $("#make_booking").attr("disabled", "disabled");
            }
        });
    }

    showModal(){
        $("#modal_unavailable_date").on("click", (e) => {
            let $id_modal = $(e.currentTarget).data("target");
            $($id_modal).modal('show', (e) => { })
        })

    }

    deleteUnavailableDate(){

        $(".deleteUnavailableDate").on("click", (e) => {
            $.ajax({
                type: "POST",
                url: $(e.currentTarget).data("href"),
                data: {
                    __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val()
                },
                dataType: "json",
                success: (result) => {
                    if (result.status == "success") {
                        let body = $("#modal_unavailable_date_block").find(".modal-body")[0];
                        $(body).html(result.view);
                        $("#booking_startDate, #booking_endDate").datepicker("destroy");
                        this.setCalendarUnavalableDate(result.unvavalibledate);
                        this.addEventDatePicker();
                    }
                },
                error: (err, result, message)=>{

                }

            })
        })
    }

}