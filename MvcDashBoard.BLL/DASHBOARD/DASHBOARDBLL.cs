using MvcDashBoard.Model.DASHBOARD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcDashBoard.Model.DASHBOARD.Models;
using Newtonsoft.Json;
using MvcDashBoard.DAL.DASHBOARD;

using System.IO;
using System.Web;
using MvcDashBoard.Common;
using MvcDashBoard.Model;
//using Esquel.Util.Log;
using System.Xml;
using Esquel.CommonWebService;

namespace MvcDashBoard.BLL.DASHBOARD
{
    public class DASHBOARDBLL
    {
        public DashboardDAL dashboarddal
        {
            get;
            set;
        }

        public DASHBOARDBLL(string FACTORY_CD)
        {
            dashboarddal = new DashboardDAL(FACTORY_CD);
        }

        public List<FACTORY_CD_DATA> Get_FactoryCD_List(string factorycd)
        {
            try
            {
                List<FACTORY_CD_DATA> FACTORY_CD_data = dashboarddal.Get_FactoryCD_List(factorycd);
                return FACTORY_CD_data;
            }
            catch (Exception ex)
            { return null; }
            finally { }
        }

        public List<GARMENT_TYPE_DATA> Get_GARMENT_TYPE_List()
        {
            try
            {
                List<GARMENT_TYPE_DATA> GARMENT_TYPE_data = dashboarddal.Get_GARMENT_TYPE_List();
                return GARMENT_TYPE_data;
            }
            catch (Exception ex)
            { return null; }
            finally { }
        }

        public List<PRODUCTION_LINE_CD_DATA> Get_LINECD_List(string factorycd, string linecd)
        {
            try
            {
                List<PRODUCTION_LINE_CD_DATA> PRODUCTION_LINE_CD_data = dashboarddal.Get_LINECD_List(factorycd, linecd);
                return PRODUCTION_LINE_CD_data;
            }
            catch (Exception ex)
            { return null; }
            finally { }
        }

        public List<SHIFT_CODE_DATA> Get_SHIFT_CODE_List(string factorycd)
        {
            try
            {
                List<SHIFT_CODE_DATA> SHIFT_CODE_data = dashboarddal.Get_SHIFT_CODE_List(factorycd);
                return SHIFT_CODE_data;
            }
            catch (Exception ex)
            { return null; }
            finally { }
        }


        public string UPDATE_ATTENDTIME(string SEQNO, String SHIFT, string TRX_DATE, string PRODUCTION_LINE_CD)
        {
            string JsonResponse = "";
            string result=dashboarddal.UPDATE_ATTENDTIME(SEQNO, SHIFT, TRX_DATE, PRODUCTION_LINE_CD);
            if (result == "successfully")
                JsonResponse = "{\"SUCCESS\":true}"; 
            else
                JsonResponse = "{\"SUCCESS\":false}"; 
            return JsonResponse;
        }

        public DASHBOARD_ATTENDANCE_LOG GET_DASHBOARD_ATTENDANCE_LOG_DATE(string FACTORY_CD, String LINE_CODE)
        {
            DASHBOARD_ATTENDANCE_LOG GET_DASHBOARD_ATTENDANCE_log = new DASHBOARD_ATTENDANCE_LOG();
            GET_DASHBOARD_ATTENDANCE_log = dashboarddal.GET_DASHBOARD_ATTENDANCE_LOG(FACTORY_CD, LINE_CODE);
            return GET_DASHBOARD_ATTENDANCE_log;
        }

        public string GETEMPLOYEEATTENDTIMEINSERT(string FACTORY_CD, String LINE_CODE, string HR_MAX_TIME_DIFFERENCE, string HR_REFRESH_INTERVAL)
        {
            DateTime dateFrom;
            DateTime dateTo;
            try
            //string HR_MAX_TIME_DIFFERENCE; HR_REFRESH_INTERVAL
            {
                //var HRSend_queue = System.Configuration.ConfigurationManager.AppSettings["ESB_HRSend_queue"];
              
                int HR_MAX_TIME_DIFFERENCE_INT = int.Parse(HR_MAX_TIME_DIFFERENCE);
     
                DASHBOARD_ATTENDANCE_LOG GET_DASHBOARD_ATTENDANCE_log = GET_DASHBOARD_ATTENDANCE_LOG_DATE(FACTORY_CD, LINE_CODE);
                if (GET_DASHBOARD_ATTENDANCE_log.FROM_TIME == null)
                {
                    dateTo = DateTime.Now;
                    dateFrom = dateTo.AddHours(-12);
                }
                else
                {
                    dateFrom = (DateTime)GET_DASHBOARD_ATTENDANCE_log.FROM_TIME;
                    //dateFrom = dateFrom.AddHours(-2);
                    dateTo = (DateTime)GET_DASHBOARD_ATTENDANCE_log.TO_TIME;
                    if (dateFrom < dateTo.AddHours(-1 * HR_MAX_TIME_DIFFERENCE_INT))
                    {
                        if (dateFrom < dateTo.AddHours(-12))
                        {
                            dateFrom = dateTo.AddHours(-12);
                        }
                        else
                        {
                            dateTo = dateFrom.AddHours(HR_MAX_TIME_DIFFERENCE_INT);
                        }
                    }
                    //dateFrom = dateTo.AddHours(-1);
                }
                //List<DASHBOARD_EMPLOYEE_ATTENDTIME> EmployeeCheckTime = new List<DASHBOARD_EMPLOYEE_ATTENDTIME>();
                //EmployeeCheckTime = ProcessHRData(dateFrom, dateTo);

                //dateTo = DateTime.Now;
               // dateTo = dateTo.AddHours(-24);
                //dateFrom = dateFrom.AddHours(-5);

                if (ProcessHRData(FACTORY_CD, LINE_CODE,dateFrom, dateTo)==false)
                {
                    return "{\"SUCCESS\":false}"; 
                }
                else
                {
                    return "{\"SUCCESS\":true}"; 
                }
            }
            catch(Exception ex)
            {
                return "{\"SUCCESS\":false}"; 
            }
            finally
            {
            }
            
        }
   
        public bool ProcessHRData(string FACTORY_CD, String LINE_CODE,DateTime fromDateTime, DateTime toDateTime)
        {
            //COMMONPROCESS COMMONPROCESS = new COMMONPROCESS();
            string logFileConfigPath = System.IO.Directory.GetCurrentDirectory() + "\\Log41Net.config";
            //COMMONPROCESS.WriteLog(logFileConfigPath, "2");

            System.Configuration.AppSettingsReader asReader = new System.Configuration.AppSettingsReader();
            var HRSend_queue = Convert.ToString(asReader.GetValue("ESB_HRSend_queue", typeof(string)));
            var userName = Convert.ToString(asReader.GetValue("ESB_UserID", typeof(string)));
            var password = Convert.ToString(asReader.GetValue("ESB_UserPWD", typeof(string)));
            var Send_Server = Convert.ToString(asReader.GetValue("ESB_Send_Server", typeof(string)));
            //var HRSend_queue = System.Configuration.ConfigurationManager.AppSettings["ESB_HRSend_queue"];
            try
            {
                LogHelper.WriteInfoLog(typeof(string), "TNS Start......");

                ESBReqSender reqSender = new ESBReqSender()
                {
                    serverUrl = Send_Server,
                    jmsExpiration = 100, //optional
                    timeout = 200 //optional 
                };
                GetEmployeeCheckTime query = new GetEmployeeCheckTime()
                {
                    fromDateTime = fromDateTime.ToString(),
                    toDateTime = toDateTime.ToString()
                };

                ESBWrapperOutput output = reqSender.RequestReply(HRSend_queue, query);
                if (output==null)
                {
                    return false;//可能没连上
                }
                if (output.ResponseObject.ResponseData.Data.InnerText=="")
                {
                    DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
                    DashboardBLL.INSERT_DASHBOARD_ATTENDANCE_LOG(FACTORY_CD, fromDateTime, toDateTime, DateTime.Now, LINE_CODE, "Y");
                    return true;//没有数据
                }
                List<DASHBOARD_EMPLOYEE_ATTENDTIME> data = new List<DASHBOARD_EMPLOYEE_ATTENDTIME> ();
                if (output.ResponseObject.ResponseData.Data != null)
                {
                    string Hrdata = output.ResponseObject.ResponseData.Data.OuterXml;
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(Hrdata); //xmldoc.InnerXml = Hrdata;

                    data = XMlProcess(xmldoc,FACTORY_CD);

                    DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
                    
                    if (DashboardBLL.INSERT_ATTENDTIME(data) == "{\"SUCCESS\":false}")
                    {
                        DashboardBLL.INSERT_DASHBOARD_ATTENDANCE_LOG(FACTORY_CD, fromDateTime, toDateTime, DateTime.Now, LINE_CODE, "N");
                    }
                    else
                    {
                        DashboardBLL.INSERT_DASHBOARD_ATTENDANCE_LOG(FACTORY_CD, fromDateTime, toDateTime, DateTime.Now, LINE_CODE, "Y");
                    }
                    //COMMONPROCESS.WriteLog("Stop.", "1");
                    LogHelper.WriteInfoLog(typeof(string), "Stop.");
                    return true;
                 }
                else
                {
                    //COMMONPROCESS.WriteLog("Not Data Is Process,Stop.","1");
                    LogHelper.WriteInfoLog(typeof(string), "Not Data Is Process,Stop.");
                    return true;
                }
            }
            catch (System.Exception exp)
            {
                LogHelper.WriteErrorLog(typeof(string), exp.InnerException);
                //throw (exp);
                return false;
                }
        }

        List<DASHBOARD_EMPLOYEE_ATTENDTIME> XMlProcess(XmlDocument xmldoc, string FACTORY_CD)
        {
            List<DASHBOARD_EMPLOYEE_ATTENDTIME> data_list = new List<DASHBOARD_EMPLOYEE_ATTENDTIME>();
            DASHBOARD_EMPLOYEE_ATTENDTIME data;
            try
            {
                XmlNode xmlNode = xmldoc.DocumentElement;//不知道根节点名称的情况
                XmlNodeList nodelist = xmldoc.FirstChild.FirstChild.ChildNodes;
                //XmlNode xmlNode = xmldoc.SelectSingleNode("GetEmployeeCheckTimeResponse");//知道根节点名称
                //XmlNodeList nodelist = xmldoc.ChildNodes[0].ChildNodes[0].ChildNodes;
                //XmlNodeList nodelist = xmldoc.GetElementsByTagName("EmployeeCheckTime");
                //XmlNodeList nodelist=xmldoc.SelectNodes("/GetEmployeeCheckTimeResponse/GetEmployeeCheckTimeResult");
                String Node_Field = "";
                String Node_Value = "";
                String Node_Value1 = "";
                foreach (XmlNode xn in nodelist)//遍历所有子节点   
                {
                    Node_Field = xn.FirstChild.InnerXml.Trim(); // xn.Name.Trim();
                    //Node_Value = xn.LastChild.InnerXml.Trim().Replace("T", " ");
                    Node_Value = xn["CheckTime"].InnerXml.Trim().Replace("T", " ");
                    Node_Value1 = xn["CreateTime"].InnerXml.Trim().Replace("T", " ");

                    data = new DASHBOARD_EMPLOYEE_ATTENDTIME();
                    data.EMPLOYEE_NO = Node_Field;
                    data.ATTEND_DATE = DateTime.ParseExact(Node_Value, "yyyy-MM-dd HH:mm:ss", null);
                    data.FACTORY_CD = FACTORY_CD;
                    data.CREATE_TIME = DateTime.Now;
                    data.HRIS_CREATE_TIME=DateTime.ParseExact(Node_Value1, "yyyy-MM-dd HH:mm:ss", null);
                    data_list.Add(data);
                    Node_Field = "";
                    Node_Value = "";
                }
            }
            catch (Exception ex)
            { }
            finally
            { }
            return data_list;
        }

        public string PROC_DASHBOARD_GET_PRODUCTION_QTY(string FactoryCd, String Line)
        {
            string JsonResponse = "";
            string result = dashboarddal.PROC_DASHBOARD_GET_PRODUCTION_QTY(FactoryCd, Line);
            if (result == "successfully")
                JsonResponse = "{\"SUCCESS\":true}";
            else
                JsonResponse = "{\"SUCCESS\":false}";
            return JsonResponse;
        }

        public string PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE, string Only1Line)
        {
            string JsonResponse = "";
            string result = dashboarddal.PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME(FactoryCd, Line, SHIFT, TRX_DATE, Only1Line);
            if (result == "successfully")
                JsonResponse = "{\"SUCCESS\":true}";
            else
                JsonResponse = "{\"SUCCESS\":false}";
            return JsonResponse;
        }

        public DASHBOARD_ATTENDANCE_LOG GET_DASHBOARD_ATTENDANCE_LOG(string FactoryCd, String Line)
        {
            DASHBOARD_ATTENDANCE_LOG DASHBOARD_ATTENDANCE_log=new DASHBOARD_ATTENDANCE_LOG();
            DASHBOARD_ATTENDANCE_log = dashboarddal.GET_DASHBOARD_ATTENDANCE_LOG(FactoryCd, Line);
            return DASHBOARD_ATTENDANCE_log;
        }



        public string INSERT_ATTENDTIME(List<DASHBOARD_EMPLOYEE_ATTENDTIME> DASHBOARD_EMPLOYEE_ATTENDTIME)
        {
            string JsonResponse = "";
            string result = dashboarddal.INSERT_ATTENDTIME(DASHBOARD_EMPLOYEE_ATTENDTIME);
            if (result == "successfully")
                JsonResponse = "{\"SUCCESS\":true}";
            else
                JsonResponse = "{\"SUCCESS\":false}";
            return JsonResponse;
        }

        public SHIFT_CODE_BY_TIME GET_SHIFT_CODE_BY_TIME(string FACTORY_CD, string SHIFT, DateTime? TRX_DATE)
        {
            SHIFT_CODE_BY_TIME SHIFT_CODE_BY_TIME = dashboarddal.SHIFT_CODE_BY_TIME(FACTORY_CD, SHIFT, TRX_DATE);
            return SHIFT_CODE_BY_TIME;
        }

        public string Get_DASHBOARD_EMPLOYEE_ATTENDTIME(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE)
        {
            string JsonResponse = "";
            EMPLOYEE_ATTENDTIME EMPLOYEE_ATTENDTIME = new EMPLOYEE_ATTENDTIME();
            try
            {
                List<DASHBOARD_EMPLOYEE_ATTENDTIME> DASHBOARD_EMPLOYEE_attendtime = dashboarddal.Get_DASHBOARD_EMPLOYEE_ATTENDTIME_List(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE);

                EMPLOYEE_ATTENDTIME_QUERY_CRITERIA EMPLOYEE_ATTENDTIME_QUERY_CRITERIA = new EMPLOYEE_ATTENDTIME_QUERY_CRITERIA();
                EMPLOYEE_ATTENDTIME_QUERY_CRITERIA.FACTORY_CD = FACTORY_CD;
                EMPLOYEE_ATTENDTIME_QUERY_CRITERIA.PRODUCTION_LINE_CD = LINE_CODE;
                EMPLOYEE_ATTENDTIME_QUERY_CRITERIA.SHIFT = SHIFT_CODE;
                EMPLOYEE_ATTENDTIME_QUERY_CRITERIA.TRX_DATE = TRX_DATE;

                EMPLOYEE_ATTENDTIME.EMPLOYEE_ATTENDTIME_list = DASHBOARD_EMPLOYEE_attendtime;
                EMPLOYEE_ATTENDTIME.EMPLOYEE_ATTENDTIME_Query_Criteria = EMPLOYEE_ATTENDTIME_QUERY_CRITERIA;
                JsonResponse = "{\"SUCCESS\":true, \"Data\": " + JsonConvert.SerializeObject(EMPLOYEE_ATTENDTIME);
                JsonResponse += "}";
                //JsonResponse = "{\"SUCCESS\":true}"; 
                return JsonResponse;
            }
            catch (Exception ex)
            {
                JsonResponse = "{\"SUCCESS\":false, \"Data\": " + JsonConvert.SerializeObject(null);
                JsonResponse += "}";
                //JsonResponse = "{\"SUCCESS\":false}";
                return JsonResponse;
            }
            finally 
            { 
            }
            
        }

        public string InquirySHIFTData(string FACTORY_CD, string SHIFT_CODE)
        {
            string JsonResponse = "";
           // DASHBOARD_LINE_SHIFT_SETTING LINE_SHIFT_SETTING = new DASHBOARD_LINE_SHIFT_SETTING();
            try
            {
                List<DASHBOARD_LINE_SHIFT_SETTING> LINE_SHIFT_SETTING_list = dashboarddal.InquirySHIFTData(FACTORY_CD, SHIFT_CODE);

                JsonResponse = "{\"SUCCESS\":true, \"Data\": " + JsonConvert.SerializeObject(LINE_SHIFT_SETTING_list);
                JsonResponse += "}";
                //JsonResponse = "{\"SUCCESS\":true}"; 
                return JsonResponse;
            }
            catch (Exception ex)
            {
                JsonResponse = "{\"SUCCESS\":false, \"Data\": " + JsonConvert.SerializeObject(null);
                JsonResponse += "}";
                //JsonResponse = "{\"SUCCESS\":false}";
                return JsonResponse;
            }
            finally
            {
            }
        }

        public string DeleteSHIFTData(string FACTORY_CD, string SHIFT_CODE, string SEQNO)
        {
            string JsonResponse = "";
            try
            {
                string result = dashboarddal.DeleteSHIFTData(FACTORY_CD, SHIFT_CODE,SEQNO);
                if (result == "successfully")
                    JsonResponse = "{\"SUCCESS\":true}";
                else
                    JsonResponse = "{\"SUCCESS\":false}";
                return JsonResponse;
            }
            catch (Exception ex)
            {
                JsonResponse = "{\"SUCCESS\":false}";
                return JsonResponse;
            }
            finally
            {
            }
        }

        public string UpdateShiftData(int SEQNO, string FACTORY_CD, string SHIFT_CODE, string SHIFT_DESC, int SHIFT_SEQ, string SHIFT_FROM, string SHIFT_TO, decimal TIME_INTERVAL, string ACTIVE)
        {
            string JsonResponse = "";
            string returnstr;
            try
            { 
                returnstr=dashboarddal.UpdateShiftData(SEQNO,FACTORY_CD, SHIFT_CODE, SHIFT_DESC, SHIFT_SEQ, SHIFT_FROM, SHIFT_TO,TIME_INTERVAL, ACTIVE);    
                if (returnstr.Equals("successfully")==true)
                {
                    JsonResponse = "{\"SUCCESS\":true}"; 
                    return JsonResponse;
                }
                else
                {
                    JsonResponse = "{\"SUCCESS\":false}";
                    return JsonResponse;
                }
            }
            catch (Exception ex)
            {
                JsonResponse = "{\"SUCCESS\":false}";
                return JsonResponse;
            }
            finally
            {
            }
        }

        public List<DASHBOARD_EMPLOYEE_ATTENDTIME> Get_DASHBOARD_EMPLOYEE_ATTENDTIME_List(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE)
        {
            try
            {
                List<DASHBOARD_EMPLOYEE_ATTENDTIME> DASHBOARD_EMPLOYEE_ATTENDTIME = dashboarddal.Get_DASHBOARD_EMPLOYEE_ATTENDTIME_List(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE);
                return DASHBOARD_EMPLOYEE_ATTENDTIME;
            }
            catch (Exception ex)
            { 
               return null; 
            }
            finally { }
        }

        public RUNNING_BASIC_INFORMATION GET_RUNNING_BASIC_INFORMATION(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE, BASICINFORMATION_DATA BASICINFORMATION_data)
        {
            try
            {
                //获取需要的信息1:来自于本机文件设置
                RUNNING_BASIC_INFORMATION RUNNING_BASIC_INFORMATION_data = new RUNNING_BASIC_INFORMATION();
                RUNNING_BASIC_INFORMATION_data.FACTORY_CD = BASICINFORMATION_data.FACTORY_CD;
                RUNNING_BASIC_INFORMATION_data.GARMENT_TYPE = BASICINFORMATION_data.GARMENT_TYPE;
                RUNNING_BASIC_INFORMATION_data.REFRESH_INTERVAL = BASICINFORMATION_data.REFRESH_INTERVAL;
                //获取需要的信息2:自动判断的信息,日期和班次（date,shift）
                // SHIFT_CODE_BY_TIME SHIFT_CODE_BY_TIME = dashboarddal.SHIFT_CODE_BY_TIME(BASICINFORMATION_data.FACTORY_CD, null, null); //remark by sunny 20180312
                SHIFT_CODE_BY_TIME SHIFT_CODE_BY_TIME = dashboarddal.SHIFT_CODE_BY_LINE_AND_TIME(BASICINFORMATION_data.FACTORY_CD, LINE_CODE, BASICINFORMATION_data.SHIFT_CODE1, BASICINFORMATION_data.SHIFT_CODE2, null); //add by sunny 20180312
                
                RUNNING_BASIC_INFORMATION_data.LANGUAGE = BASICINFORMATION_data.LANGUAGE;
                RUNNING_BASIC_INFORMATION_data.TRX_DATE = SHIFT_CODE_BY_TIME.TRX_DATE;
                //tangyh 2018.05.14
                //if (string.IsNullOrEmpty(SHIFT_CODE) || string.IsNullOrWhiteSpace(SHIFT_CODE))
                   // SHIFT_CODE = SHIFT_CODE_BY_TIME.SHIFT_CODE;
                //tangyh 3018.05.19
                if ((SHIFT_CODE == SHIFT_CODE_BY_TIME.SHIFT_CODE)||(SHIFT_CODE==null))
                {
                    RUNNING_BASIC_INFORMATION_data.CHANGESHIFT = "N";
                }
                else
                {
                    RUNNING_BASIC_INFORMATION_data.CHANGESHIFT = "Y";
                }

                RUNNING_BASIC_INFORMATION_data.SHIFT_CODE = SHIFT_CODE_BY_TIME.SHIFT_CODE;

                //获取需要的信息3:根据自动判断的班次信息和本机设置的信息判断应该显示的组别

                /*if (SHIFT_CODE == BASICINFORMATION_data.SHIFT_CODE1)
                {
                    RUNNING_BASIC_INFORMATION_data.LINE_CODE = BASICINFORMATION_data.LINE_CODE1;
                }
                else if (SHIFT_CODE == BASICINFORMATION_data.SHIFT_CODE2)
                {
                    RUNNING_BASIC_INFORMATION_data.LINE_CODE = BASICINFORMATION_data.LINE_CODE2;
                }*/  // remark by sunny 20180312  

                RUNNING_BASIC_INFORMATION_data.LINE_CODE = LINE_CODE; //add by sunny 20180312 

                if ((string.IsNullOrEmpty(BASICINFORMATION_data.SHIFT_CODE3) == false) && (string.IsNullOrEmpty(BASICINFORMATION_data.LINE_CODE3) == false))
                {
                    RUNNING_BASIC_INFORMATION_data.SHIFT_CODE = BASICINFORMATION_data.SHIFT_CODE3;
                    RUNNING_BASIC_INFORMATION_data.LINE_CODE = BASICINFORMATION_data.LINE_CODE3;
                    RUNNING_BASIC_INFORMATION_data.TRX_DATE = BASICINFORMATION_data.SHOWED_DATE3;
                }

                /*if ((string.IsNullOrEmpty(BASICINFORMATION_data.LINE_CODE1) == false) || (string.IsNullOrEmpty(BASICINFORMATION_data.LINE_CODE2) == false))
                {
                    RUNNING_BASIC_INFORMATION_data.Only1Line = "Y";
                    if (string.IsNullOrEmpty(BASICINFORMATION_data.LINE_CODE1) == false)
                    {
                        RUNNING_BASIC_INFORMATION_data.LINE_CODE = BASICINFORMATION_data.LINE_CODE1;
                    }
                    else
                    {
                        RUNNING_BASIC_INFORMATION_data.LINE_CODE = BASICINFORMATION_data.LINE_CODE2;
                    }

                }
                else
                {
                    RUNNING_BASIC_INFORMATION_data.Only1Line = "N";
                }*/ // remark by sunny 20180312

                RUNNING_BASIC_INFORMATION_data.Only1Line = "Y"; // add by sunny 20180312 support only one Line .

                RUNNING_BASIC_INFORMATION_data.HR_MAX_TIME_DIFFERENCE = BASICINFORMATION_data.HR_MAX_TIME_DIFFERENCE;
                RUNNING_BASIC_INFORMATION_data.HR_REFRESH_INTERVAL = BASICINFORMATION_data.HR_REFRESH_INTERVAL;
                RUNNING_BASIC_INFORMATION_data.TARGET_TOTAL_QTY = BASICINFORMATION_data.TARGET_TOTAL_QTY;

                //revise  target total qty with new logic by sunny 20180312 start
                TargetOutPut targetOutput = dashboarddal.GET_TARGET_OUTPUT(BASICINFORMATION_data.FACTORY_CD, RUNNING_BASIC_INFORMATION_data.LINE_CODE, RUNNING_BASIC_INFORMATION_data.TRX_DATE.ToString("yyyy-MM-dd")); //add by sunny 20180312
                if (targetOutput.Total_Product_Qty != null) 
                {
                    RUNNING_BASIC_INFORMATION_data.TARGET_TOTAL_QTY = targetOutput.Total_Product_Qty + BASICINFORMATION_data.TARGET_TOTAL_QTY;
                    if (BASICINFORMATION_data.SHIFT_CODE1.Trim() != "" && BASICINFORMATION_data.SHIFT_CODE2.Trim() != "") 
                    {
                        RUNNING_BASIC_INFORMATION_data.TARGET_TOTAL_QTY = int.Parse(Math.Round((Double)(RUNNING_BASIC_INFORMATION_data.TARGET_TOTAL_QTY / 2), 0, MidpointRounding.AwayFromZero).ToString());
                    }
                }
                //revise  target total qty with new logic by sunny 20180312 end


                RUNNING_BASIC_INFORMATION_data.TARGET_WORK_HOUR = BASICINFORMATION_data.TARGET_WORK_HOUR;
                RUNNING_BASIC_INFORMATION_data.DISPLAY_TARGET2_DEFECT = BASICINFORMATION_data.DISPLAY_TARGET2_DEFECT;
                RUNNING_BASIC_INFORMATION_data.USE_LINE_TARGET2=BASICINFORMATION_data.USE_LINE_TARGET2;
                return RUNNING_BASIC_INFORMATION_data;
            }
            catch (Exception ex)
            { 
                return null; 
            }
            finally 
            { 
            }    
        }

        public DASHBOARD_SHOW_DATA Get_DashBoard_Data_List(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE, string REFRESH)
        {
            try
            {
                DASHBOARD_SHOW_DATA DASHBOARD_SHOW_data = new DASHBOARD_SHOW_DATA();
                DASHBOARD_SHOW_data = DASHBOARDSHOW_DATA(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE,1);
                return DASHBOARD_SHOW_data;
            }
            catch (Exception ex)
            { 
                return null; 
            }
            finally 
            {
            }            
        }

        public string DASHBOARDSHOWInquiry(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE)
        {
            string JsonResponse;
            try
            {
                DASHBOARD_SHOW_DATA DASHBOARD_SHOW_data = new DASHBOARD_SHOW_DATA();
                DASHBOARD_SHOW_data=DASHBOARDSHOW_DATA(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE,2);
                JsonResponse = "{\"SUCCESS\":true, \"Data\": " + JsonConvert.SerializeObject(DASHBOARD_SHOW_data);
                JsonResponse += "}";
            }
            catch (Exception ex)
            {
                JsonResponse = "{\"SUCCESS\":false, \"Data\": " + JsonConvert.SerializeObject(null);
                JsonResponse += "}";
            }
            finally { }
            return JsonResponse;
        }

        public DASHBOARD_SHOW_DATA DASHBOARDSHOW_DATA(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE,int Flag)
        {
            DASHBOARD_SHOW_DATA DASHBOARD_SHOW_data = new DASHBOARD_SHOW_DATA();
            try
            {
                BASICINFORMATION_DATA BASICINFORMATION_data = new BASICINFORMATION_DATA();
                //读文件的基本信息
                COMMONPROCESS ReadAndWrite_File = new COMMONPROCESS(FACTORY_CD, LINE_CODE);
                BASICINFORMATION_data = ReadAndWrite_File.Read_BASICINFORMATION_File();
                if (BASICINFORMATION_data == null)
                {
                    return DASHBOARD_SHOW_data;
                }

                RUNNING_BASIC_INFORMATION RUNNING_BASIC_INFORMATION = new RUNNING_BASIC_INFORMATION();

                //if (LINE_CODE == BASICINFORMATION_data.LINE_CODE1)
                //{
                //   // SHIFT_CODE = BASICINFORMATION_data.SHIFT_CODE1;  //remark by sunny 20180312
                //}
                //else if (LINE_CODE == BASICINFORMATION_data.LINE_CODE2)
                //{
                //    // SHIFT_CODE = BASICINFORMATION_data.SHIFT_CODE2;  //remark by sunny 20180312
                //}
                //else  if (LINE_CODE == BASICINFORMATION_data.LINE_CODE3)
                //{
                //    SHIFT_CODE = BASICINFORMATION_data.SHIFT_CODE3;
                //}
                //else
                //{
                //    return DASHBOARD_SHOW_data;
                //}

                RUNNING_BASIC_INFORMATION = GET_RUNNING_BASIC_INFORMATION(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE, BASICINFORMATION_data);
                //tangyh 2018.05.16
                
                if (LINE_CODE == BASICINFORMATION_data.LINE_CODE3)
                {
                    SHIFT_CODE = BASICINFORMATION_data.SHIFT_CODE3;
                }
                else
                    SHIFT_CODE = RUNNING_BASIC_INFORMATION.SHIFT_CODE;

                DASHBOARD_SHOW_data.RUNNING_BASIC_INFORMATION = RUNNING_BASIC_INFORMATION;

                DASHBOARD_SHOW_data.BASICINFORMATION_data = BASICINFORMATION_data;

                if (string.IsNullOrEmpty(FACTORY_CD))
                    FACTORY_CD = DASHBOARD_SHOW_data.RUNNING_BASIC_INFORMATION.FACTORY_CD;
                if (string.IsNullOrEmpty(LINE_CODE))
                    LINE_CODE = DASHBOARD_SHOW_data.RUNNING_BASIC_INFORMATION.LINE_CODE;
                if (string.IsNullOrEmpty(SHIFT_CODE))
                    SHIFT_CODE = DASHBOARD_SHOW_data.RUNNING_BASIC_INFORMATION.SHIFT_CODE;

                if (TRX_DATE == null)
                    TRX_DATE = DASHBOARD_SHOW_data.RUNNING_BASIC_INFORMATION.TRX_DATE;
                if (RUNNING_BASIC_INFORMATION.CHANGESHIFT.Equals("Y")==true)
                     return DASHBOARD_SHOW_data;

                //数据处理
                dashboarddal.PROC_DASHBOARD_GET_PRODUCTION_QTY(FACTORY_CD, LINE_CODE);
                if (Flag == 1 && (string.IsNullOrEmpty(FACTORY_CD)==false))
                {  //启动时处理考勤
                    GETEMPLOYEEATTENDTIMEINSERT(FACTORY_CD, LINE_CODE, RUNNING_BASIC_INFORMATION.HR_MAX_TIME_DIFFERENCE.ToString(), RUNNING_BASIC_INFORMATION.HR_REFRESH_INTERVAL.ToString());
                    dashboarddal.PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE, DASHBOARD_SHOW_data.RUNNING_BASIC_INFORMATION.Only1Line);
                }
                //end 数据处理
                FN_DASHBOARD_SHOW_DATA_Result FN_DASHBOARD_SHOW_DATA_Result = new FN_DASHBOARD_SHOW_DATA_Result();
                FN_DASHBOARD_SHOW_DATA_Result = dashboarddal.Get_DashBoard_Data(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE,RUNNING_BASIC_INFORMATION.TARGET_TOTAL_QTY,RUNNING_BASIC_INFORMATION.TARGET_WORK_HOUR);
                DASHBOARD_SHOW_data.FN_DASHBOARD_SHOW_DATA_Result = FN_DASHBOARD_SHOW_DATA_Result;

                FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result = new FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result();
                FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result = dashboarddal.Get_DashBoard_Previous_Data(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE, RUNNING_BASIC_INFORMATION.TARGET_TOTAL_QTY, RUNNING_BASIC_INFORMATION.TARGET_WORK_HOUR);
                DASHBOARD_SHOW_data.FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result = FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result;

                List<FN_DASHBOARD_TIME_INTERVAL_QTY_Result> FN_DASHBOARD_TIME_INTERVAL_QTY_Result = new List<FN_DASHBOARD_TIME_INTERVAL_QTY_Result>();
                FN_DASHBOARD_TIME_INTERVAL_QTY_Result = dashboarddal.Get_DashBoard_Time_Interval_Qty(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE, RUNNING_BASIC_INFORMATION.TARGET_TOTAL_QTY, RUNNING_BASIC_INFORMATION.TARGET_WORK_HOUR);
                DASHBOARD_SHOW_data.FN_DASHBOARD_TIME_INTERVAL_QTY_Result = FN_DASHBOARD_TIME_INTERVAL_QTY_Result;

                List<DEFECT_TOP> DEFECT_TOP=new List<DEFECT_TOP>();
                DEFECT_TOP=dashboarddal.Get_DEFECT_TOP(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE);

                
                DASHBOARD_SHOW_data.DEFECT_TOP = DEFECT_TOP;

            }
            catch (Exception ex)
            {
            }
            finally { }
            return DASHBOARD_SHOW_data;
        }

        public List<FN_DASHBOARD_TIME_INTERVAL_QTY_Result> Get_DashBoard_Time_Interval_Qty(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE, int TARGET_TOTAL_QTY, decimal TARGET_WORK_HOUR)
        {
            List<FN_DASHBOARD_TIME_INTERVAL_QTY_Result> FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result = dashboarddal.Get_DashBoard_Time_Interval_Qty(FactoryCd, Line, SHIFT, TRX_DATE, TARGET_TOTAL_QTY, TARGET_WORK_HOUR);
            return FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result;
        }

        public FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result Get_DashBoard_Previous_Data(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE, int TARGET_TOTAL_QTY, decimal TARGET_WORK_HOUR)
        {
            FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result = dashboarddal.Get_DashBoard_Previous_Data(FactoryCd, Line, SHIFT, TRX_DATE, TARGET_TOTAL_QTY, TARGET_WORK_HOUR);
            return FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result;
        }
        public FN_DASHBOARD_SHOW_DATA_Result Get_DashBoard_Data(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE,int TARGET_TOTAL_QTY, decimal TARGET_WORK_HOUR)
        {
            FN_DASHBOARD_SHOW_DATA_Result FN_DASHBOARD_SHOW_DATA_Result = dashboarddal.Get_DashBoard_Data(FactoryCd, Line, SHIFT, TRX_DATE, TARGET_TOTAL_QTY, TARGET_WORK_HOUR);
            return FN_DASHBOARD_SHOW_DATA_Result;
        }

        public IEnumerable<MvcDashBoard.Model.DASHBOARD.Models.OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION> PROC_GET_DASHBOARD_FORM_DEFINITION(string formName, string lang)
        {

            return dashboarddal.PROC_GET_DASHBOARD_FORM_DEFINITION(formName, lang);
        }

      
        public string INSERT_DASHBOARD_ATTENDANCE_LOG(string factorycd, DateTime fromtime, DateTime totime, DateTime createtime, string linecd, string status)
        {

                string JsonResponse = "";
                string result = dashboarddal.INSERT_DASHBOARD_ATTENDANCE_LOG(factorycd, fromtime, totime, createtime, linecd, status);
                if (result == "successfully")
                    JsonResponse = "{\"SUCCESS\":true}";
                else
                    JsonResponse = "{\"SUCCESS\":false}";
                return JsonResponse;
        }
    }
}
