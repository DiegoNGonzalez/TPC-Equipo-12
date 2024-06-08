﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorLecciones.aspx.cs" Inherits="TPC_equipo_12.ProfesorLecciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="ButtonBackUnidadProf" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a Unidades" OnClick="ButtonBackUnidadProf_Click" />
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
                <asp:Repeater ID="rptLeccionesProf" runat="server">
                    <itemtemplate>
                        <tr>
                            <td><%# Eval("NroLeccion") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Descripcion") %></td>
                            <td>
                                <asp:Button ID="ButtonVerMaterialesProf" runat="server" Text="Materiales" CssClass="btn btn-primary" CommandArgument='<%# Eval("IDLeccion") %>' OnCommand="ButtonVerMaterialesProf_Command" />
                            </td>
                        </tr>
                    </itemtemplate>
                </asp:Repeater>
            </tbody>
        </table>

    </div>
</asp:Content>