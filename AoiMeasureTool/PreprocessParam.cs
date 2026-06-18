namespace AoiMeasureTool
{
    public sealed class PreprocessParam
    {
        public bool Enabled { get; set; } = true;
        public int Threshold { get; set; } = 128;
        public bool WhiteWhenGreaterThanThreshold { get; set; } = true;
        public int ErodeIterations { get; set; }
        public int DilateIterations { get; set; }
        public int OpenIterations { get; set; }
        public int CloseIterations { get; set; }
    }
}
