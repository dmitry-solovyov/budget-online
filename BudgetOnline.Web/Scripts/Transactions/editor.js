var transactionType;

function initialiseEditor(container) {
    if (container == null) return;

    var transactionTypeControl = container.find('select#TransactionType');
    if (transactionTypeControl != null) {
        changeTransactionType(container, true);

        transactionTypeControl.change(function () {
            changeTransactionType(container, false);
        });
    }
}

function changeTransactionType(container, initial) {
    var type = container.find('select#TransactionType').find(":selected").val();

    if (type == 1) {
        container.find('#in_sum_group').removeClass('hidden');
        container.find('#out_sum_group').addClass('hidden');

        if (!initial) {
            container.find('#in_sum_group #SumIn_Formula').val(container.find('#out_sum_group #SumOut_Formula').val());
            container.find('#in_sum_group #SumIn_Sum').val(container.find('#out_sum_group #SumOut_Sum').val());
            container.find('#in_sum_group #SumIn_Currency').val(container.find('#out_sum_group #SumOut_Currency').val());
        }
    }
    else if (type == 2) {
        container.find('#out_sum_group').removeClass('hidden');
        container.find('#in_sum_group').addClass('hidden');

        if (!initial) {
            container.find('#out_sum_group #SumOut_Formula').val(container.find('#in_sum_group #SumIn_Formula').val());
            container.find('#out_sum_group #SumOut_Sum').val(container.find('#in_sum_group #SumIn_Sum').val());
            container.find('#out_sum_group #SumOut_Currency').val(container.find('#in_sum_group #SumIn_Currency').val());
        }
    }
    else if (type == 3 || type == 4) {
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
