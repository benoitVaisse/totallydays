
export function showResult(result){
    let $prototype = $("#prototype-flash")[0];
    let $prototypehtml = $($prototype).html();
    let message = "";
    for (const [key, value] of Object.entries(result.message)) {
        value.forEach((message) => {
            message = $prototypehtml.replace(/__message__/gi, message).replace("none", "block").replace(/__status__/gi, key);
            $($prototype).append(message);
        })

    }
}

export function transformDate(date){
    let day = date.getDate();
    day = day.length < 2 ? "0" + day : day;
    let month = date.getMonth() + 1;
    month = month.length < 2 ? "0" + month : month;
    let year = date.getFullYear();

    return [year, month, day].join("-");
}