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
                pub.ID(value.ID);
                
                pub.Producto.ID(value.Producto.ID);
                pub.Producto.UsuarioID(value.Producto.UsuarioID);
                pub.Producto.Descripcion(value.Producto.Descripcion);
                pub.Producto.Url(value.Producto.Url);
                pub.Producto.ThumnailUrl(value.Producto.ThumnailUrl);

                self.Publicaciones.push(pub);
            });

        },
        error: function (msj) {
            alert(msj);
        }
    });
}

