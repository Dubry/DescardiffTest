using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DescartesDiff.Models
{
    public class DataModel
    {
        public int DataModelId { get; set; }
        public string LeftBase { get; set; }
        public string RightBase { get; set; }
    }
}
