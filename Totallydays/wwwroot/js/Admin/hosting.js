$(document).ready(() => {

    class HostingAdminClass {

        activeHosting = () => {

            $(".active_hosting").on("change", (e) => {
                let formData = new FormData();
                let datas = {
                    id: $(e.currentTarget).data("id"),
                    active: $(e.currentTarget).prop("checked"),
                    __RequestVerificationToken: $("input[name=\"__RequestVerificationToken\"]").val()
                };

                let url = $(e.currentTarget).data("href");

                $.ajax({
                    type: "POST",
                    url: url,
                    data: datas,
                    dataType: "json",
                    success: (result)=>{
                        this.showResult(result.message)
                    },
                    error:(resultat, statut, erreur)=> {

                    }
                })
            })

        }
        showResult = (messages) => {
            let $prototype = $("#prototype-flash")[0];
            let $prototypehtml = $($prototype).html();
            let message = "";
            for (const [key, value] of Object.entries(messages)) {
                value.forEach((message) => {
                    message = $prototypehtml.replace(/__message__/gi, message).replace("none", "block").replace(/__status__/gi, key);
                    $($prototype).append(message);
                })

            }
        }

    }

    let HostingAdminObject = new HostingAdminClass();
    HostingAdminObject.activeHosting();

})