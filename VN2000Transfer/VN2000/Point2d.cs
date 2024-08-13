using System;

namespace VN2000Transfer
{
    public struct Point : IFormattable
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point Origin { get; }
        public double Y { get; set; }
        public double X { get; set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return X.ToString() + "," + Y.ToString();
        }
    }

    public static class Point2dExtensions
    {
        public static Point TransformDatum(Point from_lat_long, ref string VNProvs, TransDatum trans = TransDatum.VN2000ToWGSLatlong, double overwride_natural_long = -1, string MuiChieu = "3")
        {
            double result_lat_longX = 0;
            double result_lat_longY = 0;
            TransExts.TransformDatum(from_lat_long.X, from_lat_long.Y, ref result_lat_longX, ref result_lat_longY, ref VNProvs, trans, overwride_natural_long, MuiChieu);
            return new Point(result_lat_longX, result_lat_longY);
        }
    }
}