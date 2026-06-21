using OpenCvSharp;

namespace AoiMeasureTool
{
    internal static class PreprocessPipelineService
    {
        public static Mat Build(Mat grayImage, PreprocessParam preprocessParam)
        {
            return ImageProcessor.Preprocess(grayImage, preprocessParam);
        }
    }
}
