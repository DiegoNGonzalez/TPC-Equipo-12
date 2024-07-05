<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteLecciones.aspx.cs" Inherits="TPC_equipo_12.EstudianteLecciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="ButtonBackUnidad" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a Unidades" OnClick="ButtonBackUnidad_Click" />
        <h2>Lista de Lecciones</h2>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Nro Lección</th>
                    <th>Nombre</th>
                    <th>Descripción</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptLecciones" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("NroLeccion") %></td>
                            <td class="curso-name"><%# Eval("Nombre") %></td>
                            <td class="descripcion-larga"><%# Eval("Descripcion") %></td>
                            <td>
                                <asp:Button ID="ButtonVerMateriales" runat="server" Text="Materiales" CssClass="btn btn-primary" CommandArgument='<%# Eval("IDLeccion") %>' OnCommand="ButtonVerMateriales_Command" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

    </div>
</asp:Content>
