<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorUnidades.aspx.cs" Inherits="TPC_equipo_12.ProfesorUnidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Custom/styles.css" rel="stylesheet" />
    <div class="container">
        <asp:Button ID="ButtonBackCursosProf" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a la Fabrica" OnClick="ButtonBackCursosProf_Click" />
        <asp:Button ID="ButtonCrearUnidadProf" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Crear Unidad" OnClick="ButtonCrearUnidadProf_Click" />
        <asp:Button ID="ButtonEliminarUnidadProf" CssClass="btn btn-warning mb-3 mt-3" runat="server" Text="Habilitar/Deshabilitar Unidad" OnClick="ButtonEliminarUnidadProf_Click" />

        <asp:Button ID="btnEstudiantesXCurso" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Estudiantes" OnClick="btnEstudiantesXCurso_Click"/>

        <h2>Lista de Unidades</h2>
        <div class="card-body">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th scope="col">Nro Unidad</th>
                        <th scope="col">Nombre</th>
                        <th scope="col">Descripción</th>
                        <th scope="col">Estado</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptUnidadesProf" runat="server" OnItemDataBound="rptUnidadesProf_ItemDataBound">
                        <ItemTemplate>
                            <tr id="trUnidad" runat="server">
                                <td><%# Eval("NroUnidad") %></td>
                                <td class="curso-name"><%# Eval("Nombre") %></td>
                                <td class="descripcion-larga"><%# Eval("Descripcion") %></td>
                                <td><%# GetEstadoText(Eval("Estado")) %></td>
                                <td>
                                    <asp:Button ID="ButtonVerLeccionesProf" runat="server" Text="Lecciones" CssClass="btn btn-primary" CommandArgument='<%# Eval("IDUnidad") %>' OnCommand="ButtonVerLeccionesProf_Command" />

                                </td>
                                <td>
                                    <asp:Button ID="ButtonModificarUnidadProf" runat="server" Text="Modificar" CssClass="btn btn-primary" CommandArgument='<%# Eval("IDUnidad") %>' OnCommand="ButtonModificarUnidadProf_Command" />

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
