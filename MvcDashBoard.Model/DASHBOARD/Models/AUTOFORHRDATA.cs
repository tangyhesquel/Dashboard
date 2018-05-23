using System.Xml.Serialization;
namespace MvcDashBoard.Model.DASHBOARD.Models
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
    //[XmlRoot(Namespace = "", IsNullable = false, ElementName = "RequestResult")]
    public partial class GetEmployeeCheckTime
    {

        private string fromDateTimeField;

        private string toDateTimeField;

        public string fromDateTime
        {
            get
            {
                return this.fromDateTimeField;
            }
            set
            {
                this.fromDateTimeField = value;
            }
        }

        public string toDateTime
        {
            get
            {
                return this.toDateTimeField;
            }
            set
            {
                this.toDateTimeField = value;
            }
        }
    }

}