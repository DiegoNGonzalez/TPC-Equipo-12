<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteMateriales.aspx.cs" Inherits="TPC_equipo_12.EstudianteMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="ButtonBackLecciones" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a Lecciones" OnClick="ButtonBackLecciones_Click" />
        <asp:Repeater ID="rptMateriales" runat="server" OnItemDataBound="rptMateriales_ItemDataBound">
            <ItemTemplate>
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                    </div>
                    <div class="card-body">
                        <asp:Literal ID="ltlYoutubeVideo" runat="server"></asp:Literal>
                        <asp:Literal ID="ltlDocumento" runat="server"></asp:Literal>
                        <div class="card mb-3">
                            <div class="card-body">
                                <p class="card-text"><strong>Descripción:</strong> <%# Eval("Descripcion") %></p>
                            </div>
                        </div>

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <h3>Preguntas y Respuestas</h3>

        <asp:Repeater ID="rptComentarios" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="media">
                            <div class="mr-3">
                                <asp:Image ID="imgPerfil" runat="server" CssClass="mg-fluid rounded-circle" Width="64px" Height="64px" ImageUrl='<%# "~/Images/" + Eval("UsuarioEmisor.ImagenPerfil.URL") %>'  />
                            </div>
                            <div class="media-body">
                                <h5 class="mt-0"><%# Eval("UsuarioEmisor.Nombre") %></h5>
                                <p class="card-text"><%# Eval("CuerpoComentario") %></p>
                                <p class="card-text"><small class="text-muted"><%# Eval("FechaCreacion", "{0:dd/MM/yyyy HH:mm}") %></small></p>
                                <asp:Button ID="btnRespuesta" CssClass="btn btn-sm btn-secondary" runat="server" Text="Responder" OnClick="btnRespuesta_Click" CommandArgument='<%# Eval("IDComentario") %>' />
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <!-- Formulario para Agregar Comentario -->
        <div class="mt-5">
            <div class="card mt-3">
                <div class="card-body">
                    <asp:TextBox ID="txtComentario" CssClass="form-control mb-3" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Escribe tu duda aquí..."></asp:TextBox>
                    <asp:Button ID="btnPreguntar" CssClass="btn btn-primary" runat="server" Text="Preguntar" OnClick="btnPreguntar_Click" />
                </div>
            </div>
        </div>

    </div>

</asp:Content>
