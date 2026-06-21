namespace AoiMeasureTool
{
    public sealed class PreprocessParam
    {
        public bool Enabled { get; set; }
        public bool WhiteObject { get; set; }
        public int Threshold { get; set; }
        public int ErodeIterations { get; set; }
        public int DilateIterations { get; set; }
        public int OpenIterations { get; set; }
        public int CloseIterations { get; set; }
    }
}
