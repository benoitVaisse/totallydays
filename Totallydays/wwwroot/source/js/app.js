import jQuery from "jquery";
import "bootstrap";
window.$ = window.jQuery = jQuery;

import { showResult, transformDate } from "./site.js"
import MapClass from "./map.js";
import accountClass from "./account.js"
import BookingClass from "./booking.js"
import MyHostingClass from "./hosting.js"
import { DateBooking } from "./dateBooking";

$(document).ready(() => {
    let mapAutocomplete = new MapClass();
    let accountObject = new accountClass();
    let bookingObject = new BookingClass();
    let MyHostingObject = new MyHostingClass();
    let DateBookingObject = new DateBooking(jsDateUnavailable, amount)
})