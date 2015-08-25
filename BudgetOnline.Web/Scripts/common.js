'use strict';

var budgetGlobals = {
    settings: {
        dateFormat: 'DD.MM.YYYY',
        dateFormatUtc: 'DD.MM.YYYY UTC',
        timeFormat: 'HH:MM'
    }
};

//$.datepicker.setDefaults($.datepicker.regional['en']); 

// ************ ON READY

$(document).ready(function () {
    try { turnOn_datetime(); } catch (z) { }
    try { turnOn_numeric(); } catch (z) { }
    //turnOn_calculator();
    try { $('.selectpicker').selectpicker(); } catch (z) { console.log('selectpicker', z); }
    try { turnOn_dynamicBox(); } catch (z) { console.log('turnOn_dynamicBox', z); }
    try { $(function () {
         $('#language-switch ul.dropdown-menuli a').click(function (e) {
             e.preventDefault(); 
             var lang = $(this).attr('data-lang'); 
             changeLang(lang);
         });
    }); } catch (z) { console.log('turnOn_dynamicBox', z); }
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

var lastDynamicdatepicker;

function hideDynamicdatepicker() {
    if (lastDynamicdatepicker) {
        lastDynamicdatepicker.datepicker('hide').datepicker('destroy');
        $(lastDynamicdatepicker).remove();
        lastDynamicdatepicker = null;
    }
}

function turnOn_datetime() {

    $('.date-picker').each(function () {
        var control = $(this);
        control.datepicker({
            dateFormat: "dd.mm.yy",
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true
        });
        control.datepicker("option", $.datepicker.regional['ru']);

        var conrtainer = control.parent().next('div');

        conrtainer.find('*[data-direction="left"]').click(function (event) {
            event.preventDefault();

            var controlVal = control.val();
            if (!controlVal)
                controlVal = moment().format(budgetGlobals.settings.dateFormat);

            var dt = moment(controlVal, budgetGlobals.settings.dateFormat).subtract(1, 'd');

            control.val(dt.format(budgetGlobals.settings.dateFormat));
            control.datepicker("setDate", dt.toDate());
        });

        conrtainer.find('*[data-direction="right"]').click(function (event) {
            event.preventDefault();

            var controlVal = control.val();
            if (!controlVal)
                controlVal = moment().format(budgetGlobals.settings.dateFormat);

            var dt = moment(controlVal, budgetGlobals.settings.dateFormat).add(1, 'd');

            control.val(dt.format(budgetGlobals.settings.dateFormat));
            control.datepicker("setDate", dt.toDate());
        });

        conrtainer.find('span[data-select]').click(function (event) {
            event.preventDefault();
            control.datepicker('show');
        });

        var setDate = function (type) {
            var dt = moment();
            switch (type) {
                case 'week-begin':
                    if (dt.isoWeekday() > 1) {
                        dt.subtract('days', dt.isoWeekday() - 1);
                    }
                    break;
                case 'month-begin':
                    if (dt.date() > 1) {
                        dt.subtract('days', dt.date() - 1);
                    }
                    break;
                case 'yesterday':
                    dt.subtract('days', 1);
                    break;

                case 'today':
                default:
            }

            control.val(dt.format(budgetGlobals.settings.dateFormat));
        };

        conrtainer.find('div.navigation-helper button[data-dt-autofill], div.navigation-helper a[data-dt-autofill]').click(function (event) {
            event.preventDefault();
            setDate(event.target.getAttribute('data-dt-autofill'));
        });
    });


    $('a[data-type="date-select"]').click(function (event) {
        event.preventDefault();

        var self = this;
        var targetId = $(self).attr('data-dtselect-target');

        if (lastDynamicdatepicker) {
            var isSame = lastDynamicdatepicker.attr('data-dtselect-target') == targetId;
            hideDynamicdatepicker();
            if (isSame)
                return;
        }

        var input = $(self).parent().find('#' + targetId);

        var newEntry = $('<div id="datepicker" class="unhidden" style="position: absolute; border: 2px solid darkgray; z-index: 999;"></div>');
        newEntry.attr('data-dtselect-target', targetId);
        newEntry.css('left', $(self).position().left);

        lastDynamicdatepicker = $(newEntry).insertAfter($(self));
        lastDynamicdatepicker.datepicker({
            inline: true,
            dateFormat: 'dd.mm.yy',
            onSelect: function (date) {
                input.val(date);
                self.innerHTML = date;
                hideDynamicdatepicker();
            },
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true
        });
        lastDynamicdatepicker.datepicker("option", $.datepicker.regional['ru']);


        if (input.val() !== '') {
            lastDynamicdatepicker.datepicker("setDate", input.val()).datepicker('show');
        } else
            lastDynamicdatepicker.datepicker('show');
    });

    $('a[data-type="date-deselect"]').click(function (event) {
        event.preventDefault();

        var self = this;

        var input = $(self).parent().find('#' + $(self).attr('data-dtselect-target'));
        if (input)
            input.val('');

        var label = $(self).parent().find('#' + $(self).attr('data-dtselect-displaytarget'));
        if (label)
            label.html($(self).attr('data-dtselect-default'));
    });
}

function turnOn_dynamicBox() {
    //dyn - box

    $('div.dyn-box .refresh').click(
		function (event) {
		    event.preventDefault();
		    var container = $(this).closest("div.dyn-box");

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
				    hideIndicatorFor($(this).closest(".content"), 'progress');
				});
		    }

		    return true;
		});

    $('div.dyn-box .refresh').each(function () {
        var container = $(this).closest("div.dyn-box");
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

