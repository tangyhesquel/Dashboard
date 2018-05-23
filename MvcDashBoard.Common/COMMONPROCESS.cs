using log4net;
using MvcDashBoard.Model.DASHBOARD.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
//using System.Web.Mvc;


namespace MvcDashBoard.Common
{
    public class COMMONPROCESS
    {
        //string filePathName = System.AppDomain.CurrentDomain.BaseDirectory+@"machine.xml"; //
        //static string ComputerName = System.Environment.GetEnvironmentVariable("ComputerName");
        //string filePathName = HttpContext.Current.Server.MapPath("~/App_Data/") + ComputerName + "_" + @"machine.xml";
        string filePathName;
        public COMMONPROCESS(string FACTORY_CD, string LINE_CODE)
        {
            filePathName = HttpContext.Current.Server.MapPath("~/App_Data/") + FACTORY_CD + "_" + LINE_CODE + "_" + @"machine.xml";
        }

        String XML_Node_Field = "";
        String XML_Node_Value = "";
        XmlDocument xmlDoc = new XmlDocument();

        BASICINFORMATION_DATA BASICINFORMATION_data=new BASICINFORMATION_DATA();
        public BASICINFORMATION_DATA Read_BASICINFORMATION_File()
        {
            if (File.Exists(filePathName) == true)
            {                
                xmlDoc.Load(filePathName);
                XmlNode xmlNode = xmlDoc.DocumentElement;
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("BaseData");
                nodeList = xmlNode.ChildNodes;

                Type t = BASICINFORMATION_data.GetType();

                foreach (XmlNode xn in nodeList)//遍历所有子节点   
                {
                    XML_Node_Field = xn.Name.Trim();
                    XML_Node_Value = xn.InnerXml.Trim();
               
                    foreach (PropertyInfo pi in t.GetProperties())
                    {
                        if (pi.Name == XML_Node_Field)
                        {
                            //pi.GetValue(XML_Node_Field);
                            switch(pi.Name)
                            {
                                case "FACTORY_CD":
                                    BASICINFORMATION_data.FACTORY_CD = XML_Node_Value;
                                    break;
                                case "GARMENT_TYPE":
                                    BASICINFORMATION_data.GARMENT_TYPE = XML_Node_Value;
                                    break;
                                case "REFRESH_INTERVAL":
                                    {
                                        if (XML_Node_Value.Trim()=="")
                                        {
                                            XML_Node_Value="0";
                                        }
                                        BASICINFORMATION_data.REFRESH_INTERVAL = int.Parse(XML_Node_Value);
                                    }
                                    break;
                                case "LINE_CODE1":
                                    BASICINFORMATION_data.LINE_CODE1 = XML_Node_Value;
                                    break;
                                case "SHIFT_CODE1":
                                    BASICINFORMATION_data.SHIFT_CODE1 = XML_Node_Value;
                                    break;
                                case "LINE_CODE2":
                                    BASICINFORMATION_data.LINE_CODE2 = XML_Node_Value;
                                    break;
                                case "SHIFT_CODE2":
                                    BASICINFORMATION_data.SHIFT_CODE2 = XML_Node_Value;
                                    break;
                                case "SHIFT_CODE3":
                                    BASICINFORMATION_data.SHIFT_CODE3 = XML_Node_Value;
                                    break;
                                case "LINE_CODE3":
                                    BASICINFORMATION_data.LINE_CODE3 = XML_Node_Value;
                                    break;
                                case "SHOWED_DATE3":
                                    if (XML_Node_Value == "")
                                        XML_Node_Value = "2017-01-01";
                                    BASICINFORMATION_data.SHOWED_DATE3 = DateTime.ParseExact(XML_Node_Value, "yyyy-MM-dd",null);
                                    break;
                                case "LANGUAGE":
                                    BASICINFORMATION_data.LANGUAGE = XML_Node_Value;
                                    break;

                                case "HR_MAX_TIME_DIFFERENCE":
                                    {
                                        if (XML_Node_Value.Trim() == "")
                                        {
                                            XML_Node_Value = "0";
                                        }
                                        BASICINFORMATION_data.HR_MAX_TIME_DIFFERENCE = int.Parse(XML_Node_Value);
                                    }
                                    break;
                                case "HR_REFRESH_INTERVAL":
                                    {
                                        if (XML_Node_Value.Trim() == "")
                                        {
                                            XML_Node_Value = "0";
                                        }
                                        BASICINFORMATION_data.HR_REFRESH_INTERVAL = int.Parse(XML_Node_Value);
                                    }
                                    break;
                                case "TARGET_TOTAL_QTY":
                                    BASICINFORMATION_data.TARGET_TOTAL_QTY = int.Parse(XML_Node_Value);
                                    break;
                                case "TARGET_WORK_HOUR":
                                    BASICINFORMATION_data.TARGET_WORK_HOUR = decimal.Parse(XML_Node_Value);
                                    break;
                                case "DISPLAY_TARGET2_DEFECT":
                                    BASICINFORMATION_data.DISPLAY_TARGET2_DEFECT = XML_Node_Value;
                                    break;
                                case "USE_LINE_TARGET2":
                                    BASICINFORMATION_data.USE_LINE_TARGET2 = XML_Node_Value;
                                    break;
                                    

                            }
                        }
                    }
                }
                return BASICINFORMATION_data;
            }
            else
            {
                

                return BASICINFORMATION_data;
            }
        }

        public string Write_BASICINFORMATION_File(string FACTORY_CD, string GARMENT_TYPE, string REFRESH_INTERVAL, string LINE_CODE1, string SHIFT_CODE1, string LINE_CODE2, string SHIFT_CODE2, string LINE_CODE3, string SHIFT_CODE3, string SHOWED_DATE3, string LANGUAGE, string HR_MAX_TIME_DIFFERENCE, string HR_REFRESH_INTERVAL, int TARGET_TOTAL_QTY, decimal TARGET_WORK_HOUR, string DISPLAY_TARGET2_DEFECT, string USE_LINE_TARGET2)
        {
            string JsonResponse = "";
            try
            {
            string[] LineStr = new string[20];
            int i = 0;
            //LineStr[i++] ="<?xml version="1.0" encoding="gb2312" ?>";
            LineStr[i++] = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            LineStr[i++] = "<BasicData>";
            LineStr[i++] = "<FACTORY_CD>" + FACTORY_CD + "</FACTORY_CD>";
            LineStr[i++] = "<GARMENT_TYPE>" + GARMENT_TYPE + "</GARMENT_TYPE>";
            LineStr[i++] = "<REFRESH_INTERVAL>" + REFRESH_INTERVAL + "</REFRESH_INTERVAL>";
            LineStr[i++] = "<LINE_CODE1>" + LINE_CODE1 + "</LINE_CODE1>";
            LineStr[i++] = "<SHIFT_CODE1>" + SHIFT_CODE1 + "</SHIFT_CODE1>";
            LineStr[i++] = "<LINE_CODE2>" + LINE_CODE2 + "</LINE_CODE2>";
            LineStr[i++] = "<SHIFT_CODE2>" + SHIFT_CODE2 + "</SHIFT_CODE2>";
            LineStr[i++] = "<LINE_CODE3>" + LINE_CODE3 + "</LINE_CODE3>";
            LineStr[i++] = "<SHIFT_CODE3>" + SHIFT_CODE3 + "</SHIFT_CODE3>";
            LineStr[i++] = "<SHOWED_DATE3>" + SHOWED_DATE3 + "</SHOWED_DATE3>";
            LineStr[i++] = "<LANGUAGE>" + LANGUAGE + "</LANGUAGE>";

            LineStr[i++] = "<HR_REFRESH_INTERVAL>" + HR_REFRESH_INTERVAL + "</HR_REFRESH_INTERVAL>";
            LineStr[i++] = "<HR_MAX_TIME_DIFFERENCE>" + HR_MAX_TIME_DIFFERENCE + "</HR_MAX_TIME_DIFFERENCE>";

            LineStr[i++] = "<TARGET_TOTAL_QTY>" + TARGET_TOTAL_QTY + "</TARGET_TOTAL_QTY>";
            LineStr[i++] = "<TARGET_WORK_HOUR>" + TARGET_WORK_HOUR + "</TARGET_WORK_HOUR>";

            LineStr[i++] = "<DISPLAY_TARGET2_DEFECT>" + DISPLAY_TARGET2_DEFECT + "</DISPLAY_TARGET2_DEFECT>";
            LineStr[i++] = "<USE_LINE_TARGET2>" + USE_LINE_TARGET2 + "</USE_LINE_TARGET2>";

            LineStr[i++] = "</BasicData>";
            File.WriteAllLines(filePathName, LineStr);
            JsonResponse = "{\"SUCCESS\":true}"; 
           
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

        //public void WriteLog(string Msg, string Type)
        //{

        //    if (Type == "1")
        //    {
        //        ILog log = log41net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //        log.Info(Msg);
        //    }
        //    else
        //    {
        //        log41net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Msg));
        //    }
        //}        
    }
}