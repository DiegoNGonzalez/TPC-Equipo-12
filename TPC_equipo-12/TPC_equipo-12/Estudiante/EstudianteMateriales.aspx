<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteMateriales.aspx.cs" Inherits="TPC_equipo_12.EstudianteMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="ButtonBackLecciones" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a Lecciones" OnClick="ButtonBackLecciones_Click" />
        <h1 class="text-center">Materiales</h1>
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
        <asp:Panel ID="pnlPreguntasRespuestas" runat="server" Visible="false">
            <h3 class="mt-3 mb-3">Preguntas y Respuestas</h3>
            <asp:Repeater ID="rptComentarios" runat="server">
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="card-body">
                            <div class="d-flex align-items-center mb-2">
                                <div class="me-2">
                                    <asp:Image ID="imgPerfil" runat="server" CssClass="img-fluid rounded-circle" Width="64px" Height="64px" ImageUrl='<%# "~/Images/" + Eval("UsuarioEmisor.ImagenPerfil.URL") %>' />
                                </div>
                                <div>
                                    <h5 class="mt-0"><%# Eval("UsuarioEmisor.Nombre") %> <%# Eval("UsuarioEmisor.Apellido") %>:</h5>
                                </div>
                            </div>
                            <div>
                                <p class="card-text"><%# Eval("CuerpoComentario") %></p>
                                <p class="card-text"><small class="text-muted"><%# Eval("FechaCreacion", "{0:dd/MM/yyyy HH:mm}") %></small></p>
                                <asp:Button ID="btnRespuesta" CssClass="btn btn-sm btn-secondary" runat="server" Text="Responder" OnClick="btnRespuesta_Click" CommandArgument='<%# Eval("IDComentario") %>' />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="mt-5">
                <div class="card mt-3">
                    <div class="card-body">
                        <asp:TextBox ID="txtComentario" CssClass="form-control mb-3" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Escribe tu duda aquí..."></asp:TextBox>
                        <asp:Button ID="btnPreguntar" CssClass="btn btn-primary" runat="server" Text="Preguntar" OnClick="btnPreguntar_Click" />
                    </div>
                </div>
            </div>

        </asp:Panel>
        <br />
        <asp:Label ID="lblMensajeInactivo" runat="server" CssClass="text-muted mt-3" Text="El contenido se encuentra deshabilitado." Visible="false"></asp:Label>
    </div>

</asp:Content>
