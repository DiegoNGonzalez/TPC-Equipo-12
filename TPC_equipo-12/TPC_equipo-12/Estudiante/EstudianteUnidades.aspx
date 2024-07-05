<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteUnidades.aspx.cs" Inherits="TPC_equipo_12.EstudianteUnidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Custom/styles.css" rel="stylesheet" />
    <div class="container">
        <asp:Button ID="ButtonBackCursos" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a Cursos" OnClick="ButtonBackCursos_Click" />
        <asp:Button ID="btnReseniaCurso" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Reseña Curso" OnClick="btnReseniaCurso_Click" />
            <h2> Lista de Unidades</h2>
            <div class="card-body">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th scope="col">Nro Unidad</th>
                            <th scope="col">Nombre</th>
                            <th scope="col">Descripción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptUnidades" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("NroUnidad") %></td>
                                    <td class="curso-name"><%# Eval("Nombre") %></td>
                                    <td class="descripcion-larga"><%# Eval("Descripcion") %></td>
                                    <td> <asp:Button ID="ButtonVerLecciones" runat="server" Text="Lecciones" CssClass="btn btn-primary" CommandArgument='<%# Eval("IDUnidad") %>' OnCommand="ButtonVerLecciones_Command"/> </td>
                                </tr>
                               
                            </ItemTemplate>
                            
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
</asp:Content>
