import jQuery from "jquery";
import "bootstrap";
window.$ = window.jQuery = jQuery;

import MapClass from "./map.js";
import {showResult} from "./site.js"
$(document).ready(() => {

    let mapAutocomplete = new MapClass();
})