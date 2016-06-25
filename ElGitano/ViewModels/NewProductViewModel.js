var NewProductViewModel = function () {

    var self = this;

    var tab = CATSUBCAT;

    self.Categorias = ko.observableArray();

    self.SubCategorias = ko.observableArray();

    self.CategoriaVisibility = ko.observable(true);

    self.SubCategoriaVisibility = ko.observable(false);

    self.ImagesUploadVisibility = ko.observable(false);

    self.IdCategoriaSelected = ko.observable(); //categoria seleccionada

    self.IdSubCategoriaSelected = ko.observable(); //subcategoria seleccionada

    self.Descripcion = ko.observable(); //Descripcion escrita del producto

    self.GetSubCategorias = function (data) {
        GSubCategorias(data, self);
    }

    self.GuardarCategoria = function (data) {
        self.IdSubCategoriaSelected(0); //limpio subcategoria
        self.IdCategoriaSelected(data.ID);
    }

    self.GuardarSubCategoria = function (data) {
        self.IdSubCategoriaSelected(data.ID);
    }

    self.BtnSiguiente = function () {
        switch (tab) {
            case CATSUBCAT:
                self.CategoriaVisibility(false);
                self.SubCategoriaVisibility(true);
                self.ImagesUploadVisibility(false);
                tab = DESCRIPCION;
                break;
            case DESCRIPCION:
                self.SubCategoriaVisibility(false);
                self.ImagesUploadVisibility(true);
                self.CategoriaVisibility(false);
                tab = IMAGENES;
                break;
        }
    }

    self.BtnAtras = function () {

        switch (tab) {
            case DESCRIPCION:
                self.SubCategoriaVisibility(false);
                self.ImagesUploadVisibility(false);
                self.CategoriaVisibility(true);
                tab = CATSUBCAT;
                break;
            case IMAGENES:
                self.ImagesUploadVisibility(false);
                self.SubCategoriaVisibility(true);
                self.SubCategoriaVisibility(false);
                tab = DESCRIPCION;
                break;
        }
    }

    self.BtnCrearPublicacion = function () {
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


function GSubCategorias(data, context) {
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