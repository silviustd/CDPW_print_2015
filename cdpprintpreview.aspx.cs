using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using CDPW.DAL;
using CDPW.BLL;

namespace CDPW
{
    public partial class cdpprintpreview : System.Web.UI.Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        long WAppUserId = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            
            try
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null && HttpContext.Current.Session["cdpUser"] is CurrentUser)
                
                {
                    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];

                    //string url = Request.RawUrl.ToString();
                    string Raport = Request.QueryString["Report"];
                    if (Raport.Contains("CANForm"))
                    {
                        if (!uLogin.SavedDCAN)
                            Response.Redirect("DCANForm.aspx?saved=false",false);
                    }
                    else if (Raport.Contains("USAForm") || Raport.Contains("USAFormB"))
                    {
                        if (!uLogin.SavedDUSA)
                            Response.Redirect("DUSAForm.aspx?saved=false",false);
                    }

                    if (!IsPostBack)    
                    {
                        WAppUserId = uLogin.WAppUserId;
                        
                        rwPrintPreview.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

                        switch (Raport)
                        {
                            case "USAForm":
                                rwPrintPreview.LocalReport.ReportPath = Server.MapPath("USForm.rdlc");
                                rwPrintPreview.LocalReport.DataSources.Add(new ReportDataSource(rwPrintPreview.LocalReport.GetDataSourceNames()[0], Load_USReport_Data(WAppUserId)));
                                break;
                            case "USAFormB":
                                rwPrintPreview.LocalReport.ReportPath = Server.MapPath("USForm_Back.rdlc");
                                rwPrintPreview.LocalReport.DataSources.Add(new ReportDataSource(rwPrintPreview.LocalReport.GetDataSourceNames()[0], Load_USReport1_Data(WAppUserId)));
                                break;
                            case "CANForm":
                                rwPrintPreview.LocalReport.ReportPath = Server.MapPath("CanadianForm.rdlc");
                                rwPrintPreview.LocalReport.DataSources.Add(new ReportDataSource(rwPrintPreview.LocalReport.GetDataSourceNames()[0], Load_CNDReport_Data(WAppUserId)));
                                rwPrintPreview.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                                break;
                        }

                        //string Raport = Request.QueryString["report"];
                        if (Raport == "CANForm")
                        {
                            rwPrintPreview.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                        }
                    }
                }
                else
                {
                    Response.Redirect("cdplogin.aspx");
                }
            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message;
                //Response.Redirect("Error.aspx");
                components.Error_Show.Show(phError, true, ltrError, ex, phReport, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }


        private DataTable Load_USReport_Data(long UserId)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            DataTable table = null;

            try
            {
                CDP_Dataset.Get_US_FlightInfoDataTable dt = new CDP_Dataset.Get_US_FlightInfoDataTable();
                CDP_DatasetTableAdapters.Get_US_FlightInfoTableAdapter ta = new CDP_DatasetTableAdapters.Get_US_FlightInfoTableAdapter();
                ta.Fill(dt, UserId);
                table = dt;
            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message;
                //Response.Redirect("Error.aspx");
                components.Error_Show.Show(phError, true, ltrError, ex, phReport, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return table;
            
        }


        private DataTable Load_USReport1_Data(long UserId)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            DataTable table = null;

            try
            {
                CDP_Dataset.Get_US_FlightInfo2DataTable dt = new CDP_Dataset.Get_US_FlightInfo2DataTable();
                CDP_DatasetTableAdapters.Get_US_FlightInfo2TableAdapter ta = new CDP_DatasetTableAdapters.Get_US_FlightInfo2TableAdapter();
                ta.Fill(dt, UserId);
                table = dt;
            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message;
                //Response.Redirect("Error.aspx");
                components.Error_Show.Show(phError, true, ltrError, ex, phReport, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return table;
            
        }

        private DataTable Load_CNDReport_Data(long UserId)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            DataTable table = null;

            try
            {
                CDP_Dataset.Get_CAN_FlightInfoDataTable dt = new CDP_Dataset.Get_CAN_FlightInfoDataTable();
                CDP_DatasetTableAdapters.Get_CAN_FlightInfoTableAdapter ta = new CDP_DatasetTableAdapters.Get_CAN_FlightInfoTableAdapter();
                ta.Fill(dt, UserId);
                table = dt;
            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message;
                //Response.Redirect("Error.aspx");
                components.Error_Show.Show(phError, true, ltrError, ex, phReport, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return table;
            
        }


        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            try
            {
                switch (e.DataSourceNames[0])
                {
                    case "dsPersons":
                        CDP_Dataset.Get_CAN_PersonsDataTable dtPersons = new CDP_Dataset.Get_CAN_PersonsDataTable();
                        CDP_DatasetTableAdapters.Get_CAN_PersonsTableAdapter taPersons = new CDP_DatasetTableAdapters.Get_CAN_PersonsTableAdapter();
                        taPersons.Fill(dtPersons, long.Parse(e.Parameters["AddressId"].Values[0]));
                        e.DataSources.Add(new ReportDataSource(e.DataSourceNames[0], (DataTable)dtPersons));
                        break;
                    case "dsGoods1":
                        CDP_Dataset.Get_CAN_Goods1DataTable dtGoods1 = new CDP_Dataset.Get_CAN_Goods1DataTable();
                        CDP_DatasetTableAdapters.Get_CAN_Goods1TableAdapter taGoods1 = new CDP_DatasetTableAdapters.Get_CAN_Goods1TableAdapter();
                        taGoods1.Fill(dtGoods1, long.Parse(e.Parameters["AddressId"].Values[0]));
                        e.DataSources.Add(new ReportDataSource(e.DataSourceNames[0], (DataTable)dtGoods1));
                        break;
                    case "dsGoods2":
                        CDP_Dataset.Get_CAN_Goods2DataTable dtGoods2 = new CDP_Dataset.Get_CAN_Goods2DataTable();
                        CDP_DatasetTableAdapters.Get_CAN_Goods2TableAdapter taGoods2 = new CDP_DatasetTableAdapters.Get_CAN_Goods2TableAdapter();
                        taGoods2.Fill(dtGoods2, long.Parse(e.Parameters["AddressId"].Values[0]));
                        e.DataSources.Add(new ReportDataSource(e.DataSourceNames[0], (DataTable)dtGoods2));
                        break;
                }

            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message;
                //Response.Redirect("Error.aspx");
                components.Error_Show.Show(phError, true, ltrError, ex, phReport, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }
    }
}