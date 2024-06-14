<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorEstudiantesXCurso.aspx.cs" Inherits="TPC_equipo_12.ProfesorEstudiantesXCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container">
    <div class="row">
        <div class="col-md-12">
            <asp:Button ID="btnVolver" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver" OnClick="btnVolver_Click" />
            <h1>Estudiantes Inscriptos</h1>

            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>Nro Inscripcion</th>
                        <th>Nombre</th>
                        <th>Apellido</th>
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
                                <td><%# Eval("FechaInscripcion") %></td>
                                
                                
                              
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

        </div>
    </div>
</div>
</asp:Content>
