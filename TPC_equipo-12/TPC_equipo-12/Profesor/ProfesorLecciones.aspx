<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorLecciones.aspx.cs" Inherits="TPC_equipo_12.ProfesorLecciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <link href="../Custom/styles.css" rel="stylesheet" />
<link href="../Custom/styles.css" rel="stylesheet" />

<div class="container">
    <div class="mt-3">
        <asp:Button ID="ButtonBackUnidadProf" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a Unidades" OnClick="ButtonBackUnidadProf_Click" />
        <asp:Button ID="ButtonCrearLeccionProf" CssClass="btn btn-primary mb-3 mt-3 ml-2" runat="server" Text="Crear Lección" OnClick="ButtonCrearLeccionProf_Click" />
        <asp:Button ID="ButtonEstadoLeccionProf" CssClass="btn btn-warning mb-3 mt-3 ml-2" runat="server" Text="Habilitar/Deshabilitar Lección" OnClick="ButtonEliminarLeccionProf_Click" />
    </div>

    <h3 class="text-center mt-3 mb-3">Lista de Lecciones</h3>

    <asp:Panel ID="PanelNoHayLecciones" runat="server">
        <hr />
        <div class="col-md-12 text-center">
            <asp:Label ID="LabelNoLecciones" runat="server" Visible="false"><b>No hay lecciones en esta unidad.</b></asp:Label>
        </div>
    </asp:Panel>

    <asp:Panel ID="PanelHayLecciones" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead class="thead-dark">
                            <tr>
                                <th scope="col">Nro Lección</th>
                                <th scope="col">Nombre</th>
                                <th scope="col">Descripción</th>
                                <th scope="col">Estado</th>
                                <th scope="col"></th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptLeccionesProf" runat="server" OnItemDataBound="rptLeccionesProf_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("NroLeccion") %></td>
                                        <td><%# Eval("Nombre") %></td>
                                        <td><%# Eval("Descripcion") %></td>
                                        <td><%# GetEstadoText(Eval("Estado")) %></td>
                                        <td>
                                            <asp:Button ID="ButtonVerMaterialesProf" runat="server" Text="Materiales" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("IDLeccion") %>' OnCommand="ButtonVerMaterialesProf_Command" />
                                        </td>
                                        <td>
                                            <asp:Button ID="ButtonModificarLeccionProf" runat="server" Text="Modificar" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("IDLeccion") %>' OnCommand="ButtonModificarLeccionProf_Command" />
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
