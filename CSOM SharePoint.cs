
Create List:

 void CreateList()
        {
            using (ClientContext ctx = new ClientContext("https://sharepointskytraining.sharepoint.com/sites/TSInfo/"))
            {
                ctx.AuthenticationMode = ClientAuthenticationMode.Default;
                ctx.Credentials = new SharePointOnlineCredentials("bijay@SharePointSkyTraining.onmicrosoft.com", GetSPOSecureStringPassword());

                var web = ctx.Web;
                ctx.ExecuteQuery();
                ListCollection listCollection = ctx.Web.Lists;
                ListCreationInformation newList = new ListCreationInformation();
                newList.Title = txtTitle.Text;
                newList.Description = txtDescription.Text;
                newList.Url = txtURL.Text;
                newList.TemplateType = Convert.ToInt32(ListTemplateType.GenericList);
                var list = listCollection.Add(newList);
                list.Update();
                ctx.ExecuteQuery();
                lblStatus.Text = "List Created successfully";
            }
        }

        void AddColumnsToList()
        {
            using (ClientContext ctx = new ClientContext("https://sharepointskytraining.sharepoint.com/sites/TSInfo/"))
            {
                ctx.AuthenticationMode = ClientAuthenticationMode.Default;
                ctx.Credentials = new SharePointOnlineCredentials("bijay@SharePointSkyTraining.onmicrosoft.com", GetSPOSecureStringPassword());

                var web = ctx.Web;
                List targetList = web.Lists.GetByTitle("Company Feedbacks");
                FieldCollection collField = targetList.Fields;

                string fieldSchemaforEmailID = "<Field Type='Text' DisplayName='EmailID' Name='EmailID' />";
                collField.AddFieldAsXml(fieldSchemaforEmailID, true, AddFieldOptions.AddToDefaultContentType);

                string fieldSchemaforFeedback = "<Field Type='Note' DisplayName='Feedbacks' Name='Feedbacks' NumLines='6' RichText='FALSE' />";
                collField.AddFieldAsXml(fieldSchemaforFeedback, true, AddFieldOptions.AddToDefaultContentType);

                ctx.Load(collField);
                ctx.ExecuteQuery();
                
            }
        }

        private static SecureString GetSPOSecureStringPassword()
        {
            try
            {
                var secureString = new SecureString();
                foreach (char c in "Qwe@12345")
                {
                    secureString.AppendChar(c);
                }
                return secureString;
            }
            catch
            {
                throw;
            }
        }


//Add Items to List:


ClientContext clientContext = new ClientContext(siteUrl);  
            List oList = clientContext.Web.Lists.GetByTitle("TestList");  
            ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();  
            ListItem oListItem = oList.AddItem(listCreationInformation);  
            oListItem["Title"] = "Hello World";  
            oListItem.Update();  
            clientContext.ExecuteQuery(); 


Update list Item:

ClientContext clientContext = new ClientContext(siteUrl);  
            List oList = clientContext.Web.Lists.GetByTitle("TestList");  
            ListItem oListItem = oList.GetItemById(5);  
            oListItem["Title"] = "Hello World Updated!!!";  
            oListItem.Update();  
            clientContext.ExecuteQuery(); 


Delete List Item:

ClientContext clientContext = new ClientContext(siteUrl);  
            List oList = clientContext.Web.Lists.GetByTitle("TestList");  
            ListItem oListItem = oList.GetItemById();           
            oListItem.DeleteObject();  
            clientContext.ExecuteQuery();  





void CreateSubSite()
{
using (ClientContext ctx = new ClientContext("https://onlysharepoint2013.sharepoint.com/sites/Rohit/"))
{
ctx.AuthenticationMode = ClientAuthenticationMode.Default;
ctx.Credentials = new SharePointOnlineCredentials("bijay@onlysharepoint2013.onmicrosoft.com", GetSPOSecureStringPassword());
WebCreationInformation creation = new WebCreationInformation();
creation.Title = "TSInfo Finance Site";
creation.Url = "TSInfoFinance";
creation.Description = "Finance site for TSInfo Techonologies";
creation.WebTemplate = "STS#0";
Web newWeb = context.Web.Webs.Add(creation);
context.ExecuteQuery();
}
private static SecureString GetSPOSecureStringPassword()
{
try
{
var secureString = new SecureString();
foreach (char c in "GiveYourPasswordHere")
{
secureString.AppendChar(c);
}
return secureString;
}
catch
{
throw;
}
}
}






void BindList()
        {
            using (ClientContext ctx = new ClientContext("https://onlysharepoint2013.sharepoint.com/sites/Rohit/"))
            {
                ctx.AuthenticationMode = ClientAuthenticationMode.Default;
                ctx.Credentials = new SharePointOnlineCredentials("preeti@onlysharepoint2013.onmicrosoft.com", GetSPOSecureStringPassword());
                Web web = ctx.Web;
                ListCollection lists = web.Lists;
                ctx.Load(lists);
                ctx.ExecuteQuery();

                DataTable table = new DataTable();
                table.Columns.Add("ID", typeof(string));
                table.Columns.Add("Title", typeof(string));
                DataRow dr;
                foreach (List lst in lists)
                {
                    dr = table.NewRow();
                    dr["ID"] = lst.Id;
                    dr["Title"] = lst.Title;
                    table.Rows.Add(dr);
                }

                ddlLists.DataSource = table;
                ddlLists.DataTextField = "Title";
                ddlLists.DataValueField = "ID";
                ddlLists.DataBind();
            }
        }

        void BindListItems()
        {
            //string s = "hellp";
           // var requestUri = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getByTitle(" + "'" + s + "'"+")";
            using (ClientContext ctx = new ClientContext("https://onlysharepoint2013.sharepoint.com/sites/Rohit/"))
            {
                ctx.AuthenticationMode = ClientAuthenticationMode.Default;
                ctx.Credentials = new SharePointOnlineCredentials("preeti@onlysharepoint2013.onmicrosoft.com", GetSPOSecureStringPassword());
                List lstIndustries = ctx.Web.Lists.GetByTitle("Industries");
                CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                ListItemCollection items = lstIndustries.GetItems(query);
                ctx.Load(items);
                ctx.ExecuteQuery();
                string fullItems = string.Empty;
                foreach (ListItem listItem in items)
                {
                    string url= "https://onlysharepoint2013.sharepoint.com/sites/Rohit/Lists/Industries/DispForm.aspx?ID=" + listItem["ID"].ToString();
                    fullItems += "<a href='" + url + "'>" + listItem["Title"].ToString() + "</a>" + "<br>";
                }
                Literal1.Text = fullItems;
            }
        }

        private static SecureString GetSPOSecureStringPassword()
        {
            try
            {
                var secureString = new SecureString();
                foreach (char c in "Welcome@12345")
                {
                    secureString.AppendChar(c);
                }
                return secureString;
            }
            catch
            {
                throw;
            }
        }


Copy list items from One list to another list:

public static void CopyItemsFromOneListToAnotherList()
{

using (ClientContext ctx = new ClientContext(“https://onlysharepoint2013.sharepoint.com/sites/Bhawana/”))
{
ctx.AuthenticationMode = ClientAuthenticationMode.Default;
ctx.Credentials = new SharePointOnlineCredentials(GetSPOAccountName(), GetSPOSecureStringPassword());
ctx.Load(ctx.Web);
ctx.ExecuteQuery();

List sourceList= ctx.Web.Lists.GetByTitle(“SourceList”);
ctx.Load(sourceList);
ctx.ExecuteQuery();

List destList = ctx.Web.Lists.GetByTitle(“DestinationList”);
ctx.Load(sourceList);
ctx.ExecuteQuery();
CamlQuery camlQuery = new CamlQuery();
camlQuery.ViewXml = “<View/>”;
ListItemCollection listItems = sourceList.GetItems(camlQuery);

ctx.Load(listItems);
ctx.ExecuteQuery();

foreach (ListItem item in listItems)
{
ListItemCreationInformation newItemInfo = new ListItemCreationInformation();
ListItem newItem = destList.AddItem(newItemInfo);
newItem[“Title”] = item[“Title”];
newItem[“EmailID”] = item[“EmailID”];
newItem[“Address”] = item[“Address”];
newItem.Update();
}
ctx.ExecuteQuery();
}
}

private static string GetSPOAccountName()
{
try
{
return ConfigurationManager.AppSettings[“SPOAccount”];
}
catch
{
throw;
}
}
private static SecureString GetSPOSecureStringPassword()
{
try
{
var secureString = new SecureString();
foreach (char c in ConfigurationManager.AppSettings[“SPOPassword”])
{
secureString.AppendChar(c);
}

return secureString;
}
catch
{
throw;
}
}


Create Index Column:

public static void CreateIndexForList(string siteURL, string listName)
{
using (ClientContext ctx = new ClientContext(siteURL))
{
ctx.AuthenticationMode = ClientAuthenticationMode.Default;
ctx.Credentials = new SharePointOnlineCredentials(GetSPOAccountName(), GetSPOSecureStringPassword());
var web = ctx.Web;
ctx.ExecuteQuery();
List list = ctx.Web.Lists.GetByTitle(listName);
ctx.Load(list);
ctx.ExecuteQuery();
string Columns = "Title|EmailID";
string[] splitCL = Columns.Split('|');
for (int j = 0; j < splitCL.Length; j++)
{
Field field = list.Fields.GetByTitle(splitCL[j].Trim());
field.Indexed = true;
field.Update();
}
ctx.ExecuteQuery();
}
}
private static string GetSPOAccountName()
{
try
{
return ConfigurationManager.AppSettings["SPOAccount"];
}
catch
{
throw;
}
}
private static SecureString GetSPOSecureStringPassword()
{
try
{
var secureString = new SecureString();
foreach (char c in ConfigurationManager.AppSettings["SPOPassword"])
{
secureString.AppendChar(c);
}
return secureString;
}
catch
{
throw;
}
}


//Activate Workflow Can use App Permission

public static void ActivateWorkflowFeature(string siteURL)
{
Guid WebFeatureID = Guid.Parse("ec918931-c874-4033-bd09-4f36b2e31fef");
using (ClientContext ctx = new ClientContext(siteURL))
{
ctx.AuthenticationMode = ClientAuthenticationMode.Default;
ctx.Credentials = new SharePointOnlineCredentials(GetSPOAccountName(), GetSPOSecureStringPassword());
var web = ctx.Web;
ctx.Load(web);
ctx.ExecuteQuery();
var webFeatures = ctx.Web.Features;
ctx.Load(webFeatures);
ctx.ExecuteQuery();
webFeatures.Add(WebFeatureID, true, FeatureDefinitionScope.None);
ctx.ExecuteQuery();
}
}
private static SecureString GetSPOSecureStringPassword()
{
try
{
var secureString = new SecureString();
foreach (char c in ConfigurationManager.AppSettings["SPOPassword"])
{
secureString.AppendChar(c);
}
return secureString;
}
catch
{
throw;
}
}
private static string GetSPOAccountName()
{
try
{
return ConfigurationManager.AppSettings["SPOAccount"];
}
catch
{
throw;
}
}

//Create Sub Site in SharePoint

WebCreationInformation creation = new WebCreationInformation();
creation.Title = "TSInfo Finance Site";
creation.Url = "TSInfoFinance";
creation.Description = "Finance site for TSInfo Techonologies";
creation.WebTemplate = "STS#0";
Web newWeb = context.Web.Webs.Add(creation);
context.ExecuteQuery();
label1.Text = "Sub site created";

//Create Workflow History List
Create History List

var siteURL = "https://onlysharepoint2013.sharepoint.com/sites/sptraining/";
using (ClientContext ctx = new ClientContext(siteURL))
{
ctx.AuthenticationMode = ClientAuthenticationMode.Default;
ctx.Credentials = new SharePointOnlineCredentials("*****@OnlySharepoint2013.onmicrosoft.com", GetSPOSecureStringPassword());
var web = ctx.Web;
ctx.ExecuteQuery();
ListCollection listCollection = ctx.Web.Lists;
var listName = "NewWorkflowHistory";
var listTitle = "New Workflow History";
ctx.Load(listCollection, lists => lists.Include(list => list.Title).Where(list => list.Title == listTitle));
ctx.ExecuteQuery();
if (listCollection.Count > 0)
{
}
else
{
List ourHistoryList;
ListCreationInformation creationInfo = new ListCreationInformation();
creationInfo.Title = listTitle;
creationInfo.Url = listName;
creationInfo.TemplateType = (int)ListTemplateType.WorkflowHistory;
ourHistoryList = ctx.Web.Lists.Add(creationInfo);
ourHistoryList.Update();
ctx.ExecuteQuery();
ctx.Load(ourHistoryList);
ctx.ExecuteQuery();
Console.WriteLine(ourHistoryList.Title);
Console.ReadLine();
}
}