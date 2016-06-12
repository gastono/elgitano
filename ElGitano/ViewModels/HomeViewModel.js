/// <reference path="CommonViewModel.js" />

var HomeViewModel = function ()
{
    var self = this;

    self.Publicaciones = ko.observableArray();
    GetPublicaciones(self);
}

function GetPublicaciones(context)
{
    var self = context;

    $.ajax({
        url: "api/HomeApi/GetPublicaciones",
        type: 'GET',        
        success: function (data) {                    

            self.Publicaciones.removeAll();

            $.each(data, function (index, value) {
                var pub = new Publicacion();
                var prod = new Producto();
                prod.ID(value.ID);
                prod.UsuarioID(value.UsuarioID);
                prod.Descripcion(value.Descripcion);
                prod.Url(value.Url);
                prod.ThumnailUrl(value.ThumnailUrl);

                pub.Producto(Producto);

                self.Publicaciones.push(pub);
            });

        },
        error: function (msj) {
            alert(msj);
        }
    });
}

