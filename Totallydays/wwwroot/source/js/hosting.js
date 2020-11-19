import { showResult } from "./site.js";

export default class MyHostingClass {
        constructor() {

            this.numberbed = $("#number_bed").val();
            this.submitImage();
            this.deleteImage();
            this.changeForm();
            this.submitEquipment();
            this.changeStatusPublished();
            this.addBedRomm();
            this.submitBedroom();
        }
        submitImage(){
            $("form#form-image input#Image").on("change", (e) => {
                let $el = $(e.currentTarget)[0];
                let files = $el.files
                let formData = new FormData();
                for (var i = 0; i != files.length; i++) {
                    formData.append("Image", files[i]);
                }

                formData.append("__RequestVerificationToken", $("input[name=\"__RequestVerificationToken\"]").val())

                $.ajax({
                    type: "POST",
                    url: urlImage,
                    data: formData,
                    processData: false,
                    contentType: false,
                    dataType: "json",
                    success: (result) => {
                        showResult(result);
                        if (result.status == "success") {
                            
                            $("#liste-image-hebergement-create").html(result.view);
                            this.deleteImage();

                        }
                    },
                    error: (err) => {

                    }
                })
            })
        }

        deleteImage(){
            $("div.remove-image").on("click", (e) => {
                let $el = $(e.currentTarget)[0];
                let id = $($el).data("id");

                let forgery = $("input[name=\"__RequestVerificationToken\"]").val();

                let datas = {
                    idImage: id,
                    __RequestVerificationToken: forgery
                };

                $.ajax({
                    type: "POST",
                    url: urlImageDelete,
                    data: datas,
                    dataType: "json",
                    success: (result) => {
                        $("#liste-image-hebergement-create").html(result.view);
                        this.deleteImage();
                    },
                    error: (err) => {

                    }
                })
            })
        }

        submitEquipment(){
            $("form#hosting-equipment").on("submit", (e) => {
                e.preventDefault();
                let $elForm = $(e.currentTarget)[0];
                let datas = $($elForm).serialize();
                let forgery = $("input[name=\"__RequestVerificationToken\"]").val();
                datas += "&__RequestVerificationToken=" + forgery;
                $.ajax({
                    type: "POST",
                    url: urlLinkEquipment,
                    data: datas,
                    dataType: "json",
                    success: (result) => {
                        showResult(result);
                    },
                    error: (err) => {

                    }
                })

            })
        }

        changeStatusPublished(){
            $("input.input-published").on("click", (e) => {
                
                let publish = $(e.currentTarget).prop("checked");
                let id = $(e.currentTarget).data("id");
               

                $.ajax({
                    type: "POST",
                    data: {
                        id: parseInt(id),
                        publish: publish,
                        __RequestVerificationToken: $("input[name=\"__RequestVerificationToken\"]").val()
                    },
                    url: urlPublished,
                    dataType: "json",
                    success : (result) => {
                    },
                    error: (err) => {
                    }
                })
            })
        }

        changeForm(){
            $(".hosting-creation").on("click", (e) => {
                let idShow = $(e.currentTarget).data("target");
                let idHide = $(e.currentTarget).data("parent");
                $(idShow).show();
                $(idHide).hide();
            })
        }

        addBedRomm(){
            $("#number_bed").on("change", (e) => {
                let value = $(e.currentTarget).val();
                if (!isNaN(value)) {
                    let $prototype = $("#prototype")[0];
                    if (value > this.numberbed) {
                        $("#block-bed").append($($prototype).html().replace(/__number__/gi, value).replace(/__bedroom__/gi, value - 1));
                        this.numberbed = value;

                    } else {
                        $(".bed_hosting.bed-" + this.numberbed).remove();
                        this.numberbed = value;
                    }
                }
            })
        }

        submitBedroom(){
            $("form#hosting_bed_form").on("submit", (e) => {
                e.preventDefault();
                let elForm = $(e.currentTarget)[0];
                let $elForm = $(elForm);
                let datas = $elForm.serialize();
                $.ajax({
                    type: "POST",
                    url: $elForm.attr("action"),
                    data: datas,
                    dataType: "json",
                    success: (result) => {
                        showResult(result);
                    },
                    error: (err) => {

                    }
                })
            })
        }

    }

    
    