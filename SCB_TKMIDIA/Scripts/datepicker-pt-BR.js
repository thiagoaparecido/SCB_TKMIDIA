/* Brazilian initialisation for the jQuery UI date picker plugin. */

/* Written by Leonildo Costa Silva (leocsilva@gmail.com). */

(function (factory) {

    if (typeof define === "function" && define.amd) {



        // AMD. Register as an anonymous module.

        define(["../widgets/datepicker"], factory);

    } else {



        // Browser globals

        factory(jQuery.datepicker);

    }

}(function (datepicker) {



    datepicker.regional["pt-BR"] = {

        closeText: "Fechar",

        prevText: "&#x3C;Anterior",

        nextText: "Próximo&#x3E;",

        currentText: "Hoje",

        monthNames: ["Janeiro", "Fevereiro", "Marco", "Abril", "Maio", "Junho",

        "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],

        monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun",

        "Jul", "Ago", "Set", "Out", "Nov", "Dez"],

        dayNames: [

            "Domingo",

            "Segunda-feira",

            "Terça-feira",

            "Quarta-feira",

            "Quinta-feira",

            "Sexta-feira",

            "Sabado"

        ],

        dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],

        dayNamesMin: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],

        weekHeader: "S",

        dateFormat: "dd/mm/yyyy",

        firstDay: 1,

        isRTL: false,

        showMonthAfterYear: false,

        yearSuffix: ""
    };

    datepicker.setDefaults(datepicker.regional["pt-BR"]);



    return datepicker.regional["pt-BR"];



}));