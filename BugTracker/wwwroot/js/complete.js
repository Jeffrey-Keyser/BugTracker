$((function () {
    var url;
    var redirectUrl;
    var target;

    $('body').append(`
                    <div class="modal fade" id="completeModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                        <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body complete-modal-body">
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal" id="cancel-complete">Cancel</button>
                            <button type="button" class="btn btn-success" id="confirm-complete">Complete</button>
                        </div>
                        </div>
                    </div>
                    </div>`);

    //Delete Action
    $(".complete").on('click', (e) => {
        e.preventDefault();

        target = e.target;
        var Id = $(target).data('id');
        var controller = $(target).data('controller');
        var action = $(target).data('action');
        var bodyMessage = $(target).data('body-message');
        redirectUrl = $(target).data('redirect-url');

        url = "/" + controller + "/" + action + "?Id=" + Id;
        $(".complete-modal-body").text(bodyMessage);
        $("#completeModal").modal('show');
    });

    $("#confirm-complete").on('click', () => {
        $.post(url)
            .done((result) => {
                if (!redirectUrl) {
                    return $(target).parent().parent().hide("slow");
                }
                window.location.href = redirectUrl;
            })
            .fail((error) => {
                if (redirectUrl)
                    window.location.href = redirectUrl;
            }).always(() => {
                $("#completeModal").modal('hide');
            });
    });

}()));