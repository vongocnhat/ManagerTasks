$(document).ready(function () {
    //default fromdate
    var id = window.location.href;
    id = id.substring(id.lastIndexOf('/') + 1, id.length);
    if (id == 0)
        document.getElementById('FromDate').valueAsDate = new Date();

    //check deadlinedate
    //var startDt = document.getElementById("FromDate")
    //  , endDt = document.getElementById("DeadlineDate");
    //function checkdeadlinedate() {
    //    if (new Date(startDt.value).getTime() >= new Date(endDt.value).getTime()) {
    //        endDt.setCustomValidity('Deadline must be greater than the From Date!');
    //    }
    //    else {
    //        endDt.setCustomValidity("");
    //    }
    //}
    //endDt.onkeyup = checkdeadlinedate;
    //startDt.onkeyup = checkdeadlinedate;

    //check max year
    var startDt, endDt;
    $('input[type="submit"]').click(function () {
        startDt = document.getElementById("FromDate");
        endDt = document.getElementById("DeadlineDate");
        if (new Date(startDt.value).getTime() >= new Date(endDt.value).getTime()) {
            endDt.setCustomValidity('Deadline must be greater than the From Date!');
        }
        else
        {
            endDt.setCustomValidity("");
        }
    });
    //check unitper

    $('input[name="UnitPer"]').on('keypress', function (e) {
        
        if (e.keyCode == 101 || e.keyCode == 69 || e.keyCode == 43 || e.keyCode == 45) {
            return false;
        }
    });
});
