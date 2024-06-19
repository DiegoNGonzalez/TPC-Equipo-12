<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteInscripciones.aspx.cs" Inherits="TPC_equipo_12.EstudianteInscripciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Mis inscripciones</h1>

                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Nro Inscripcion</th>
                            <th>Nombre</th>
                            <th>Apellido</th>
                            <th>Fecha Inscripcion</th>
                            <th>Curso</th>
                            <th>Estado</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptInscripciones" runat="server">
                            <ItemTemplate>

                                <tr>
                                    <td><%# Eval("IdInscripcion") %></td>
                                    <td><%# Eval("Usuario.Nombre") %></td>
                                    <td><%# Eval("Usuario.Apellido") %></td>
                                    <td><%# Eval("FechaInscripcion") %></td>
                                    <td><%# Eval("Curso.Nombre") %></td>
                                    <td><%# 
                                            ((char)Eval("Estado")) == 'A' ? "Aceptada" : 
                                            ((char)Eval("Estado")) == 'P' ? "Pendiente" : "Rechazado"
                                             %>

                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>

            </div>
        </div>
    </div>
</asp:Content>
