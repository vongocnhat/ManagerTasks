//checkbox delete
$(document).ready(function () {
	$('body').on('click', '#thcheckbox', function () {
		$("input[name='Id']").prop('checked', this.checked);
	});

	$('body').click("input[name='Id']", function () {

        if ($("input[name='Id']").length == $("input[name='Id']:checked").length) {
            $('#thcheckbox').prop('checked', true);
        }
        else
        {
            $('#thcheckbox').prop('checked', false);
        }
	});

    $('body').on('click', 'input[name="btnDelete"]', function () {
        var count = $("input[name='Id']:checked").length;
        if(count == 0)
        {
            alert("No rows slected to delete");
            return false;
        }
        else
        {
            var result = confirm(count + " row(s) will be deleted");
            if(result == true)
                $('#deleteform').submit();
        }
    });
});
