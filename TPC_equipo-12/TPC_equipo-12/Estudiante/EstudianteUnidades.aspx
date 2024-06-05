<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteUnidades.aspx.cs" Inherits="TPC_equipo_12.EstudianteUnidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card">
            <div class="card-header">
                Lista de Unidades
            </div>
            <div class="card-body">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th scope="col">Nro Unidad</th>
                            <th scope="col">Nombre</th>
                            <th scope="col">Descripción</th>
                            <th scope="col">ID Unidad</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptUnidades" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("NroUnidad") %></td>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("Descripcion") %></td>
                                    <td><%# Eval("IDUnidad") %></td>
                                    <td> <asp:Button ID="ButtonVerLecciones" runat="server" Text="Lecciones" CssClass="btn btn-primary" CommandArgument='<%# Eval("IDUnidad") %>' OnCommand="ButtonVerLecciones_Command"/> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
