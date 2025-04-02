namespace WPF_Final_Project.Shapes
{
    [Serializable]
    public class AddCircle : AddShapes
    {
        public double Radius { get; set; }
        public double Diameter => Radius * 2;
    }
}