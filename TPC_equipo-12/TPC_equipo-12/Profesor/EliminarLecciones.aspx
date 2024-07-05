<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="EliminarLecciones.aspx.cs" Inherits="TPC_equipo_12.EliminarLecciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
                <div class="card w-50 my-5">
                    <div class="card-body">
                        <h5 class="card-title d-flex justify-content-center align-items-center">Elige una leccion</h5>
                        <div class="mb-3">
                            <asp:Label ID="LabelNombreLeccion" runat="server" CssClass="form-label" Text="Nombre de la Leccion"></asp:Label>
                            <asp:DropDownList ID="DropDownListNombreLeccion" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="d-flex justify-content-between align-items-center">
                            <asp:Button ID="ButtonEstadoLeccion" runat="server" Text="Habilitar/Deshabilitar Leccion" CssClass="btn btn-warning" OnClick="ButtonEstadoLeccion_Click" />
                            <asp:Button ID="ButtonVolver" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="ButtonVolver_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
