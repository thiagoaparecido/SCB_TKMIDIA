function fnCalcSemanaCine() {

    //De quinta à quarta da próxima semana.
    var quinta = 4; //Quinta.
    var quarta = 3;
    var myDate = new Date();
    var dtIni;
    var dtFim;
    var hoje = 0;

    var month;
    var day;
    var year;

    hoje = myDate.getDay();

    if (hoje == quinta) {
        day = myDate.getDate();
        month = myDate.getMonth() + 1;
        year = myDate.getFullYear();
        dtIni = day + '/' + month + '/' + year;

        myDate.setDate(myDate.getDate() + 6);
        day = myDate.getDate();
        month = myDate.getMonth() + 1;
        year = myDate.getFullYear();
        dtFim = day + '/' + month + '/' + year;

    }
    else {
        if (hoje < quinta) {
            while (hoje != quinta) {
                myDate.setDate(myDate.getDate() - 1);
                hoje = myDate.getDay();
            }

            day = myDate.getDate();
            month = myDate.getMonth() + 1;
            year = myDate.getFullYear();
            dtIni = day + '/' + month + '/' + year;

            myDate.setDate(myDate.getDate() + 6);
            day = myDate.getDate();
            month = myDate.getMonth() + 1;
            year = myDate.getFullYear();
            dtFim = day + '/' + month + '/' + year;
        }
        else {
            if (hoje > quinta) {
                while (hoje != quarta) {
                    myDate.setDate(myDate.getDate() + 1);
                    hoje = myDate.getDay();
                }

                day = myDate.getDate();
                month = myDate.getMonth() + 1;
                year = myDate.getFullYear();
                dtFim = day + '/' + month + '/' + year;


                myDate.setDate(myDate.getDate() - 6);
                day = myDate.getDate();
                month = myDate.getMonth() + 1;
                year = myDate.getFullYear();
                dtIni = day + '/' + month + '/' + year;

            }

        }
    }

    var datas = new Array();
    datas[0] = dtIni;
    datas[1] = dtFim;

    return datas
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
    var textRange; var j = 0;
    tab = document.getElementById('prog_index'); // id of table

    for (j = 0 ; j < tab.rows.length ; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
    }
    else                 //other browser not tested on IE 11
    {
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
    }
    return (sa);
}

function goBack()
{
        window.history.back();
}