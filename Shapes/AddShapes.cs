using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Final_Project.Shapes
{
    [Serializable]
    public class AddShapes
    {
        public int UserId { get; set; }
        public string ShapeType => GetType().Name;
        public double X { get; set; }
        public double Y { get; set; }
    }
}
