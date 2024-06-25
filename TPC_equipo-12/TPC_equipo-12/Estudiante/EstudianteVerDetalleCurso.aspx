<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteVerDetalleCurso.aspx.cs" Inherits="TPC_equipo_12.EstudianteVerDetalleCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Repeater ID="RepeaterVerDetalleCurso" runat="server" OnItemDataBound="RepeaterVerDetalleCurso_ItemDataBound">
                    <itemtemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-center align-items-center mb-4">
                                    <h1><%# Eval("Nombre") %></h1>
                                </div>
                                <div class="d-flex align-items-center mb-4">
                                    <div class="flex-shrink-0">
                                        <img class="img-fluid" src='<%# Eval("Imagen.URL") %>' style="width: 300px; height: auto;">
                                    </div>
                                    <div class="ms-4">
                                        <h5>Descripción:</h5>
                                        <p><%# Eval("Descripcion") %></p>
                                    </div>
                                </div>
                                <p>
                                    <h5>Duracion:</h5>
                                    <%# Eval("Duracion") %> hs.
                                </p>
                                <p>
                                    <h5>Fecha estreno:</h5>
                                    <%# Eval("Estreno") %>
                                </p>
                                <p>
                                    <h5>Categoria:</h5>
                                    <%# Eval("Categoria.Nombre") %>
                                </p>
                                <h5>Unidades:</h5>
                                <asp:Label ID="LabelNoHayUnidades" runat="server" Visible="false"></asp:Label>
                                <asp:Repeater ID="RepeaterUnidades" runat="server">
                                    <itemtemplate>
                                        <ol class="list-group list-group-numbered">
                                            <li class="list-group-item d-flex justify-content-between align-items-start">
                                                <div class="ms-2 me-auto">
                                                    <div class="fw-bold"><%# Eval("Nombre") %></div>
                                                    <%# Eval("Descripcion") %>
                                                </div>
                                            </li>
                                        </ol>
                                    </itemtemplate>
                                </asp:Repeater>
                                <h5 class="mt-4">Reseñas:</h5>
                                <asp:Label ID="LabelNoHayResenias" runat="server" Visible="false"></asp:Label>
                                <asp:Repeater ID="RepeaterResenias" runat="server">
                                    <itemtemplate>
                                        <ul class="list-group mb-2">
                                            <li class="list-group-item d-flex justify-content-between align-items-start">
                                                <span class="badge bg-success rounded-pill"><%# Eval("Calificacion") %></span>
                                                <div class="ms-2 me-auto">
                                                    <div class="fw-bold"><%# Eval("Estudiante.NombreCompleto") %></div>
                                                    <%# Eval("Comentario") %>.
                                                </div>
                                            </li>
                                        </ul>
                                    </itemtemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </itemtemplate>

                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
