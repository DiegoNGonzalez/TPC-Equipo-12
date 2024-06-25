<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteVerDetalleCurso.aspx.cs" Inherits="TPC_equipo_12.EstudianteVerDetalleCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
     <div class="row">
         <div class="col-md-12">
             <h1>Detalle del Curso</h1>
             <hr />
         </div>
     </div>
     <div class="row">
         <div class="col-md-12">
             <asp:Repeater ID="RepeaterVerDetalleCurso" runat="server" OnItemDataBound="RepeaterVerDetalleCurso_ItemDataBound">
                 <ItemTemplate>
                     <div class="row">
                         <div class="col-md-12">
                             <h2><%# Eval("Nombre") %></h2>
                             <p>Descripcion: <%# Eval("Descripcion") %></p>
                             <p>Duracion: <%# Eval("Duracion") %> hs.</p>
                             <p>Fecha estreno: <%# Eval("Estreno") %></p>
                             <p>Categoria: <%# Eval("Categoria.Nombre") %></p>
                             <h5>Unidades:</h5>
                             <asp:Label ID="LabelNoHayUnidades" runat="server" Visible="false"></asp:Label>
                             <asp:Repeater ID="RepeaterUnidades" runat="server">
                                 <ItemTemplate>
                                     <ol class="list-group list-group-numbered">
                                         <li class="list-group-item d-flex justify-content-between align-items-start">
                                             <div class="ms-2 me-auto">
                                                 <div class="fw-bold"><%# Eval("Nombre") %></div>
                                                 <%# Eval("Descripcion") %>
                                             </div>
                                         </li>
                                     </ol>
                                 </ItemTemplate>
                             </asp:Repeater>
                             <h5>Reseñas:</h5>
                             <asp:Label ID="LabelNoHayResenias" runat="server" Visible="false"></asp:Label>
                             <asp:Repeater ID="RepeaterResenias" runat="server">
                                 <ItemTemplate>
                                     <ul class="list-group mb-2">
                                         <li class="list-group-item d-flex justify-content-between align-items-start">
                                         <span class="badge bg-success rounded-pill"><%# Eval("Calificacion") %></span>
                                             <div class="ms-2 me-auto">
                                                 <div class="fw-bold">Nombre: <%# Eval("Estudiante.NombreCompleto") %></div>
                                                 Comentario: <%# Eval("Comentario") %>.
                                             </div>
                                         </li>
                                     </ul>
                                 </ItemTemplate>
                             </asp:Repeater>
                             <img class="img-fluid" src='<%# Eval("Imagen.URL") %>'>
                         </div>
                     </div>
                 </ItemTemplate>

             </asp:Repeater>
         </div>
     </div>
 </div>
</asp:Content>
