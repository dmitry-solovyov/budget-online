﻿
// ************ ON READY

$(document).ready(function () {
    try { turnOn_datetime(); } catch (z) { }
    try { turnOn_numeric(); } catch (z) { }
    //turnOn_calculator();
    try { $('.selectpicker').selectpicker(); } catch (z) { }
    try { turnOn_dynamicBox(); } catch (z) { }
});

// ************ FUNCTIONS

function turnOn_numeric() {
    $('.data-type-numeric').keypress(
		function (event) {
		    //debugger;
		    var allowedKeys = [37, 39, 8, 32, 9, 36, 35, 45, 46];
		    var regex = new RegExp("^[0-9\,\.]+$");
		    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
		    if (!(event.ctrlKey || event.metaKey)
				&& allowedKeys.indexOf(event.keyCode) == -1
				&& key.length > 0 && !regex.test(key)) {
		        event.preventDefault();
		        return false;
		    }

		    return true;
		});
}

function turnOn_datetime() {
    $('.date-picker').datepicker({
        dateFormat: "dd.mm.yy",
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true
    });
}

function turnOn_dynamicBox() {
    //dyn - box

    $('div.dyn-box .refresh').click(
		function (event) {
		    event.preventDefault();
		    var container = $(this).parent().parent().parent();

		    var url = container.attr('data-dynamic-url');

		    if (url && url.length > 0) {
		        showIndicatorFor(container.find('.content'), 'progress');

		        var jqxhr = $.ajax({
		            url: url,
		            type: "GET",
		            cache: false,
		            dataType: "json"
		        })
				.done(function (response) {
				    container.find('div.content').html(response.Data.Content);
				    container.find('div.footer span').html(response.Data.UpdateTime);
				})
				.fail(function () {
				    //alert("Error");
				})
				.always(function () {
				    hideIndicatorFor($(this).parent().parent().parent().find('.content'), 'progress');
				});
		    }

		    return true;
		});

    $('div.dyn-box .refresh').each(function () {
        var container = $(this).parent().parent().parent();
        var autoload = container.attr('data-autoload');
        var autoloaddelay = container.attr('data-autoload-delay');
        if (autoload === 'true') {
            var btn = $(this);
            setTimeout(function () { btn.click(); }, parseInt(autoloaddelay));
        }
    });
}

function turnOn_calculator(id) {
    if (typeof (id) == undefined)
        return;

    $('#' + id + ' input.data-type-calculator').keypress(
		function (event) {
		    var allowedKeys = [37, 39, 8, 32, 9, 36, 35, 45, 46];
		    var regex = new RegExp("^[0-9+-/\*]+$");
		    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
		    if (!(event.ctrlKey || event.metaKey)
				&& allowedKeys.indexOf(event.keyCode) == -1
				&& key.length > 0 && !regex.test(key)) {
		        event.preventDefault();
		        return false;
		    }

		    return true;
		});

    $('#' + id + ' input.data-type-calculator').bind("keyup change",
		function (event) {
		    var target = $('#' + id + ' .calculator-result');
		    if (typeof (target) == undefined)
		        return;
		    var errorText = '#ошибка';

		    try {
		        var txt = $(this).val();
		        if (txt.length > 1 && new RegExp("^[+-/\*]+$").test(txt.substr(txt.length - 1)))
		            txt = txt.substring(0, txt.length - 2);
		        if (txt.length > 1 && new RegExp("^[/\*]+$").test(txt.substring(0, 1)))
		            txt = txt.substring(1, txt.length - 2);

		        var expr = eval(txt);
		        if (typeof (expr) != undefined)
		            target.val(expr);
		        else
		            target.val(errorText);
		    } catch (z) {
		        target.val(errorText);
		    }
		}
	);
}



function clearForm(form) {
    $(':input', form).each(function () {
        var type = this.type;
        var tag = this.tagName.toLowerCase();

        if (type == 'text' || type == 'password' || tag == 'textarea')
            this.value = "";

        else if (type == 'checkbox' || type == 'radio')
            this.checked = false;

        else if (tag == 'select')
            this.selectedIndex = -1;
    });
};

function showIndicatorFor(container, id) {
    var newIndicator = $(container).find('.progress-indicator');
    if (newIndicator == null || newIndicator.length == 0)
        newIndicator = $('.progress-indicator').clone();

    newIndicator.attr('id', id);

    newIndicator.css('width', container.width());
    if (container.height() < 20) {
        newIndicator.css('height', 20);
    } else
        newIndicator.css('height', container.height());

    $(container).prepend(newIndicator);

    newIndicator = $(container).find('.progress-indicator');

    newIndicator.removeClass('hidden');
}

function hideIndicatorFor(container, id) {
    container.find('.progress-indicator').remove();
}

function changeLang(lang) {
    $.cookie('lang', lang);
    window.location.reload();
    return false;
}

function loadDataWithUI(url, template) {
    var jqxhr = $.ajax({
        url: url,
        type: "GET",
        cache: false,
        dataType: "json"
    })
    .done(function (response) {
        alert(response);
    })
    .fail(function () {

    })
    .always(function () {

    });
}