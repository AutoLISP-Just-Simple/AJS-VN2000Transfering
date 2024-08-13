using Autodesk.AutoCAD.Geometry;
using VN2000Transfer;

namespace AutoCAD_VN2000
{
    internal static class ImporVn2000Transfer
    {
        public static Point3d TransformDatum(this Point3d from_lat_long, TransDatum trans = TransDatum.VN2000ToWGSLatlong, double overwride_natural_long = -1, string MuiChieu = "3")
        {
            double result_lat_longX = 0;
            double result_lat_longY = 0;
            TransExts.TransformDatum(from_lat_long.X, from_lat_long.Y, ref result_lat_longX, ref result_lat_longY, trans, overwride_natural_long, MuiChieu);
            return new Point3d(result_lat_longX, result_lat_longY, 0);
        }

        public static Point3d TransformDatum(this Point3d from_lat_long, ref string VNProvs, TransDatum trans = TransDatum.VN2000ToWGSLatlong, double overwride_natural_long = -1, string MuiChieu = "3")
        {
            double result_lat_longX = 0;
            double result_lat_longY = 0;
            TransExts.TransformDatum(from_lat_long.X, from_lat_long.Y, ref result_lat_longX, ref result_lat_longY, ref VNProvs, trans, overwride_natural_long, MuiChieu);
            return new Point3d(result_lat_longX, result_lat_longY, 0);
        }
    }
}