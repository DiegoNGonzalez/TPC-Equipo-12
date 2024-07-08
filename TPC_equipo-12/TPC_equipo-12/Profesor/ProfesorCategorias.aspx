<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorCategorias.aspx.cs" Inherits="TPC_equipo_12.ProfesorCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="text-center mt-3">Categorías</h3>

    <div class="container vh-75 d-flex flex-column justify-content-center align-items-center mt-4">
        <div class="row justify-content-center w-100">
            <div class="col-md-4">
                <div class="mb-3">
                    <label class="form-label"><b>Seleccionar Categoría</b></label>
                    <asp:DropDownList ID="dropCategorias" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label"><b>Nueva Categoría</b></label>
                    <asp:TextBox ID="txtNuevaCategoria" runat="server" CssClass="form-control" />
                    <asp:Label ID="lblNotificacion" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Fila para los botones -->
        <div class="row justify-content-center mt-3 w-100">
            <div class="col-md-4 text-center">
                <asp:Button Text="Agregar" CssClass="btn btn-primary mb-2" ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" />
                <asp:Button Text="Modificar" CssClass="btn btn-secondary mb-2" ID="btnModificar" runat="server" OnClick="btnModificar_Click" />
                <asp:Button Text="Eliminar" CssClass="btn btn-danger mb-2" ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" />
            </div>
        </div>
    </div>


</asp:Content>
