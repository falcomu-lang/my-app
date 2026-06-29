using System;
using OpenCvSharp;

namespace AoiMeasureTool
{
    public static class ImageProcessor
    {
        public static Mat Preprocess(Mat grayImage, PreprocessParam param)
        {
            if (grayImage == null || grayImage.Empty())
            {
                throw new ArgumentException("Gray image is empty.", nameof(grayImage));
            }

            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            var binary = new Mat();
            if (param.UseDualThreshold)
            {
                var lower = Math.Min(param.Threshold, param.UpperThreshold);
                var upper = Math.Max(param.Threshold, param.UpperThreshold);
                Cv2.InRange(grayImage, new Scalar(lower), new Scalar(upper), binary);
            }
            else
            {
                var thresholdType = param.WhiteObject
                    ? ThresholdTypes.Binary
                    : ThresholdTypes.BinaryInv;

                Cv2.Threshold(grayImage, binary, param.Threshold, 255, thresholdType);
            }

            using (var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3)))
            {
                if (param.ErodeIterations > 0)
                {
                    Cv2.Erode(binary, binary, kernel, iterations: param.ErodeIterations);
                }

                if (param.DilateIterations > 0)
                {
                    Cv2.Dilate(binary, binary, kernel, iterations: param.DilateIterations);
                }

                if (param.OpenIterations > 0)
                {
                    Cv2.MorphologyEx(binary, binary, MorphTypes.Open, kernel, iterations: param.OpenIterations);
                }

                if (param.CloseIterations > 0)
                {
                    Cv2.MorphologyEx(binary, binary, MorphTypes.Close, kernel, iterations: param.CloseIterations);
                }
            }

            return binary;
        }
    }
}
