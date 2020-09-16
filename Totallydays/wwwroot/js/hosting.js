$(document).ready(function () {

    class HostingImageClass {

        submitImage = () => {
            $("form#form-image input#Image").on("change", (e) => {
                let $el = $(e.currentTarget)[0];
                let files = $el.files
                let formData = new FormData();
                for (var i = 0; i != files.length; i++) {
                    formData.append("Image", files[i]);
                }

                formData.append("AntiforgeryFieldname", $("input[name=\"AntiforgeryFieldname\"]").val())

                let toto = "ezerz";
                $.ajax({
                    type: "POST",
                    url: urlImage,
                    data: formData,
                    processData: false,
                    contentType: false,
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

        deleteImage = () => {
            $("div.remove-image").on("click", (e) => {
                let $el = $(e.currentTarget)[0];
                let id = $($el).data("id");

                let forgery = $("input[name=\"AntiforgeryFieldname\"]").val();

                let datas = {
                    idImage: id,
                    AntiforgeryFieldname: forgery
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
                let forgery = $("input[name=\"AntiforgeryFieldname\"]").val();
                datas += "&AntiforgeryFieldname=" + forgery;
                $.ajax({
                    type: "POST",
                    url: urlLinkEquipment,
                    data: datas,
                    dataType: "json",
                    success: (result) => {

                    },
                    error: (err) => {

                    }
                })

            })
        }

        changeForm = () => {
            $(".btn-hosting-creation-next-equipment").on("click", (e) => {
                $(".hosting-creation-equipment").show();
                $(".hosting-creation-image").hide();
            })

            $(".btn-hosting-creation-previous-image").on("click", (e) => {
                $(".hosting-creation-equipment").hide();
                $(".hosting-creation-image").show();
            })
        }
    }

    let HostingImage = new HostingImageClass();

    HostingImage.submitImage();
    HostingImage.deleteImage();
    HostingImage.changeForm();
    HostingImage.submitEquipment();
})