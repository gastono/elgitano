//CONSTANTS

var CATSUBCAT = "CATSUBCAT";
var DESCRIPCION = "DESCRIPCION";
var UBICACION = "UBICACION";
var IMAGENES = "IMAGENES";
var DEFAULTTHUMBNAILIMAGE = "http://stmedia.stimg.co/strib-sports-upload-color.png?w=150&h=150";
var DEFAULTFILEINPUTQUANTITY = 3;


var CommonViewModel = function ()
{
    var self = this;   
}

function EmptyProducto(context)
{
    return new Producto();
}

var  Producto = function()
{
    var self = this;
    self.ID = ko.observable();
    self.UsuarioID = ko.observable();
    self.Descripcion = ko.observable();
    self.Url = ko.observable();
    self.ThumnailUrl = ko.observable();
}

var Publicacion = function ()
{
    var self = this;
    self.ID = ko.observable();
    self.Producto = new Producto();
}

var Categoria = function ()
{
    var self = this;
    self.ID = ko.observable();
    self.Descripcion = ko.observable();    }

var SubCategoria = function ()
{
    var self = this;
    self.ID = ko.observable();
    self.Descripcion = ko.observable();
    self.CategoriaID = ko.observable
}

var NuevaPublicacionRequest = function ()
{
    var self = this;
    self.Categoria = ko.observable();
}

var Image = function ()
{
    var self = this;
    self.Url = ko.observable();
    self.ThumnailUrl = ko.observable(DEFAULTTHUMBNAILIMAGE);
}