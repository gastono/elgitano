var NewProductViewModel = function () {

    var self = this;

    var tab = CATSUBCAT;

    self.Categorias = ko.observableArray();

    self.SubCategorias = ko.observableArray();

    self.CategoriaVisibility = ko.observable(true);

    self.SubCategoriaVisibility = ko.observable(false);

    self.ImagesUploadVisibility = ko.observable(false);

    self.DescripcionVisibility = ko.observable(false);

    self.IdCategoriaSelected = ko.observable(); //categoria seleccionada

    self.IdSubCategoriaSelected = ko.observable(); //subcategoria seleccionada

    self.Descripcion = ko.observable(); //Descripcion escrita del producto

    self.InputFiles = ko.observableArray();

    self.GetSubCategorias = function (data) {
        self.IdCategoriaSelected(data.ID()); // obtengo la categoria seleccionada
        GSubCategorias(data, self);
    }

    self.GuardarCategoria = function (data) {
        self.IdSubCategoriaSelected(0); //limpio subcategoria
        self.IdCategoriaSelected(data.ID);
    }

    self.GuardarSubCategoria = function (data) {
        self.IdSubCategoriaSelected(data.ID); //obtengo la subcategoria seleccionada
    }

    self.BtnSiguiente = function () {
        switch (tab) {
            case CATSUBCAT:
                self.CategoriaVisibility(false);
                self.DescripcionVisibility(true);
                self.ImagesUploadVisibility(false);
                tab = DESCRIPCION;
                break;
            case DESCRIPCION:
                self.CategoriaVisibility(false);
                self.DescripcionVisibility(false);
                self.ImagesUploadVisibility(true);               
                tab = IMAGENES;
                break;
        }
    }

    self.BtnAtras = function () {
        switch (tab) {
            case DESCRIPCION:
                self.DescripcionVisibility(false);
                self.ImagesUploadVisibility(false);
                self.CategoriaVisibility(true);
                tab = CATSUBCAT;
                break;
            case IMAGENES:
                self.ImagesUploadVisibility(false);
                self.CategoriaVisibility(false);
                self.DescripcionVisibility(true);
                tab = DESCRIPCION;
                break;            
        }
    }    

    self.BtnCargarPublicacion = function (data,context) {

        var files = $("#file-input").get(0).files;

        var form = new FormData();

        for (i = 0; i < files.length; i++) {

            form.append("file" + i, files[i]);

            form.append("UsuarioId", "1");

            form.append("Titulo", "CanadePescar");

            $.ajax({
                type: "POST",
                url: "/api/NewProductApi/ProcesarImagen",
                processData: false,
                contentType: false,               
                data: form,
                success: function (resp) {

                    $("#inputFile").val('');           

                    data.Url(resp.Url);

                    data.ThumnailUrl(resp.ThumnailUrl)
                }
            });
        }
    }

    window.onbeforeunload = function (e) {
        return 'Dialog text here.';
    };

    window.onunload = function (e)
    {

    }

    GetCategorias(self);

    AddInputFiles(self,DEFAULTFILEINPUTQUANTITY);
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

function ConfirmarPublicacion(context)
{
    var self = context;

    var request = new ConfirmarPublicacionRequest();

    request.CategoriaId(self.IdCategoriaSelected());

    request.SubCategoriaId(self.IdSubCategoriaSelected());

    request.Comentario(self.Comentario());

    request.Titulo(self.Titulo());

    request.ProductoId(NUEVOPRODUCTOID);

    ko.utils.arrayForEach(self.InputFiles(), function (image)
    {
        request.Imagenes.push(image);
    });

    $.ajax({
        url: "/api/NewProductApi/ConfirmarPublicacion",
        type: "POST",
        data: request,
        success: function (data) {
            


        },
        error: function (msj) {
            alert(msj);
        }
    });
}

//agrega imputfiles para ingresar imagenes a la publicacion
function AddInputFiles(context, quant)
{
    var self = context;

    for (var i = 0; i < quant; i++) {        

        self.InputFiles.push(new Image());
    }
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