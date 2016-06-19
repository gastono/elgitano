var NewProductViewModel = function ()
{
    var self = this;
    
    self.Categorias = ko.observableArray();
    self.SubCategorias = ko.observableArray();

    GetCategorias(self);

}

function GetCategorias(context)
{
    var self = context;

    $.ajax({
        url: "/api/NewProductApi/GetCategorias",
        type: 'GET',
        success: function (data) {

            self.Categorias.removeAll();

            $.each(data, function (index, value) {
                var cat = new Categoria();
                cat.ID(value.ID);
                cat.Descripcion(value.Descripcion);

                self.Categorias.push(cat);
            });

        },
        error: function (msj) {
            alert(msj);
        }
    });
}