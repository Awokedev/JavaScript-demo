To work with JavaScript Object model we have to use SP.js file which is located in the LAYOUTS folder.

E:\Program Files\Common Files\microsoft shared\Web Server Extensions\16\TEMPLATE\LAYOUTS

Server side object model or client side object model requires a starting point to work with SharePoint objects. The starting point is known as contexts.

The context object provides an entry point into the associated application programming interface (API) that can be used to gain access to other objects.

ExecuteOrDelayUntilScriptLoaded(clickMethod,'sp.js'): Here it will call the javascript function after the sp.js load successfully in the page.

Example-1:

<input type='button' value='Get Site Title' onclick="getSiteTitle();"/>
<br />

<script language="javascript" type="text/javascript">
var site;
function getSiteTitle() {
var clientContext = new SP.ClientContext.get_current();
site=clientContext.get_web();
clientContext.load(site);
clientContext.executeQueryAsync(success, failure);
}
function success() {
alert(site.get_title());
}
function failure() {
alert("Failure!");
}
</script>

Example-2:

<script language="javascript" type="text/javascript">

ExecuteOrDelayUntilScriptLoaded(getSiteTitle,'sp.js');

function getSiteTitle()
{
var context=new SP.ClientContext.get_current();
var web=context.get_web();
context.load(web);
context.executeQueryAsync(success,failure);
}

function success()
{
alert(web.get_title());
}

function failure()
{
alert('Failed');
}

</script>

Example-3:

<input type='button' id='btnGetUserName' value='Get User Name' onclick="GetLoggedInUserName();"/>
 
<script type="text/javascript">
var currentUser;
function GetLoggedInUserName()
{
var context = new SP.ClientContext.get_current();
var website = context.get_web();
currentUser = website.get_currentUser();
context.load(currentUser);
context.executeQueryAsync(onQuerySucceeded,onQueryFailed);
} 
function onQuerySucceeded()
{
 alert(currentUser.get_loginName());
}
function onQueryFailed()
{
alert('request failed ');
}
</script>

Example-4

<input type='button' value='Get All Groups' onclick="clickMethod();"/>
<br />
<script language="javascript" type="text/javascript">
var siteGroups ='';
function clickMethod() {
   
var clientContext = new SP.ClientContext.get_current();
siteGroups = clientContext.get_web().get_siteGroups();
clientContext.load(siteGroups);
clientContext.executeQueryAsync(onQuerySucceeded, onQueryFailed);
}
function onQuerySucceeded() {
var allGroups='Group Name:       Group ID '+'\n';
for (var i =0 ; i < siteGroups.get_count(); i++)
{
allGroups +=siteGroups.itemAt(i).get_title()+'       '+siteGroups.itemAt(i).get_id()+'\n';
}
alert(allGroups);
}
function onQueryFailed() {
    
alert('Request failed.');
}
</script>


==================

<input type='button' value='Get Users from Group 9' onclick="retrieveAllUsersInGroup();"/>
<br />
<p id="demo"></p>
<script language="javascript" type="text/javascript">
var collUser;
function retrieveAllUsersInGroup() {
var clientContext = new SP.ClientContext("https://sharepointskytraining.sharepoint.com/sites/TSInfo/");
var collGroup = clientContext.get_web().get_siteGroups();
var oGroup = collGroup.getById(9);
collUser = oGroup.get_users();
clientContext.load(collUser);
clientContext.executeQueryAsync(Function.createDelegate(this, this.onQuerySucceeded), Function.createDelegate(this, this.onQueryFailed));
}
function onQuerySucceeded() {
var userInfo = '';
var userEnumerator = collUser.getEnumerator();
while (userEnumerator.moveNext()) {
var oUser = userEnumerator.get_current();
userInfo += '<br>User: ' + oUser.get_title() +
'<br>ID: ' + oUser.get_id() +
'<br>Email: ' + oUser.get_email() +
'<br>Login Name: ' + oUser.get_loginName();
}
document.getElementById("demo").innerHTML = userInfo;
}
function onQueryFailed(sender, args) {
alert('Request failed. ' + args.get_message() + '\n' + args.get_stackTrace());
}
</script>



Example-5:

<input type='button' value='Add User to SharePoint Group' onclick="AddUserToSharePointGroup();"/>
<br />
<script language="javascript" type="text/javascript">
var user;
var spGroup;
function AddUserToSharePointGroup() {   
var clientContext = new SP.ClientContext.get_current();
 
var siteGroups = clientContext.get_web().get_siteGroups();
spGroup=siteGroups.getById(7);
user=clientContext.get_web().get_currentUser();
alert(user.email);
var userCollection=spGroup.get_users();
userCollection.addUser(user);
clientContext.load(user);
clientContext.load(spGroup);
clientContext.executeQueryAsync(onQuerySucceeded, onQueryFailed);
}

function onQuerySucceeded() {
alert('success');
}

function onQueryFailed() {
alert('Request failed.');
}
</script>

Example-6:

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<h2>Create List using JavaScript Object Model (jsom) SharePoint Online</h2><br/>
<table>
<tr>
<td>List Title:</td>
<td><input type="text" id="txtTitle" size="40"/></td>
</tr>
<tr>
<td>List URL:</td>
<td><input type="text" id="txtURL" size="40"/></td>
</tr>
<tr>
<td>List Description:</td>
<td><textarea rows="4" cols="50" id="txtDescription"></textarea></td>
</tr>
<tr>
<td>
</td>
<td>
<input id="btnCreate" type="button" value="Create List" />
</td>
</tr>
</table>
<script language="javascript" type="text/javascript">
$(document).ready(function () {
$("#btnCreate").click(function () {
CreateList();
});
});
function CreateList() {

var title = $("#txtTitle").val();

var url = $("#txtURL").val();

var description = $("#txtDescription").val();

var context = new SP.ClientContext.get_current();

var curWeb = context.get_web();

var listCreationInfo = new SP.ListCreationInformation();
listCreationInfo.set_title(title);
listCreationInfo.set_url(url);
listCreationInfo.set_description(description);
listCreationInfo.set_templateType(SP.ListTemplateType.genericList);
var myList = curWeb.get_lists().add(listCreationInfo);
context.executeQueryAsync(onSuccess,onFailure);
}
function onSuccess() {
alert('List Created Successfully');
}
function onFailure() {
alert('Error while creating the list');
}
</script>

Example-7:

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script src="https://onlysharepoint2013.sharepoint.com/sites/Raju/SiteAssets/Preetihtml/SpProperties.js"></script>
<head>
<meta charset="utf-8" />
<title></title>
</head>
<body>
<h2>Retrive Web Site Details</h2>
Site Title: <p id="pTitle"></p>
Site description: <p id="pdescript"></p>
Site template: <p id="ptemp"></p>
</body>
</html>

/ JavaScript source code
ExecuteOrDelayUntilScriptLoaded(clickMethod, 'sp.js');

var site;
function clickMethod() {
var clientContext = new SP.ClientContext.get_current();
site = clientContext.get_web();
clientContext.load(site);
clientContext.executeQueryAsync(success, failure);
}
function success() {
$("#pTitle").html(site.get_title());
$("#pdescript").html(site.get_description());
$("#ptemp").html(site.get_webTemplate());
}
function failure() {
alert("Failure!");
}

Example-8:

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script>
$(document).ready(function(){
$("#btnClick").click(function(){
yFunction();
});
});
function myFunction()
{
//Your jsom code will be here
var clientContext = new SP.ClientContext.get_current();
site=clientContext.get_web();
clientContext.load(site);
clientContext.executeQueryAsync(success, failure);
}
function success() {
alert(site.get_title());
}
function failure() {
alert("Failure!");
}
</script>
<input type='button' id='btnClick' value='Get Site Title'/>


Example-9:

<p style="font-size:25px;" width="500px;">Industries (JSOM):</p>
<hr>
<p id="industries" style="font-size:15px;" width="500px;"></p>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<script>
$(document).ready(function () {
ExecuteOrDelayUntilScriptLoaded(getIndustriesJSOM, "sp.js");
});
var colltaskListItem;
function getIndustriesJSOM()
{
var clientContext = new SP.ClientContext.get_current();
var oList = clientContext.get_web().get_lists().getByTitle('Departments');
var camlQuery = new SP.CamlQuery();
colltaskListItem = oList.getItems(camlQuery);
clientContext.load(colltaskListItem);
clientContext.executeQueryAsync(onSuccess, onFailure);
}
function onSuccess()
{
var allIndustries='';
var listItemEnumerator = colltaskListItem.getEnumerator();
while (listItemEnumerator.moveNext()) {
var oListItem = listItemEnumerator.get_current();
var url=_spPageContextInfo.webAbsoluteUrl +"/Lists/Departments/DispForm.aspx?ID="+oListItem.get_item('ID');
allIndustries+= "<a href='" + url + "'>" + oListItem.get_item('Title') + "</a>" + "<br />";
}
$("#industries").html(allIndustries);
}
function onFailure()
{
alert('Some error occurred');
}
</script>


Example-10:

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script>
$(document).ready(function() {
SP.SOD.executeFunc('sp.js', 'SP.ClientContext', geyByID);
});
function geyByID ()
{
var itemId = 1;
var ctx = new SP.ClientContext.get_current();
var customList = ctx.get_web().get_lists().getByTitle('MyList');
var listItem = customList.getItemById(itemId);
ctx.load(listItem);
ctx.executeQueryAsync(
function(){
alert('Item title: ' + listItem.get_item("Title"));
},
function(sender, args){ alert('Error: ' + args.get_message()); }
);
}
</script>







