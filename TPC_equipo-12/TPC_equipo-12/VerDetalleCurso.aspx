<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerDetalleCurso.aspx.cs" Inherits="TPC_equipo_12.VerDetalleCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="ButtonVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-3 mb-3" OnClick="ButtonVolver_Click" />
        <div class="row">
            <div class="col-md-12">
                <asp:Repeater ID="RepeaterVerDetalleCurso" runat="server" OnItemDataBound="RepeaterVerDetalleCurso_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-center align-items-center">
                                    <h1><%# Eval("Nombre") %></h1>
                                </div>
                                <div class="d-flex align-items-center mb-4">
                                    <div class="flex-shrink-0">
                                        <asp:Image ID="ImagenCurso" runat="server" CssClass="img-fluid" Style="width: 300px; height: auto;" ImageUrl='<%# "~/Images/" + Eval("Imagen.URL") %>' />
                                    </div>
                                    <div class="ms-4">
                                        <h5>Descripción:</h5>
                                        <p><%# Eval("Descripcion") %></p>
                                    </div>
                                </div>
                                <h5>Duración:</h5>
                                <p><%# Eval("Duracion") %> hs.</p>
                                <h5>Fecha estreno:</h5>
                                <p><%# Eval("Estreno") %></p>
                                <h5>Categoría:</h5>
                                <p><%# Eval("Categoria.Nombre") %></p>
                                <h5>Unidades:</h5>
                                <asp:Label ID="LabelNoHayUnidades" runat="server" Visible="false"></asp:Label>
                                <asp:Repeater ID="RepeaterUnidades" runat="server">
                                    <ItemTemplate>
                                        <ol class="list-group list-group-numbered">
                                            <li class="list-group-item d-flex justify-content-between align-items-start">
                                                <div class="ms-2 me-auto">
                                                    <div class="fw-bold"><%# Eval("Nombre") %></div>
                                                    <%# Eval("Descripcion") %>
                                                </div>
                                            </li>
                                        </ol>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <h5 class="mt-4">Reseñas:</h5>
                                <asp:Label ID="LabelNoHayResenias" runat="server" Visible="false"></asp:Label>
                                <asp:Repeater ID="RepeaterResenias" runat="server">
                                    <ItemTemplate>
                                        <ul class="list-group mb-4">
                                            <li class="list-group-item d-flex justify-content-between align-items-start">
                                                <span class="badge bg-success rounded-pill"><%# Eval("Calificacion") %></span>
                                                <div class="ms-2 me-auto">
                                                    <div class="fw-bold"><%# Eval("Estudiante.NombreCompleto") %></div>
                                                    <%# Eval("Comentario") %>.
                                                </div>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
