window.addEventListener("popstate", function (e) {
    $.ajax({
        url: location.href,
        success: function (result) {
            $('#containlist').html(result);
        }
    });
});

function nextUrlIndex(page)
{
    searchString = $('#searchString').val();
    page = page || 1;
    searchColumn = $('select[name="searchColumn"]').val();
    if (searchString == undefined)
        searchString = '';
    url = window.location.pathname + '?page=' + page + '&pageSize=' + $('select[name="pageSize"]').val() + '&searchString=' + searchString + '&searchColumn=' + searchColumn;
    return url;
}

function ajaxHtml(page)
{
    $.ajax({
        url: nextUrlIndex(page),
        success: function (result) {
            $('#containlist').html(result);
        }
    });
}
$('select[name="pageSize"]').change(function () {
    ajaxHtml();
});


$("#searchString").keyup(function () {
    ajaxHtml();
});


$(function () {
    //pageNumber
    $('body').on('click', '.pagination li a', function (e) {
        e.preventDefault();
        if ($(this).attr('href') != undefined && $(this).attr('href') != '') {            
            var page = $(this).text();
            if ($(this).text() == "»")
                page = parseInt($('body .pagination .active a').text()) + 1;
            else
                if ($(this).text() == "«")
                    page = parseInt($('body .pagination .active a').text()) - 1;
            ajaxHtml(page);
        }
    });
});
//create search column
$('th').each(function () {
    if ($(this).text() != '' && $(this).text() != 'Delete' && $(this).text() != 'Edit' && $(this).text() != 'Detail')
        $('select[name="searchColumn"]').append('<option>' + $(this).text() + '</option>');
});

//search column change

$('select[name="searchColumn"]').change(function () {
    //if ($(this).find('option:selected').text() == 'Is Active') {
    //    $('#searchString').hide();
    //    $('select[name="isActive"]').css('display', 'unset');
    //    $('#searchString').val("True");
    //}
    //else {
    //    $('#searchString').show();
    //    $('select[name="isActive"]').hide();
    //}
    //if ($(this).find('option:selected').text() == 'From Date' || $(this).find('option:selected').text() == 'DeadLine Date') {
    //    $('#searchString').hide();
    //    $('select[name="searchDate"]').css('display', 'unset');
    //    //$('#searchString').val("True");
    //}
    //else {
    //    $('#searchString').show();
    //    $('select[name="isActive"]').hide();
    //}
    var column = $(this).find('option:selected').text();
    var searchDate = $('input[name="searchDate"]');
    var isActive = $('select[name="isActive"]');
    switch (column)
    {
        case "Is Active":
            isActive.css('display', 'unset');
            $('#searchString').hide();
            $(searchDate).hide();
            $('#searchString').val("True");
            break;
        //case "From Date":
        //case "Deadline Date":
        //    searchDate.css('display', 'unset');
        //    $('#searchString').hide();
        //    $(isActive).hide();
        //    break;
        default: $('#searchString').show();
                 $(isActive).hide();
                 $(searchDate).hide();
                 
    }
    ajaxHtml();
    //set default search string
    $('#searchString').val("");
});
//isActive change
$('select[name="isActive"]').change(function () {
    $('#searchString').val($(this).find('option:selected').text());
    $("#searchString").keyup();       
});
//search date change
//$('input[name="searchDate"]').keypress(function () {
//    var parts = $(this).val().split('-');
//    console.log(parts);
//    var searchDate = parts[2] + '/' + parts[1] + '/' + parts[0];
//    $('#searchString').val(searchDate);
//    $("#searchString").keyup();
//});
