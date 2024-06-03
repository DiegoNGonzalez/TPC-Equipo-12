<%@ Page Title="" Language="C#" MasterPageFile="~/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorEstudiantes.aspx.cs" Inherits="TPC_equipo_12.ProfesorEstudiantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Estudiantes</h1>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Nombre</th>
                            <th>Apellido</th>
                            <th>DNI</th>
                            <th>Genero</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptEstudiantes" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("Apellido") %></td>
                                    <td><%# Eval("DNI") %></td>
                                    <td><%# Eval("Genero") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
