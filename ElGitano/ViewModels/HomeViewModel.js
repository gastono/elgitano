/// <reference path="CommonViewModel.js" />

var HomeViewModel = function ()
{
    var self = this;

    self.Publicaciones = ko.observableArray();

}

function GetItems(context)

    var self = context;

    $.ajax({
        url: "api/HomeApi/GetPublicaciones",
        type: 'GET',        
        success: function (data) {                    

            self.Publicaciones.removeAll();

            $.each(data, function (index, value) {
                var Producto = new Producto()

                //crear el producto de la publciacion , colocar los valores obtenido desde el server
                
            });

        },
        error: function (msj) {
            alert(msj);
        }
    });
}

