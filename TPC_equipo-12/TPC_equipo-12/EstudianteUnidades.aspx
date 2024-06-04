<%@ Page Title="" Language="C#" MasterPageFile="~/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteUnidades.aspx.cs" Inherits="TPC_equipo_12.EstudianteUnidades" %>

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
                            <th scope="col">ID Unidad</th>
                            <th scope="col">Nombre</th>
                            <th scope="col">Nro Unidad</th>
                            <th scope="col">Descripción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptUnidades" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("IDUnidad") %></td>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("NroUnidad") %></td>
                                    <td><%# Eval("Descripcion") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
