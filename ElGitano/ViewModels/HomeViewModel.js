var HomeViewModel = function ()
{
    var self = this;

    self.Publicaciones = ko.observableArray();

}

function GetItems(context)
{
    var self = context;

    $.ajax({
        url: "api/HomeApi/GetPublicaciones",
        type: 'GET',
        data: { userId: '1' },
        success: function (data) {
            self.Imagenes.removeAll();
            $.each(data, function (index, value) {


            });
        },
        error: function (msj) {
            alert(msj);
        }
    });
}