export function DateTimeDiffer(date1, date2){
    let diff = date2 - date1;
    diff = diff/1000;
    if(diff < 60)
        return 'Недавно';
    diff = diff/60;
    if(diff < 60)
        return NRound(diff) + ' минут назад';
    diff = diff/60;
    diff = diff/24;
    if (diff < 4 && date1.getDate() === new Date(Date.now()).getDate())
        return 'сегодня в ' + date1.getHours() + ':' + date1.getMinutes();
    if(diff < 4 && DateIsOneDayDiff(date1, new Date(Date.now())))
        return 'вчера в ' + date1.getHours() + ':' + date1.getMinutes();
    //if(diff >= 2)
        return DateFormat(date1);
}

export function NRound(num){
    return Math.round(num);
}

function DateIsOneDayDiff(date1, date2){
    const d1d = date1.getDate();
    const d2d = date2.getDate();
    const d1m = date1.getMonth();
    const d2m = date2.getMonth();
    const d1y = date1.getFullYear();
    const d2y = date2.getFullYear();
    ///Не дописано, обрабатывает не все случаи, нужно как-то переработать
    if(d1d + 1 === d2d)
        return true;
    else
        return false;
}

export function DateFormat(date, form){
    //let date = Date(datestr);

    let year = date.getFullYear();
    let month = date.getMonth() + 1;
    let days = date.getDate();

    let hours = date.getHours();
    let minutes = date.getMinutes();
    let seconds = date.getSeconds();

    switch (month) {
        case 1:
            month = 'января';
            break;
        case 2:
            month = 'февраля';
            break;
        case 3:
            month = 'марта';
            break;
        case 4:
            month = 'апреля';
            break;
        case 5:
            month = 'мая';
            break;
        case 6:
            month = 'июня';
            break;
        case 7:
            month = 'июля';
            break;
        case 8:
            month = 'августа';
            break;
        case 9:
            month = 'сентября';
            break;
        case 10:
            month = 'октября';
            break;
        case 11:
            month = 'ноября';
            break;
        case 12:
            month = 'декабря';
            break;
    }

    return days + ' ' + month + ' ' + year + ' в ' + hours + ':' + minutes+ ":" + seconds;
}