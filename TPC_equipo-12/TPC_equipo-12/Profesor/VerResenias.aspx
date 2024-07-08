<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="VerResenias.aspx.cs" Inherits="TPC_equipo_12.VerResenias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-2" OnClick="btnVolver_Click" />
    <div class="text-center my-3">
        <asp:Label CssClass="h3" ID="LabelNoHayResenias" runat="server" Visible="false"></asp:Label>
    </div>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="RepeaterResenias" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card w-75 text-center mt-3">
                        <asp:Image ID="imgPerfilRespuesta" runat="server" CssClass="card-img-top img-fluid rounded-circle mx-auto mt-2" Width="100px" Height="100px" ImageUrl='<%# "~/Images/" + Eval("Estudiante.ImagenPerfil.URL") %>' />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Estudiante.NombreCompleto") %></h5>
                            <hr />
                            <p class="card-text"><b>Calificacion: </b><%# Eval("Calificacion") %></p>
                            <hr />
                            <p class="card-text"><b>Reseña: </b><%# Eval("Comentario") %>.</p>
                        </div>
                        <div class="card-footer">
                            <small class="text-body-secondary"><%# Eval("FechaCreacion") %></small>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
</asp:Content>
