﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorMateriales.aspx.cs" Inherits="TPC_equipo_12.ProfesorMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="ButtonBackLeccionesProf" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a Lecciones" OnClick="ButtonBackLeccionesProf_Click" />
        <asp:Button ID="ButtonCrearMaterialProf" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Crear un Material" OnClick="ButtonCrearMaterialProf_Click" />
        <asp:Button ID="ButtonEliminarMaterialProf" CssClass="btn btn-warning mb-3 mt-3" runat="server" Text="Habilitar/Deshabilitar Material" OnClick="ButtonEliminarMaterialProf_Click" />
        <h1 class="text-center">Materiales</h1>
        <asp:Repeater ID="rptMaterialesProf" runat="server" OnItemDataBound="rptMaterialesProf_ItemDataBound">
            <ItemTemplate>
                <div class="card">
                    <div class="card-header d-flex justify-content-between align-items-center">
                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                        <asp:Button ID="ButtonModificarMaterialesProf" CssClass="btn btn-primary" runat="server" Text="Modificar Material" CommandArgument='<%# Eval("IDMaterial") %>' OnCommand="ButtonModificarMaterialesProf_Command" />
                    </div>
                    <div class="card-body">
                        <asp:Literal ID="ltlYoutubeVideo" runat="server"></asp:Literal>
                        <asp:Literal ID="ltlDocumento" runat="server"></asp:Literal>
                        <div class="card mb-3">
                            <div class="card-body">
                                <p class="card-text"><strong>Descripción:</strong> <%# Eval("Descripcion") %></p>
                                <p class="card-text"><strong>Estado:</strong> <%# GetEstadoText(Eval("Estado")) %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <h3 class="mt-3 mb-3">Preguntas y Respuestas de los Estudiantes</h3>

        <asp:Repeater ID="rptComentarios" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="d-flex align-items-center mb-2">
                            <div class="me-2">
                                <asp:Image ID="imgPerfil" runat="server" CssClass="img-fluid rounded-circle" Width="64px" Height="64px" ImageUrl='<%# "~/Images/" + Eval("UsuarioEmisor.ImagenPerfil.URL") %>' />
                            </div>
                            <div>
                                <strong><%# Eval("UsuarioEmisor.NombreCompleto") %>:</strong>
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

    </div>
</asp:Content>
