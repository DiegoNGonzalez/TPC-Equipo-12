﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="Inscripciones.aspx.cs" Inherits="TPC_equipo_12.Inscripciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Inscripciones Pendientes</h1>
                <asp:Panel ID="PanelInscripciones" runat="server" Visible="false">

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Nro Inscripcion</th>
                                <th>Nombre</th>
                                <th>Apellido</th>
                                <th>DNI</th>
                                <th>Curso</th>
                                <th>Fecha Inscripcion</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptInscripciones" runat="server">
                                <ItemTemplate>

                                    <tr>
                                        <td><%# Eval("IdInscripcion") %></td>
                                        <td><%# Eval("Usuario.Nombre") %></td>
                                        <td><%# Eval("Usuario.Apellido") %></td>
                                        <td><%# Eval("Usuario.DNI") %></td>
                                        <td><%# Eval("FechaInscripcion") %></td>
                                        <td><%# Eval("Curso.Nombre") %></td>
                                        <td>
                                            <asp:Button Text="Aceptar" runat="server" CssClass="btn btn-success" ID="btnAceptarInscripcion" OnClick="btnAceptarInscripcion_Click" CommandArgument='<%# Eval("IdInscripcion") %>' autopostback="true" />
                                            <asp:HiddenField ID="hiddenId" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Button Text="Rechazar" runat="server" CssClass="btn btn-danger" ID="btnEliminarInscripcion" OnClick="btnEliminarInscripcion_Click" CommandArgument='<%# Eval("IdInscripcion") %>' autopostback="true" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </asp:Panel>
                <div class="d-flex justify-content-center align-items-start">
                    <asp:Label ID="LabelNoHayInscripciones" runat="server" CssClass="display-4 font-weight-bold mt-5" Visible="false">No hay inscripciones pendientes!</asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
