using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDashBoard.Model.DASHBOARD.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FACTORY_CD_DATA
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string FACTORY_CD { get; set; }
    }

    public class GARMENT_TYPE_DATA
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string GARMENT_TYPE { get; set; }
    }

    public class PRODUCTION_LINE_CD_DATA
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string PRODUCTION_LINE_CD { get; set; }
    }

    public class SHIFT_CODE_DATA
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string SHIFT_CODE { get; set; }
    }

}
