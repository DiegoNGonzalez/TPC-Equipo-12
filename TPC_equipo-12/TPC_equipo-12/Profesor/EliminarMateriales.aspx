<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="EliminarMateriales.aspx.cs" Inherits="TPC_equipo_12.EliminarMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
                <div class="card w-50 my-5">
                    <div class="card-body">
                        <h5 class="card-title d-flex justify-content-center align-items-center">Elige el numero de material a eliminar</h5>
                        <div class="mb-3">
                            <asp:Label ID="LabelTipoMaterial" runat="server" CssClass="form-label" Text="Nro de Material"></asp:Label>
                            <asp:DropDownList ID="DropDownListNroMaterial" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="ButtonEliminarMaterial" runat="server" Text="Eliminar Material" CssClass="btn btn-danger" OnClick="ButtonEliminarMaterial_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
