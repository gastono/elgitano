var NewProductViewModel = function () {
    var self = this;

    self.Categorias = ko.observableArray();

    self.SubCategorias = ko.observableArray();

    self.CategoriaVisibility = ko.observable(true);

    self.SubCategoriaVisibility = ko.observable(false);

    self.DescripcionCategoriaSelected = ko.observable();
    
    self.DescripcionSubCategoriaSelected = ko.observable();

    self.GetSubCategorias = function (data) {
        GSubCategorias(data, self);
        self.DescripcionCategoriaSelected(data.Descripcion);
    }

    self.BtnSiguiente = function ()
    {
        self.CategoriaVisibility(false);

        self.SubCategoriaVisibility(true);
    }

    self.BtnAtras = function () {       

        self.SubCategoriaVisibility(false);

        self.CategoriaVisibility(true);
    }

    self.BtnCrearPublicacion = function ()
    {
        var files = $("#inputFile").get(0).files;
        var data = new FormData();
        
        for (i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);

            data.append("categoria", "categoriaseleccionada");
            data.append("subcategoria", "subcategoria seleccionada");
            data.append("descripcion", "descipcion");

        $.ajax({
            type: "POST",
            url: "/api/NewProductApi/CrearPublicacion",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result) {
                    alert('Archivos subidos correctamente');
                    $("#inputFile").val('');
                }
            }
        });
    }

    GetCategorias(self);

}

function GetCategorias(context) {
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


function GSubCategorias(data, context)
{
    var self = context;

    var categoriaID = data.ID();


    $.ajax({
        url: "/api/NewProductApi/GetSubCategorias",
        type: 'GET',
        data: { categoriaID: categoriaID },
        success: function (data) {

            self.SubCategorias.removeAll();

            $.each(data, function (index, value) {
                var subCat = new SubCategoria();
                subCat.ID(value.ID);
                subCat.Descripcion(value.Descripcion);
                subCat.CategoriaID(value.CategoriaID);

                self.SubCategorias.push(subCat);
            });
                      
        },
        error: function (msj) {
            alert(msj);
        }
    });


}