

export default class accountClass {

        constructor(){
            this.changePicture();
        }

        changePicture() {
            $("div#picture-account input[name=picture]").on("change", (e) => {
                let $el = $(e.currentTarget)[0];
                let files = $el.files
                let formData = new FormData();
                for (var i = 0; i != files.length; i++) {
                    formData.append("picture", files[i]);
                }

                formData.append("__RequestVerificationToken", $("input[name=\"__RequestVerificationToken\"]").val());

                $.ajax({
                    type: "POST",
                    url: url,
                    data: formData,
                    processData: false,
                    contentType: false,
                    dataType: "json",
                    success: (result) => {

                        $("#status-picture").removeClass();
                        if (result.status == "error") {
                            $("#status-picture").addClass("alert alert-danger");
                        } else {
                            $("#status-picture").addClass("alert alert-success");
                            $("#picture-block").html(result.view);
                            this.changePicture();
                        }

                        $("#status-picture").html(result.message)
                    },
                    error: (err) => {
                        alert("une erreur est survenu, veuillez réessayer ultérieurement");
                    }
                })
            })
        }
    }