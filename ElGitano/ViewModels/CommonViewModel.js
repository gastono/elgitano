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
    self.CategoriaID = ko.observable();
}