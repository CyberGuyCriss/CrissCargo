function GetOrderByOrderNumber(OrderNumber){
    if (OrderNumber != "")
    {
        $.ajax({
            type: 'post',
            dataType: 'Json',
            url: '/Admin/GetOrderByOrderNumber',
            data:
            {
                orderNo: OrderNumber
            },
            success: function (result) {
                debugger;
                if (!result.isError)
                {
                    $("#orderBynoResult").empty();
                    $.each(result, function (i, order) {
                        debugger;
                        $("#prepareOrderBtn").empty();
                        var myOrderBTN = '<a type="submit" class="btn btn-primary w-50 rounded-pill" class="nav-link text-white" href=/Admin/PrepareQuotation?orderNo=' + OrderNumber +'> Prepare Quotation</a>';
                        $("#prepareOrderBtn").append(myOrderBTN);

                        $("#orderBynoResult").append('<tr>' +
                            '<td>' + order.productName +'</td>' +
                            '<td>' + order.productLink + '</td>' +
                            '<td>' + order.quantity + '</td>' +
                            '<td>' + order.colour + '</td>' +
                            '<td>' + order.size + '</td>' +
                            '<td>' + order.moreDescription + '</td>' +
                            '<td class="d-flex">' + '<button typeof="button" class="btn btn-primary"><i class="bi bi-pencil-square"></i></button> <button type="button" class="btn btn-danger"><i class="bi bi-x-square-fill"></i></button></td >' 
                            );
                    });
                }
                else
                {
                    errorAlert(result.msg);
                }
            },
            Error: function (ex)
            {
                errorAlert(ex);
            }
        });
    }
}

function PrepareOrderForQuotation(OrderNumber) {
    debugger;
    if (OrderNumber != "") {
        $.ajax({
            type: 'post',
            dataType: 'Json',
            url: '/Admin/PrepareOrderForQuotation',
            data:
            {
                orderNo: OrderNumber
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    $("#orderBynoResult").empty();
                    $.each(result, function (i, order) {
                        debugger;
                        $("#orderBynoResult").append('<tr>' +
                            '<td>' + order.productName + '</td>' +
                            '<td>' + order.productLink + '</td>' +
                            '<td>' + order.quantity + '</td>' +
                            '<td>' + order.colour + '</td>' +
                            '<td>' + order.size + '</td>' +
                            '<td class="d-flex">' + '<button typeof="button" class="btn btn-primary"><i class="bi bi-pencil-square"></i></button> <button type="button" class="btn btn-danger"><i class="bi bi-x-square-fill"></i></button></i></button> <button type="button" class="btn btn-danger" href="/Admin/PrepareQuotation' + order.Id + '">Prepare</button>' + '</td >'
                        );
                    });
                }
            }
        });
    }
}



function CalculateQuotation(quantity, id){
    debugger
    var unitPrice = $("#unitPrice"+id).val();
    var totalOrderPrice = quantity * unitPrice;
    $("#totalPrice" + id).val(totalOrderPrice);
   
} 

function GetTotalAmountForQuotation(){
    var totalAmount = 0;
     var allInputWithSameClass = document.getElementsByClassName('totalQuotationPrice');

    $.each(allInputWithSameClass, function (index, value) {
        var inputId = value.id;
        var bagaggePrice = parseInt($("#" + inputId).val());
        totalAmount += bagaggePrice;
    }); 

    if (totalAmount > 0) {
        $("#totalAmountBar").html("Total Amount for this Quotation is: " + totalAmount);
        $("#totalAmountCol").html(totalAmount);
        $("#totalAmountBar").show();
    }
    else
    {
        return;
    }
};


function copyBtn() {
    debugger;
    var copyText = document.getElementById("text1");
    copyText.select();
    copyText.setSelectionRange(0, 99999);
    navigator.clipboard.writeText(copyText.value);
   
};