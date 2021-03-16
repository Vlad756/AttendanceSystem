using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AttendanceSystemDomain.Models
{
    [Serializable]
    public class LectureReport
    {
        public string LectureTitle { get; set; }
        [XmlArrayItem("student")]
        public List<string> Students { get; set; }
    }
}
