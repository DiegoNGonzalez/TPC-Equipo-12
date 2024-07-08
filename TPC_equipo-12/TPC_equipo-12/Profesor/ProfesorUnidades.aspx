<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorUnidades.aspx.cs" Inherits="TPC_equipo_12.ProfesorUnidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../Custom/styles.css" rel="stylesheet" />
<div class="container">
    <div class="mt-3">
        <asp:Button ID="ButtonBackCursosProf" CssClass="btn btn-primary mb-3" runat="server" Text="Volver a la Fabrica" OnClick="ButtonBackCursosProf_Click" />
        <asp:Button ID="ButtonCrearUnidadProf" CssClass="btn btn-primary mb-3 ml-2" runat="server" Text="Crear Unidad" OnClick="ButtonCrearUnidadProf_Click" />
        <asp:Button ID="ButtonEliminarUnidadProf" CssClass="btn btn-warning mb-3 ml-2" runat="server" Text="Habilitar/Deshabilitar Unidad" OnClick="ButtonEliminarUnidadProf_Click" />
        <asp:Button ID="btnEstudiantesXCurso" CssClass="btn btn-primary mb-3 ml-2" runat="server" Text="Estudiantes" OnClick="btnEstudiantesXCurso_Click"/>
    </div>

    <h3 class="text-center mt-3 mb-3">Lista de Unidades</h3>

    <asp:Panel ID="PanelNoHayUnidades" runat="server">
        <hr />
        <div class="text-center">
            <asp:Label ID="LabelNoUnidades" runat="server" CssClass="text-muted"><b>No hay unidades en este curso.</b></asp:Label>
        </div>
    </asp:Panel>

    <asp:Panel ID="PanelHayUnidades" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead class="thead-dark">
                            <tr>
                                <th scope="col">Nro Unidad</th>
                                <th scope="col">Nombre</th>
                                <th scope="col">Descripción</th>
                                <th scope="col">Estado</th>
                                <th scope="col"></th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptUnidadesProf" runat="server" OnItemDataBound="rptUnidadesProf_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="trUnidad" runat="server">
                                        <td><%# Eval("NroUnidad") %></td>
                                        <td><%# Eval("Nombre") %></td>
                                        <td><%# Eval("Descripcion") %></td>
                                        <td><%# GetEstadoText(Eval("Estado")) %></td>
                                        <td>
                                            <asp:Button ID="ButtonVerLeccionesProf" runat="server" Text="Lecciones" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("IDUnidad") %>' OnCommand="ButtonVerLeccionesProf_Command" />
                                        </td>
                                        <td>
                                            <asp:Button ID="ButtonModificarUnidadProf" runat="server" Text="Modificar" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("IDUnidad") %>' OnCommand="ButtonModificarUnidadProf_Command" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
</asp:Content>
