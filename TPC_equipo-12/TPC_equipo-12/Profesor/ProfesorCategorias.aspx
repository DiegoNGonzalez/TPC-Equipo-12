<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorCategorias.aspx.cs" Inherits="TPC_equipo_12.ProfesorCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center">Categorías</h2>

    <div class="container vh-75 d-flex flex-column justify-content-center align-items-center mt-5">
        <!-- Fila para el dropdown y los botones -->
        <div class="row justify-content-center w-100">
            <!-- Columna para el dropdown -->
            <div class="col-md-4">
                <div class="mb-3">
                    <label class="form-label">Seleccionar Categoría</label>
                    <asp:DropDownList ID="dropCategorias" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Nueva Categoría</label>
                    <asp:TextBox ID="txtNuevaCategoria" runat="server" CssClass="form-control" />
                    <asp:Label ID="lblNotificacion" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Fila para los botones -->
        <div class="row justify-content-center mt-3 w-100">
            <div class="col-md-4 text-center">
                <asp:Button Text="Agregar" CssClass="btn btn-primary mb-2" ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" />
                <asp:Button Text="Modificar" CssClass="btn btn-secondary mb-2" ID="btnModificar" runat="server" />
                <asp:Button Text="Eliminar" CssClass="btn btn-danger mb-2" ID="btnEliminar" runat="server" />
            </div>
        </div>
    </div>


</asp:Content>
