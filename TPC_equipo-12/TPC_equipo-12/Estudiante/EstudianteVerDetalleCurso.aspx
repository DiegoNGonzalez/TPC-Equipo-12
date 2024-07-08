<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteVerDetalleCurso.aspx.cs" Inherits="TPC_equipo_12.EstudianteVerDetalleCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <asp:Button ID="volverCursos" runat="server" Text="Volver" CssClass="btn btn-primary mb-3" OnClick="volverCursos_Click" />
        <asp:Repeater ID="RepeaterVerDetalleCurso" runat="server" OnItemDataBound="RepeaterVerDetalleCurso_ItemDataBound">
            <ItemTemplate>
                <div class="card mb-4">
                    <div class="card-header text-center">
                        <h1><%# Eval("Nombre") %></h1>
                    </div>
                    <div class="card-body">
                        <div class="d-flex flex-column flex-md-row align-items-center mb-4">
                            <div class="flex-shrink-0 mb-3 mb-md-0">
                                <asp:Image ID="ImagenCurso" runat="server" CssClass="img-fluid" Style="width: 300px; height: auto;" ImageUrl='<%# "~/Images/" + Eval("Imagen.URL") %>' />
                            </div>
                            <div class="ms-md-4">
                                <h5>Descripción:</h5>
                                <p><%# Eval("Descripcion") %></p>
                            </div>
                        </div>
                        <div class="mb-3">
                            <h5>Duración:</h5>
                            <p><%# Eval("Duracion") %> hs.</p>
                        </div>
                        <div class="mb-3">
                            <h5>Fecha estreno:</h5>
                            <p><%# Eval("Estreno") %></p>
                        </div>
                        <div class="mb-3">
                            <h5>Categoría:</h5>
                            <p><%# Eval("Categoria.Nombre") %></p>
                        </div>
                        <div class="mb-3">
                            <h5>Unidades:</h5>
                            <asp:Label ID="LabelNoHayUnidades" runat="server" Visible="false" CssClass="text-muted"></asp:Label>
                            <asp:Repeater ID="RepeaterUnidades" runat="server">
                                <ItemTemplate>
                                    <ol class="list-group list-group-numbered mb-3">
                                        <li class="list-group-item d-flex justify-content-between align-items-start">
                                            <div class="ms-2 me-auto">
                                                <div class="fw-bold"><%# Eval("Nombre") %></div>
                                                <%# Eval("Descripcion") %>
                                            </div>
                                        </li>
                                    </ol>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div>
                            <h5 class="mt-4">Reseñas:</h5>
                            <asp:Label ID="LabelNoHayResenias" runat="server" Visible="false" CssClass="text-muted"></asp:Label>
                            <asp:Repeater ID="RepeaterResenias" runat="server">
                                <ItemTemplate>
                                    <ul class="list-group mb-4">
                                        <li class="list-group-item d-flex justify-content-between align-items-start">
                                            <span class="badge bg-success rounded-pill"><%# Eval("Calificacion") %></span>
                                            <div class="ms-2 me-auto">
                                                <div class="fw-bold"><%# Eval("Estudiante.NombreCompleto") %></div>
                                                <%# Eval("Comentario") %>
                                            </div>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
