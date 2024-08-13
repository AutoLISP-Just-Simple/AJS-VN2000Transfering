using System;
using System.Diagnostics;

namespace VN2000Transfer
{
    public enum TransDatum
    { VN2000ToWGSLatlong, WGS2LatlongToVN20000 };

    public static class TransExts
    {
        public static void TransformDatum(double from_lat_longX, double from_lat_longY, ref double result_lat_longX, ref double result_lat_longY, ref string VNProvs, TransDatum trans = TransDatum.VN2000ToWGSLatlong, double overwride_natural_long = -1, string MuiChieu = "3")
        {
            //"www.lisp.vn"
            if (overwride_natural_long < 0)
            {
                if (VNProvs.ToUpper().Contains("AUTO") && trans == TransDatum.WGS2LatlongToVN20000)
                {
                    VNProvs = VNProvinces.FindProvinceByLong(from_lat_longY);
                }
            }

            var naturallong = overwride_natural_long > 1 ? overwride_natural_long : VNProvinces.GetNaturalLong(VNProvs);
            if (naturallong > 1)
                TransformDatum(from_lat_longX, from_lat_longY, ref result_lat_longX, ref result_lat_longY, trans, naturallong, MuiChieu);

            //"www.lisp.vn"
        }

        public static void TransformDatum(double from_lat_longX, double from_lat_longY, ref double result_lat_longX, ref double result_lat_longY, TransDatum trans = TransDatum.VN2000ToWGSLatlong, double overwride_natural_long = 105.5, string MuiChieu = "3")
        {
            //"www.lisp.vn"
            var naturallong = overwride_natural_long.ToRadian();
            if (naturallong > 1)
            {
                if (trans == TransDatum.VN2000ToWGSLatlong)
                {
                    var naturallat = 0.0;             //D7

                    var D7 = naturallat;
                    var D8 = naturallong;

                    var C10 = 500000.0;
                    var C11 = 0.0;
                    var scalefactor = MuiChieu.Contains("3") ? 0.9999 : 0.9996;
                    var C12 = scalefactor;

                    double a = 6378137;             //C15
                    var C15 = a;
                    double b = 6356752.31424496;
                    double _1f = 298.25722356;      //C16

                    //VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO
                    //VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO
                    //VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO//VN2000_GEO
                    double e2 = 2 * (1 / _1f) - (1 / _1f) * (1 / _1f);      //C17
                    var C17 = e2;
                    double e22 = e2 / ((1 - (1 / _1f)) * (1 - (1 / _1f)));  //C18
                    var C18 = e22;

                    Debug.Print("e2=" + e2);
                    Debug.Print("e22=" + e22);

                    double e1 = (1 - Math.Sqrt(1 - e2)) / (1 + Math.Sqrt(1 - e2));
                    Debug.Print("e1=" + e1);
                    var C21 = e1;

                    double i25 = C17 / 4.0;
                    double i26 = 3.0 * C17 * C17 / 64.0;
                    double i27 = 5 * C17 * C17 * C17 / 256.0;
                    double i28 = 3.0 * C17 / 8.0;
                    double i29 = 3.0 * C17 * C17 / 32.0;
                    double i30 = 45 * C17 * C17 * C17 / 1024.0;
                    double i31 = 15 * C17 * C17 / 256.0;
                    double i32 = 45 * C17 * C17 * C17 / 1024.0;
                    double i33 = 35 * C17 * C17 * C17 / 3072.0;

                    Debug.Print("i25-33=" + i25 + "/" + i26 + "/" + i27 + "/" + i28 + "/" + i29 + "/" + i30 + "/" + i31 + "/" + i32 + "/" + i33);

                    double M0 = a * ((1 - i25 - i26 - i27) * naturallat - (i28 + i29 + i30) * Math.Sin(2.0 * naturallat) + (i31 + i32) * Math.Sin(4.0 * naturallat) - i33 * Math.Sin(6.0 * D7));
                    var C22 = M0;

                    var M1 = C22 + (from_lat_longY - C11) / C12;
                    var C23 = M1;

                    var u1 = C23 / (C15 * (1.0 - (C17 / 4.0) - 3.0 * (C17 * C17 / 64.0) - 5.0 * (C17 * C17 * C17 / 256.0)));
                    var C24 = u1;

                    var F1 = C24 + ((3.0 * C21 / 2.0) - 27.0 * C21 * C21 * C21 / 32.0) * Math.Sin(2.0 * C24)
                        + (21.0 * (C21 * C21) / 16.0 - 55.0 * (C21 * C21 * C21 * C21) / 32.0) * Math.Sin(4 * C24) + (151.0 * (C21 * C21 * C21) / 96.0) * Math.Sin(6.0 * C24)
                        + (1097.0 * (C21 * C21 * C21 * C21) / 512.0) * Math.Sin(8.0 * C24);
                    var C25 = F1;

                    var T1 = Math.Tan(C25) * Math.Tan(C25);
                    var C26 = T1;

                    var v1 = C15 / Math.Sqrt(1 - C17 * (Math.Sin(C25) * Math.Sin(C25)));
                    var C27 = v1;

                    var p1 = C15 * (1.0 - C17) / Math.Pow((1.0 - C17 * (Math.Sin(C25) * Math.Sin(C25))), 1.5);
                    var C28 = p1;

                    var c1 = C18 * (Math.Cos(C25) * Math.Cos(C25));
                    var C29 = c1;

                    var D = (from_lat_longX - C10) / (C12 * C27);
                    var C30 = D;

                    double lat = C25 - (C27 * Math.Tan(C25) / C28) * (C30 * C30 / 2.0 - (5.0 + 3.0 * C26 + 10.0 * C29 - 4.0 * (C29 * C29) - 9.0 * C18)
                        * (C30 * C30 * C30 * C30) / 24.0 + (61.0 + 90.0 * C26 + 298.0 * C29 + 45.0 * (C26 * C26) - 252.0 * C18 - 3.0 * C29 * C29) * Math.Pow(C30, 6.0) / 720.0);
                    //lat = C25;

                    double lon = D8 + (C30 - (1.0 + 2.0 * C26 + C29) * (Math.Pow(C30, 3.0)) / 6.0 + (5.0 - 2.0 * C29 + 28.0 * C26 - 3.0 * (C29 * C29)
                        + 8.0 * C18 + 24.0 * C26 * C26) * (Math.Pow(C30, 5.0)) / 120.0) / Math.Cos(C25);

                    //GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC
                    //GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC
                    //GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC

                    double H = 0;
                    var v = a / Math.Sqrt(1.0 - e2 * (Math.Sin(lat)) * (Math.Sin(lat)));
                    Debug.Print("C17=v=" + v);

                    var X = (H + v) * Math.Cos(lat) * Math.Cos(lon);
                    Debug.Print("X=" + X);

                    var Y = (H + v) * Math.Cos(lat) * Math.Sin(lon);
                    Debug.Print("Y=" + Y);

                    var Z = ((1.0 - e2) * v + H) * Math.Sin(lat);
                    Debug.Print("Z=" + Z);

                    double Xgeo = X, Ygeo = Y, Zgeo = Z;
                    Trans7Params(X, Y, Z, ref Xgeo, ref Ygeo, ref Zgeo, trans);

                    //GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO
                    //GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO
                    //GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO
                    var p = Math.Sqrt(Xgeo * Xgeo + Ygeo * Ygeo);
                    var F = Math.Atan(Zgeo * a / (p * b));
                    Debug.Print("v/p/F=" + v + "/" + p + "/" + F);

                    var newlat = Math.Atan((Zgeo + e22 * b * Math.Pow(Math.Sin(F), 3.0)) / (p - e2 * a * Math.Pow(Math.Cos(F), 3.0))).ToDegree();
                    var newlon = Math.Atan(Ygeo / Xgeo);
                    var d27 = newlon > 0 ? -(180.0 - newlon.ToDegree()) : newlon.ToDegree();
                    var d28 = newlon > 0 ? newlon.ToDegree() : (180.0 + newlon.ToDegree());
                    var d29 = naturallong < 0 ? d27 : d28;

                    newlon = d29;

                    Debug.Print("newlat/newlon=" + newlat + "/" + newlon);

                    //retpt = new Point2d(newlat, newlon);
                    result_lat_longX = newlat;
                    result_lat_longY = newlon;
                }
                else if (trans == TransDatum.WGS2LatlongToVN20000)
                {
                    var naturallat = 0.0;             //D7

                    //naturallong = 107.5.ToRadian();

                    var D7 = naturallat;

                    var C10 = 500000.0;
                    var C11 = 0.0;
                    //var scalefactor = MuiChieu.Contains("3") ? 0.9999 : 0.9996;
                    //var C12 = scalefactor;

                    double a = 6378137;             //C15
                    var C15 = a;
                    double b = 6356752.31424496;
                    double _1f = 298.25722356;      //C16

                    //UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO
                    //UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO
                    //UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO//UTM_GEO
                    double e2 = 2 * (1 / _1f) - (1 / _1f) * (1 / _1f);      //C17
                    double e22 = e2 / ((1 - (1 / _1f)) * (1 - (1 / _1f)));  //C18

                    double lat = from_lat_longX.ToRadian();
                    double lon = from_lat_longY.ToRadian();
                    //lat = 0.203635817;
                    //lon = 1.89344633;

                    Debug.Print("lat=" + lat + "\nlong=" + lon + " D7=" + D7);

                    //GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC
                    //GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC
                    //GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC//GEO_GEOCENTRIC

                    double H = 0;
                    var v = a / Math.Sqrt(1.0 - e2 * Math.Sin(lat) * (Math.Sin(lat)));
                    Debug.Print("C17=v=" + v);

                    var X = (H + v) * Math.Cos(lat) * Math.Cos(lon);
                    Debug.Print("X=" + X);

                    var Y = (H + v) * Math.Cos(lat) * Math.Sin(lon);
                    Debug.Print("Y=" + Y);

                    var Z = ((1.0 - e2) * v + H) * Math.Sin(lat);
                    Debug.Print("Z=" + Z);

                    //Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params
                    //Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params
                    //Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params//Transfer7params

                    double Xgeo = X, Ygeo = Y, Zgeo = Z;
                    Trans7Params(X, Y, Z, ref Xgeo, ref Ygeo, ref Zgeo, trans);

                    //GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO
                    //GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO
                    //GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO//GEOCENTRIC_GEO
                    var p = Math.Sqrt(Xgeo * Xgeo + Ygeo * Ygeo);
                    var F = Math.Atan(Zgeo * a / (p * b));
                    Debug.Print("v/p/F=" + v + "/" + p + "/" + F);

                    var newlat = Math.Atan((Zgeo + e22 * b * Math.Pow(Math.Sin(F), 3.0)) / (p - e2 * a * Math.Pow(Math.Cos(F), 3.0))).ToDegree();
                    var newlon = Math.Atan(Ygeo / Xgeo);
                    var d27 = newlon > 0 ? -(180.0 - newlon.ToDegree()) : newlon.ToDegree();
                    var d28 = newlon > 0 ? newlon.ToDegree() : (180.0 + newlon.ToDegree());
                    var d29 = naturallong < 0 ? d27 : d28;

                    newlon = d29;

                    //GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000
                    //GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000
                    //GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000//GEO_VN2000
                    var d4 = newlat.ToRadian();
                    var d5 = newlon.ToRadian();

                    var d6 = naturallat;
                    var d7 = naturallong;

                    var C17 = e2;
                    double I25 = C17 / 4.0;
                    double I26 = 3.0 * C17 * C17 / 64.0;
                    double I27 = 5 * C17 * C17 * C17 / 256.0;
                    double I28 = 3.0 * C17 / 8.0;
                    double I29 = 3.0 * C17 * C17 / 32.0;
                    double I30 = 45 * C17 * C17 * C17 / 1024.0;
                    double I31 = 15 * C17 * C17 / 256.0;
                    double I32 = 45 * C17 * C17 * C17 / 1024.0;
                    double I33 = 35 * C17 * C17 * C17 / 3072.0;

                    var C12 = 0.9999;

                    var C21 = (d5 - d7) * Math.Cos(d4);
                    var C22 = Math.Tan(d4) * Math.Tan(d4);
                    var C23 = C15 / Math.Sqrt(1 - e2 * Math.Sin(d4) * Math.Sin(d4));
                    var C24 = e2 * Math.Cos(d4) * Math.Cos(d4) / (1 - e2);
                    var C25 = C15 * ((1 - I25 - I26 - I27) * d4 - (I28 + I29 + I30) * Math.Sin(2 * d4) + (I31 + I32) * Math.Sin(4 * d4) - I33 * Math.Sin(6 * d4));
                    var C26 = C15 * ((1 - I25 - I26 - I27) * d6 - (I28 + I29 + I30) * Math.Sin(2 * d6) + (I31 + I32) * Math.Sin(4 * d6) - I33 * Math.Sin(6 * d6));
                    var newX = C10 + C12 * C23 * (C21 + ((1 - C22 + C24) * Math.Pow(C21, 3) / 6) + ((5 - 18 * C22 + Math.Pow(C22, 2) + 72 * C24 - 58 * e22) * Math.Pow(C21, 5) / 120));
                    var newY = C11 + C12 * (C25 - C26 + (C23 * Math.Tan(d4) * ((((C21 * C21) / 2) + (5 - C22 + 9 * C24 + 4 * Math.Pow(C24, 2)) * Math.Pow(C21, 4) / 24) + ((61 - 58 * C22 + C22 * C22 + 600 * C24 - 330 * e22) * Math.Pow(C21, 6) / 720))));

                    result_lat_longX = newX;
                    result_lat_longY = newY;
                }
            }
            //"www.lisp.vn"
            return;
        }

        public const double DELTA_X = -191.90441429;
        public const double DELTA_Y = -39.30318279;
        public const double DELTA_Z = -111.45032830;
        public const double ROT_X = -0.00928836;
        public const double ROT_Y = 0.01975479;
        public const double ROT_Z = -0.00427372;
        public const double BWSCALE = 0.000000252906277 / 1000000.0;

        private static void Trans7Params(double X, double Y, double Z, ref double Xgeo, ref double Ygeo, ref double Zgeo, TransDatum trans = TransDatum.VN2000ToWGSLatlong)
        {
            double k = trans == TransDatum.VN2000ToWGSLatlong ? 1 : -1;
            Xgeo = k * DELTA_X + ((1.0 + k * BWSCALE) * (X + Y * (k * ROT_Z / 3600.0).ToRadian() - Z * (k * ROT_Y / 3600.0).ToRadian()));
            Ygeo = k * DELTA_Y + ((1.0 + k * BWSCALE) * (-X * (k * ROT_Z / 3600.0).ToRadian() + Y + Z * (k * ROT_X / 3600.0).ToRadian()));
            Zgeo = k * DELTA_Z + ((1.0 + k * BWSCALE) * (X * (k * ROT_Y / 3600.0).ToRadian() - Y * (k * ROT_X / 3600.0).ToRadian() + Z));
        }

        public static double ToDegree(this double radian)
            => radian * 180.0 / Math.PI;

        public static double ToRadian(this double degree)
            => degree * Math.PI / 180.0;

        public static double ToDouble(this string obj, double def = 0.0)
        {
            double retval = def;
            try
            {
                double.TryParse(obj.ToString(), out retval);
            }
            catch { }
            return retval;
        }
    }
}