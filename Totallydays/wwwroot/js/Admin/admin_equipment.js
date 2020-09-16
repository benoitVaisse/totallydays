$(document).ready(() => {

    // ----------------- equipment type --------------------------//

    class AdminEquipementTypeClass {

        submitForm = () => {

            $("form#create_equipment_type").on("submit", (e) => {
                e.preventDefault();
                let formEl = $(e.currentTarget);
                console.log(formEl);
                let datas = formEl.serialize();
                console.log(datas);

                $.ajax({
                    type: "POST",
                    url: urlAjaxCreateEquipmentType,
                    data: datas,
                    dataType: "json",
                    success: (result) => {
                        $(".list-e-type").html(result.view);
                        this.resetForm(formEl);
                        this.searchEquipment();
                    },
                    error: (err) => {
                        alert("fail");
                    }
                })
            })
        }

        searchEquipment = () => {
            $(".e-type-single").on("click", (e) => {

                let el = $(e.currentTarget);
                let id = el.data("id");

                $.ajax({
                    type: "POST",
                    url: urlAjaxEditEquipmentType,
                    data: {
                        id:id
                    },
                    dataType: "json",
                    success: (result) => {
                        if (result.status = "success") {
                            console.log(result.model);
                            $('form#create_equipment_type').find("#Equipment_type_id").val(result.model.equipment_type_id);
                            $('form#create_equipment_type').find("#Name").val(result.model.name);
                        } else {
                            alerte("fail");
                        }
                    },
                    error: (err) => {
                        alert("error");
                    }
                })
            })
        }

        resetForm = (form) => {
            form[0].reset();
            form.find("#Equipment_type_id").val("");
        }

    }

    class AdminEquipmentClass {

        submitForm = () => {
            $("#create_equipment").on("submit", (e) => {
                e.preventDefault();
                let formEl = $(e.currentTarget);
                let datas = formEl.serialize();
                $.ajax({
                    type: "POST",
                    url: urlAjaxCreateEquipment,
                    data: datas,
                    dataType: "json",
                    success: (result) => {
                        $(".list-e").html(result.view);
                        this.resetForm(formEl);
                        this.searchE();
                    },
                    error: (err)=> {
                        alert("erreur");
                        console.log(err);
                    }
                })
            })
        }

        searchE = () => {
            $(".e-single").on("click", (e) => {
                let el = $(e.currentTarget);
                let type_id = el.parent().data("parent");
                $.ajax({
                    type: "POST",
                    url: urlAjaxEditEquipment,
                    data: {
                        id: el.data("id")
                    },
                    dataType: "json",
                    success: (result) => {
                        this.deselectOption();
                        $("form#create_equipment").find("#Equipment_id").val(result.equipment.equipment_id);
                        $("form#create_equipment").find("#Name").val(result.equipment.name);
                        $("form#create_equipment").find("option#option-eq-t-"+type_id).attr("selected", "selected");
                    },
                    error: (err) => {

                    }
                })
            })
        }

        deselectOption = () => {
            $("form#create_equipment option").each((i,e) => {
                $(e).removeAttr("selected");
            })
        }

        resetForm = (formEl) => {
            formEl[0].reset();
            formEl.find("#Equipment_id").val("");
        }
    }


    let AdminEquipementType = new AdminEquipementTypeClass();
    AdminEquipementType.submitForm();
    AdminEquipementType.searchEquipment();

    let AdminEquipment = new AdminEquipmentClass();
    AdminEquipment.submitForm();
    AdminEquipment.searchE();
})