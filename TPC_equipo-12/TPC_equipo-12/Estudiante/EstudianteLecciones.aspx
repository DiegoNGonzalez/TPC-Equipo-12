<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteLecciones.aspx.cs" Inherits="TPC_equipo_12.EstudianteLecciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Lista de Lecciones</h2>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>ID Lección</th>
                    <th>Nro Lección</th>
                    <th>Nombre</th>
                    <th>Descripción</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptLecciones" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("IDLeccion") %></td>
                            <td><%# Eval("NroLeccion") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Descripcion") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
