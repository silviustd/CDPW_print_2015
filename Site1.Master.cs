using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDPW.BLL;

namespace CDPW
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            string url = Request.RawUrl.ToString();



            if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
            {

                CurrentUser cU = (CurrentUser)HttpContext.Current.Session["cdpUser"];

                if ((url.IndexOf("DCANForm.aspx") > 0 || url.IndexOf("DUSAForm.aspx") > 0) && !cU.NoSignUp )
                {
                    //hlPrint.Visible = true;
                    //hlMySettings.Visible = true;
                    hlCForms.Visible = false;
                    //hlLogout.Visible = true;
                    hlLogout.Visible = true;
                    hlMySettings.Visible = true;

                    //if (cU.NoSignUp)
                    //{
                    //    hlLogout.Visible = false;
                    //    hlMySettings.Visible = false;
                    //}
                    //else
                    //{
                    //    hlLogout.Visible = true;
                    //    hlMySettings.Visible = true;
                    //}
                }
                else if (url.IndexOf("cdpmysettings.aspx") > 0 && !cU.NoSignUp)
                {
                    //hlPrint.Visible = false;
                    hlMySettings.Visible = false;
                    hlCForms.Visible = true;
                    hlLogout.Visible = true;
                }
                else if (url.IndexOf("cdpselectpay.aspx") > 0 && !cU.NoSignUp)
                {
                    hlMySettings.Visible = true;
                    hlCForms.Visible = false;
                    hlLogout.Visible = true;
                }
                else
                {
                    //hlPrint.Visible = false;
                    hlMySettings.Visible = false;
                    hlCForms.Visible = false;
                    hlLogout.Visible = false;
                }

                // show the correct URL for form print
                //if (url.IndexOf("DCANForm.aspx") > 0)
                //    { hlPrint.NavigateUrl = "cdpprintpreview.aspx?Report=CANForm"; }
                //else if (url.IndexOf("DUSAForm.aspx") > 0)
                //    { hlPrint.NavigateUrl = "cdpprintpreview.aspx?Report=USAForm"; }

            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }


        public void hlPrint_Click(Object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            string url = Request.RawUrl.ToString();
            try
            {

                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                {
                    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];

                    if (url.IndexOf("DCANForm.aspx") > 0)
                    {
                        if (uLogin.SavedDCAN)
                            Response.Redirect("cdpprintpreview.aspx?Report=CANForm");
                        else
                        {
                            Response.Redirect("DCANForm.aspx?saved=false");
                        }
                    }
                    else if (url.IndexOf("DUSAForm.aspx") > 0)
                    {
                        //if (uLogin.SavedDUSA)
                        //    Response.Redirect("cdpprintpreview.aspx?Report=USAForm");
                        //else
                        //{
                        //    Response.Redirect("DUSAForm.aspx?saved=false");
                        //}
                    }
                }

            }
            catch (Exception ex) {
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");

        }

        public void hlSave_Click(Object sender, EventArgs e)
        {
        }

        public void hlLogout_Click(Object sender, EventArgs e)
        {
        }

    
    }
}