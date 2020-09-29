$(document).ready(() => {

    let countryRestrict = { 'country': 'fr' };
    let autocomplete = new google.maps.places.Autocomplete(
        document.getElementById('City') , {
        types: ['(cities)'],
        componentRestrictions: countryRestrict
    });

    autocomplete.addListener('place_changed', onCitySearchChanged);

    function onCitySearchChanged(){
        var place = autocomplete.getPlace();
        $('.city').value(place.name);

    }

    

})