import jQuery from "jquery";
import "bootstrap";
window.$ = window.jQuery = jQuery;

import { showResult } from "./site.js"
import MapClass from "./map.js";
import accountClass from "./account.js"
import BookingClass from "./booking.js"
import MyHostingClass from "./hosting.js"

$(document).ready(() => {
    let mapAutocomplete = new MapClass();
    let accountObject = new accountClass();
    let bookingObject = new BookingClass();
    let MyHostingObject = new MyHostingClass();
})