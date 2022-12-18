namespace Packages
{
    [Serializable]
    public class Vector
    {
        public int Id { get; set; }
        public float[] Values { get; set; }

        public int TreatmentsNumber { get; set; }

        public bool IsResolving { get; set; }

        public bool IsEndMessage = false;
    }
}