<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Software24" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
       RouteConfig.RegisterRoutes(RouteTable.Routes);
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Exception exception = Server.GetLastError().GetBaseException();
        string err = "Error Caught in Application_Error event\n" +
           "Error in: " + Request.Url.ToString() +
           "\nError Message:" + exception.Message.ToString() +
           "\nStack Trace:" + exception.StackTrace.ToString();

        Response.Redirect("/error?url=" + HttpContext.Current.Request.Url.ToString() + "&message=" + exception.Message.ToString());
    }
</script>
