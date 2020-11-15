
export default class MapClass {

    constructor() {
        this.countryRestrict = { 'country': 'fr' };
        this.autocomplete = new google.maps.places.Autocomplete(
            document.getElementById('City'), {
            types: ['(cities)'],
            componentRestrictions: this.countryRestrict
        });
        this.createAutocompleteCity();

    }


    onCitySearchChanged(){
        var place = this.autocomplete.getPlace();
        $('.city').value(place.name);

}

    createAutocompleteCity(){ 
        this.autocomplete.addListener("place_changed", this.onCitySearchChanged);
    }
}
