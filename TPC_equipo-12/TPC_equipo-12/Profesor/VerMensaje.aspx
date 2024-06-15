<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="VerMensaje.aspx.cs" Inherits="TPC_equipo_12.VerMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Ver Mensaje</h1>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>De:</label>
                    <asp:Label ID="lblDe" runat="server" Text=""></asp:Label>
                </div>
                <div class="form-group">
                    <label>Asunto:</label>
                    <asp:Label ID="lblAsunto" runat="server" Text=""></asp:Label>
                </div>
                <div class="form-group">
                    <label>Mensaje:</label>
                    <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label>
                </div>
                <div class="form-group">
                    <label>Mensaje:</label>
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </div>
               <%-- <div class="form-group">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="btnVolver_Click" />
                </div>--%>
            </div>
        </div>
    </div>

</asp:Content>
