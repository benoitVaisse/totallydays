import jQuery from "jquery";
import "bootstrap";
window.$ = window.jQuery = jQuery;

import MapClass from "./map.js";
import accountClass from "./account.js"
import {showResult} from "./site.js"
$(document).ready(() => {

    let mapAutocomplete = new MapClass();
    let accountObject = new accountClass();
})