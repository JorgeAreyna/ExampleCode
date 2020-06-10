<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebExample.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/bootstrap.css" rel="stylesheet" type="text/css" />


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<script type="text/javascript">

    function FormatPhone(Obj) {
        if (Obj.value.length == 0 ) {
            Obj.value = Obj.value + "("
        }
       
        if (Obj.value.length == 4 ) {
            Obj.value = Obj.value + ") "
        }
        if (Obj.value.length == 9) {
            Obj.value = Obj.value + "- "
        }
    }

    function numbersonly(e) {
        var unicode = e.charCode ? e.charCode : e.keyCode
        if (unicode != 8) {
            if (unicode < 48 || unicode > 57)
            { return false }
        }
    }

</Script>


    <form id="form1" runat="server">
 
        <div>

              <div class='container content_middle'>              
                   <div class="col-lg-10"   style="width:75%; margin: 0 auto;">
                      <div class="panel panel-default" id ="Div_Datos">
                          <div class="panel-heading">Crud Customer </div>
                           <div class="panel-body">
                                 <div class="form-group required">
                                       <div class="input-group">
                                         <span class="input-group-addon">Company Name:</span>                       
                                           <asp:TextBox ID="Txt_CompanyName" runat="server" class="form-control"  autocomplete="off"  data-validation="required" data-validation-error-msg-container="#error-container"></asp:TextBox>
                                         
                                       </div> 
                                    <div id="error-container"></div>
                                 </div>
                               <div class="form-group required">
                                       <div class="input-group">
                                         <span class="input-group-addon">Contact Name:</span>   
                                           <asp:TextBox ID="Txt_ContactName" runat="server" class="form-control"  autocomplete="off"  data-validation="required" data-validation-error-msg-container="#error-container"></asp:TextBox>                    
                                          <input id="HidCustomer" type="hidden" runat ="server" /> 
                                          <input id="hdAction" type="hidden" runat ="server" /> 
                                       </div> 
                                    <div id="error-container2"></div>
                                 </div>

                                 <div class="form-group required">
                                       <div class="input-group">
                                         <span class="input-group-addon">Adress:</span>   
                                           <asp:TextBox ID="Txt_Address" runat="server" class="form-control"  autocomplete="off"  data-validation="required" data-validation-error-msg-container="#error-container"></asp:TextBox>                    
                                       
                                       </div> 
                                    <div id="error-containerAddress"></div>
                                 </div>

                               <div class="form-group required">
                                       <div class="input-group">
                                         <span class="input-group-addon">City:</span>   
                                           <asp:TextBox ID="Txt_City" runat="server" class="form-control"  autocomplete="off"  data-validation="required" data-validation-error-msg-container="#error-container"></asp:TextBox>                    
                                      
                                       </div> 
                                    <div id="error-containerCity"></div>
                                 </div>


                                <div class="form-group required">
                                       <div class="input-group">
                                         <span class="input-group-addon">City:</span>   
                                           <asp:DropDownList ID="Cmb_Country" runat="server" class="form-control"  autocomplete="off"  data-validation="required" data-validation-error-msg-container="#error-container"></asp:DropDownList>                    
                                      
                                       </div> 

                                 </div>


                                 <div class="form-group required">
                                       <div class="input-group">
                                         <span class="input-group-addon">Phone:</span>
                                         <asp:TextBox ID="Txt_phone" runat="server" class="form-control"  onkeypress ="FormatPhone(this); return numbersonly(event);"   autocomplete="off"  data-validation="required" data-validation-error-msg-container="#error-container" MaxLength="15"></asp:TextBox>                                        
                                       </div> 
                                    <div id="error-container3"></div>
                                 </div>
                                 <div class="form-group required">
                                       <div class="input-group">
                                         <span class="input-group-addon">Zip:</span>
                                             <asp:TextBox ID="Txt_Zip" runat="server" class="form-control"  
                                               autocomplete="off"  data-validation="required" 
                                               data-validation-error-msg-container="#error-container" MaxLength="14"></asp:TextBox>
                                           <input id="Zip" type="hidden" /> 
                                       </div> 
                                    <div id="error-container4"></div>
                                 </div>
                               <div  class="form-group">
                                       <div >
                                           <asp:GridView ID="Gv_Customers" runat="server" CellPadding="4" 
                                               ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Gv_Customers_SelectedIndexChanged">
                                               <AlternatingRowStyle BackColor="White" />
                                               <EditRowStyle BackColor="#2461BF" />
                                               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                               <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                               <RowStyle BackColor="#EFF3FB" />
                                               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                               <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                               <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                               <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                               <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                           </asp:GridView>
                                       </div> 
                                    
                                 </div>
                             <div class="btn-group" role="group" aria-label="...">
                                 <asp:Button ID="BtnSave" runat="server" Text="Save"  value="Save" class="btn btn-default" OnClick="BtnSave_Click"/>
                                  <asp:Button ID="BtnDelete" runat="server" Text="Delete"  value="Delete" class="btn btn-default" OnClick="BtnDelete_Click"/>
                                                                                                    
                            </div>  
                          </div>
                      </div>
                    </div>

               
            </div>
        </div>

    </form>
</body>
</html>