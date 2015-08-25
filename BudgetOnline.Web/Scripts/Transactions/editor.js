var transactionType;
var outSum, outCurrency, outAccount;
var inSum, inCurrency, inAccount;
var dt, form;
var container;
var checkPerformed;

function initialiseEditor(editorContainer) {
    if (editorContainer == null) return;
    container = editorContainer;

    dt = container.find('form #Date');
    form = container.find('form');

    outSum = container.find('form #out_sum_group #SumOut_Formula');
    outCurrency = container.find('form #out_sum_group #SumOut_Currency');
    outAccount = container.find('form #out_sum_group #SumOut_Account');

    inSum = container.find('form #in_sum_group #SumIn_Formula');
    inCurrency = container.find('form #in_sum_group #SumIn_Currency');
    inAccount = container.find('form #in_sum_group #SumIn_Account');

    var transactionTypeControl = container.find('form select#TransactionType');
    if (transactionTypeControl != null) {
        changeTransactionType(true);

        transactionTypeControl.change(function () {
            changeTransactionType(false);
        });
    }

    if (container.find('#entityRef').val() === '0') {
        dt.val('');
    }

    try {
        turnOn_validators();
    } catch (z) {
        console.log('turnOn_validators', z);
    }
}

function changeTransactionType(initial) {
    var type = container.find('select#TransactionType').find(":selected").val();

    if (type == 1) {
        container.find('#in_sum_group').removeClass('hidden');
        container.find('#out_sum_group').addClass('hidden');

        if (!initial) {
            container.find('#in_sum_group #SumIn_Formula').val(container.find('#out_sum_group #SumOut_Formula').val());
            container.find('#in_sum_group #SumIn_Sum').val(container.find('#out_sum_group #SumOut_Sum').val());
            container.find('#in_sum_group #SumIn_Currency').val(container.find('#out_sum_group #SumOut_Currency').val());
        }
    } else if (type == 2) {
        container.find('#out_sum_group').removeClass('hidden');
        container.find('#in_sum_group').addClass('hidden');

        if (!initial) {
            container.find('#out_sum_group #SumOut_Formula').val(container.find('#in_sum_group #SumIn_Formula').val());
            container.find('#out_sum_group #SumOut_Sum').val(container.find('#in_sum_group #SumIn_Sum').val());
            container.find('#out_sum_group #SumOut_Currency').val(container.find('#in_sum_group #SumIn_Currency').val());
        }
    } else if (type == 3 || type == 4) {
        container.find('#in_sum_group').removeClass('hidden');
        container.find('#out_sum_group').removeClass('hidden');
    }

    if (type == 3) {
        container.find('#in_sum_group #SumIn_Currency').parent().addClass('hidden');
        container.find('#in_sum_group #SumIn_Formula').parent().addClass('hidden');
        container.find('#in_sum_group #SumIn_Sum').parent().addClass('hidden');
    } else {
        container.find('#in_sum_group #SumIn_Currency').parent().removeClass('hidden');
        container.find('#in_sum_group #SumIn_Formula').parent().removeClass('hidden');
        container.find('#in_sum_group #SumIn_Sum').parent().removeClass('hidden');
    }
    if (type == 4) {
        container.find('#in_sum_group #SumIn_Account').attr('disabled', 'disabled');
        container.find('#out_sum_group #SumOut_Account').on("change", function () { accountChange(container); });
        accountChange(container);
    } else {
        container.find('#in_sum_group #SumIn_Account').removeAttr('disabled');
        container.find('#out_sum_group #SumOut_Account').off("change");
    }
}

var accountChange = function (container) {
    if (typeof container === 'undefined' || container == null)
        return;

    var val = container.find('#out_sum_group #SumOut_Account').val();
    container.find('#in_sum_group #SumIn_Account').val(val);
};

var separatePopupInitialized = false;

function SeparateInstance() {
    if (!separatePopupInitialized) {
        $('#separatePopup').on('shown', function () {
            $('#separatePopup #separateSum').focus();
        });

        $('#separatePopup .modal-footer button.btn-primary').on('click', function () {
            var sum = $('#separatePopup #separateSum').val();
            var url = $('#separatePopup button.btn-primary').attr('data-target-url');
            postSeparateData({ Url: url, Sum: sum });
        });

        separatePopupInitialized = true;
    }

    $('#separatePopup').modal('show');
}

function postSeparateData(params) {
    $.ajax({
        url: params.Url,
        type: "POST",
        data: {
            Sum: params.sum
        },
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json"
    })
            .done(function (response) {
                if (!response.Success) {
                    alert(response.ErrorMessage);
                } else {
                    $('#separatePopup').modal('hide');
                }
            })
            .fail(function () {

            })
            .always(function () {

            });
}

function turnOn_validators() {
    console.log('Turn on validators.');
    $('form').each(function () {
        var form = $(this);
        form.submit(function (event) {
            var isValid = true;

            try {
                var selects = form.find('select');
                for (var i = 0; i < selects.length; i++) {
                    var select = $(selects[i]);

                    if (select.attr('required')) {
                        var formGroup = select.closest(".form-group");

                        if (!formGroup || !formGroup.hasClass('hidden')) {
                            var v = select.find(":selected").val();
                            if (v == '0') {
                                console.log('Select not valid!. Id= ' + select.attr('id'));
                                isValid = false;
                                select.addClass('input-validation-error');
                            } else {
                                select.removeClass('input-validation-error');
                            }
                        }
                    }
                }

                var type = form.find('select#TransactionType').find(":selected").val();

                outSum.removeClass('input-validation-error');
                outCurrency.removeClass('input-validation-error');
                outAccount.removeClass('input-validation-error');

                inSum.removeClass('input-validation-error');
                inCurrency.removeClass('input-validation-error');
                inAccount.removeClass('input-validation-error');

                if (type == 2 || type == 3 || type == 4) {
                    if (!outSum.val()) {
                        console.log('Input not valid!. Id=' + outSum.attr('id'));
                        outSum.addClass('input-validation-error');
                        isValid = false;
                    }
                    if (outCurrency.find(":selected").val() == '0') {
                        console.log('Select not valid!. Id=' + outCurrency.attr('id'));
                        outCurrency.addClass('input-validation-error');
                        isValid = false;
                    }
                    if (outAccount.find(":selected").val() == '0') {
                        console.log('Select not valid!. Id=' + outAccount.attr('id'));
                        outAccount.addClass('input-validation-error');
                        isValid = false;
                    }
                }
                if (type == 1 || type == 3 || type == 4) {
                    if (inAccount.find(":selected").val() == '0') {
                        console.log('Select not valid!. Id=' + inAccount.attr('id'));
                        inAccount.addClass('input-validation-error');
                        isValid = false;
                    }

                    if (type != 3) {
                        if (!inSum.val()) {
                            console.log('Input not valid!. Id=' + inSum.attr('id'));
                            inSum.addClass('input-validation-error');
                            isValid = false;
                        }
                        if (inCurrency.find(":selected").val() == '0') {
                            console.log('Select not valid!. Id=' + inCurrency.attr('id'));
                            inCurrency.addClass('input-validation-error');
                            isValid = false;
                        }
                    }
                }

                if (!dt.val()) {
                    console.log('Datetime not valid!. Id=' + dt.attr('id'));
                    dt.addClass('input-validation-error');
                    isValid = false;
                } else {
                    dt.removeClass('input-validation-error');
                }
            } catch (ex) {
                console.log(ex);
                isValid = false;
            }

            if (!isValid) {
                event.preventDefault();
                return false;
            }

//            try {
//                if (!checkPerformed) {
//                    checkSimilar();
//                    return false;
//                }
//            } catch (ex) {
//                console.log(ex);
//                return false;
//            }

            return true;
        });
    });
}

function isUseOutSum(type) {
    return (type == 2 || type == 3 || type == 4);
}

function isUseInSum(type) {
    return (type == 1 || type == 4);
}

function checkSimilar() {
    console.log(form.attr('action'));

    var sum = 0;
    if (isUseOutSum(transactionType))
        sum = parseFloat(outSum.val());
    else
        sum = parseFloat(inSum.val());

    $.ajax({
        url: '/budget4/transactions/findSimilar',
        type: "POST",
        data: JSON.stringify({ Date: dt.val(), Sum: sum }),
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json"
    })
    .done(function (response) {
        if (response.Data.Content) {
            var table = editorContainer.find('#findSimilarPopup table');

            var items = response.Data.Content;
            table.empty();
            for (var i = 0; i < items.length; i++) {
                var item = items[i];

                console.log('!!item', item);

                var tr = $('<tr></tr>');
                var td1 = $('<td></td>').append(item.Id);
                tr.append(td1);

                var td2 = $('<td></td>').append(item.Sum);
                tr.append(td2);

                var td4 = $('<td></td>').append(item.Sum);
                tr.append(td4);

                var td3 = $('<td><a href="" target="_blank"></a></td>');
                td3.find('a').attr('href', 'http://localhost/budget4/transactions/edit/' + item.Id);
                td3.find('a').text(item.Id);
                tr.append(td3);

                table.append(tr);
            }

            $('#findSimilarPopup')
                .on('shown.bs.modal', function (e) {
                    $('#findSimilarPopup .confirm').off('click').on('click', function () {
                        checkPerformed = true;
                        console.log('Continue saving...');
                        $('#findSimilarPopup').modal('hide');

                        editorContainer.find('button[type="submit"]').click();
                    });
                })
                .on('hide.bs.modal', function (e) {
                }).modal('show');
        }
    })
    .fail(function () {

    })
    .always(function () {

    });

    return false;
}