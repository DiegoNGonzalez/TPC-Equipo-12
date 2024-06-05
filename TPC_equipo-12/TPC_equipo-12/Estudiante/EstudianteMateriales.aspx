<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteMateriales.aspx.cs" Inherits="TPC_equipo_12.EstudianteMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Lista de Materiales</h2>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>ID Material</th>
                    <th>Nombre</th>
                    <th>Tipo de Material</th>
                    <th>URL</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptMateriales" runat="server">
                    <itemtemplate>
                        <tr>
                            <td><%# Eval("IDMaterial") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("TipoMaterial") %></td>
                            <td><%# Eval("URL") %></td>
                        </tr>
                    </itemtemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
