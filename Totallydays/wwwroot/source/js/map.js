
export default class MapClass {

    /**
     * initilize map and autocomplete city
     * 
     */
    constructor() {
        this.countryRestrict = { 'country': 'fr' };
        this.autocomplete = new google.maps.places.Autocomplete(
            document.getElementById('City'), {
            types: ['(cities)'],
            componentRestrictions: this.countryRestrict
        });
        this.createAutocompleteCity();

        this.initMap();
        

    }


    onCitySearchChanged(){
        var place = this.autocomplete.getPlace();
        $('.city').value(place.name);

}

    createAutocompleteCity(){ 
        this.autocomplete.addListener("place_changed", this.onCitySearchChanged);
    }
    
    initMap() {
        if (document.getElementById('map')) {
            let lat = $("#map").data("lat");
            let lng = $("#map").data("lng");
            let zoom = $("#map").data("zoom")

            if (typeof lat == "string")
                lat = this.replace(lat);
            if (typeof lng == "string")
                lng = this.replace(lng);
            if (typeof zoom == "string")
                zoom = this.replace(zoom);

            this.map = new google.maps.Map(document.getElementById('map'), {
                zoom: zoom,
                center: { lat: lat, lng: lng }
            });
            this.createMarker();

        }
    }
    replace(value) {
        return parseFloat(value.replace(",", "."));
    }
    createMarker() {
        $(".coordinate-hosting").each((i, e) => {

            let lat_hosting = parseFloat($(e).data("lat"));
            let lng_hosting = parseFloat($(e).data("lng"));
            if (typeof lat_hosting == "string")
                lat_hosting = this.replace(lat);
            if (typeof lng_hosting == "string")
                lng_hosting = this.replace(lng);
            let title = $(e).data("title");
            var marker = new google.maps.Marker({
                position: { lat: lat_hosting, lng: lng_hosting },
                map: this.map,
                title: title
            })
        })
    }
}
