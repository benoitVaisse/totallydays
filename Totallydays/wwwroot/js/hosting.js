$(document).ready(function () {

    class MyHostingClass {

        submitImage = () => {
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
                        this.showResult(result);
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

        deleteImage = () => {
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

        submitEquipment = () => {
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
                        this.showResult(result);
                    },
                    error: (err) => {

                    }
                })

            })
        }

        changeStatusPublished = () => {
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

        changeForm = () => {
            $(".hosting-creation").on("click", (e) => {
                let idShow = $(e.currentTarget).data("target");
                let idHide = $(e.currentTarget).data("parent");
                $(idShow).show();
                $(idHide).hide();
            })
        }

        addBedRomm = () => {
            $("#number_bed").on("change", (e) => {
                let value = $(e.currentTarget).val();
                if (!isNaN(value)) {
                    let $prototype = $("#prototype")[0];
                    if (value > numberbed) {
                        $("#block-bed").append($($prototype).html().replace(/__number__/gi, value).replace(/__bedroom__/gi, value - 1));
                        numberbed = value;

                    } else {
                        $(".bed_hosting.bed-" + numberbed).remove();
                        numberbed = value;
                    }
                }
            })
        }

        submitBedroom = () => {
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
                        this.showResult(result);
                    },
                    error: (err) => {

                    }
                })
            })
        }

        showResult = (result) => {
            let $prototype = $("#prototype-flash")[0];
            let $prototypehtml = $($prototype).html();
            let message = "";
            for(const [key, value] of Object.entries(result.message)) {
                value.forEach((message) => {
                    message = $prototypehtml.replace(/__message__/gi, message).replace("none", "block").replace(/__status__/gi, key);
                    $($prototype).append(message);
                })
                
            }
        }

    }

    let numberbed = $("#number_bed").val();

    let MyHosting = new MyHostingClass();

    MyHosting.submitImage();
    MyHosting.deleteImage();
    MyHosting.changeForm();
    MyHosting.submitEquipment();
    MyHosting.changeStatusPublished();
    MyHosting.addBedRomm();
    MyHosting.submitBedroom();
})