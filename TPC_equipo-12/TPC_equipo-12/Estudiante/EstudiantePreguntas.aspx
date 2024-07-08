<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudiantePreguntas.aspx.cs" Inherits="TPC_equipo_12.EstudiantePreguntas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="btnVolver" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver" OnClick="btnVolver_Click" />
<div class="card mb-3">
    <div class="card-body">
        <h5 class="card-title">Comentario Principal</h5>
        <div class="d-flex align-items-center mb-2">
            <div class="me-2">
                <asp:Image ID="imgPerfilPadre" runat="server" CssClass="img-fluid rounded-circle" Width="64px" Height="64px" />
            </div>
            <div>
                <strong>
                    <asp:Label ID="lblNombreYApellido" runat="server"></asp:Label>
                </strong>
            </div>
        </div>
        <div class="media-body">
            <p class="card-text">
                <asp:Label ID="lblCuerpoComentario" runat="server"></asp:Label>
            </p>
            <p class="card-text">
                <small class="text-muted">
                    <asp:Label ID="lblFechaCreacion" runat="server"></asp:Label>
                </small>
            </p>
        </div>
    </div>
</div>

        <asp:Repeater ID="rptRespuestas" runat="server">
    <ItemTemplate>
        <div class="card mb-3">
            <div class="card-body">
                <div class="d-flex align-items-center mb-2">
                    <div class="me-2">
                        <asp:Image ID="imgPerfil" runat="server" CssClass="img-fluid rounded-circle" Width="64px" Height="64px" ImageUrl='<%# "~/Images/" + Eval("UsuarioEmisor.ImagenPerfil.URL")%>' />
                    </div>
                    <div>
                        <strong><%# Eval("UsuarioEmisor.Nombre")%> <%# Eval("UsuarioEmisor.Apellido") %>:</strong>
                    </div>
                </div>
                <div>
                    <p class="card-text"><%# Eval("CuerpoComentario") %></p>
                    <p class="card-text"><small class="text-muted"><%# Eval("FechaCreacion", "{0:dd/MM/yyyy HH:mm}") %></small></p>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
        <div class="mt-5">
            <div class="card mt-3">
                <div class="card-body">
                    <asp:TextBox ID="txtRespuesta" CssClass="form-control mb-3" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Escribe tu respuesta aquí..."></asp:TextBox>
                    <asp:Button ID="btnResponder" CssClass="btn btn-primary" runat="server" Text="Responder" OnClick="btnResponder_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
