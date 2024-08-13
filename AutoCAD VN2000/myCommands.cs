// (C) Copyright 2024 by
//
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(AutoCAD_VN2000.MyCommands))]

namespace AutoCAD_VN2000
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class MyCommands
    {
        // Modal Command with localized name
        [CommandMethod("ToLatLong")]
        public void ChuyenDoiVN2000_sang_LatLong() // This method can have any name
        {
            // Put your command code here
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;

                var psr = ed.GetPoint("\nXác định điểm trên bản vẽ: ");
                if (psr.Status != PromptStatus.OK) return;

                foreach (var prov in VN2000Transfer.VNProvinces.Datas)
                    ed.WriteMessage("\n" + prov + " => " + VN2000Transfer.VNProvinces.GetNaturalLong(prov));

                var per = ed.GetDouble("\nChọn kinh tuyến tương ứng với Tỉnh/TP");
                if (per.Status != PromptStatus.OK || per.Value < 80) return;

                var point = psr.Value.TransformDatum(VN2000Transfer.TransDatum.VN2000ToWGSLatlong, per.Value);
                ed.WriteMessage("\nTọa độ Lat/Long chuyển đổi: " + point.X + "," + point.Y);
                ed.WriteMessage("\nwww.lisp.vn");
            }
        }

        [CommandMethod("ToVN2000")]
        public void ChuyenDoiveVN2000() // This method can have any name
        {
            // Put your command code here
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;

                var psr = ed.GetPoint("\nXác định tọa độ Lat,Long (VD: 11.3495405,108.8745916): ");
                if (psr.Status != PromptStatus.OK) return;

                foreach (var prov in VN2000Transfer.VNProvinces.Datas)
                    ed.WriteMessage("\n" + prov + " => " + VN2000Transfer.VNProvinces.GetNaturalLong(prov));

                var per = ed.GetDouble("\nChọn kinh tuyến tương ứng với Tỉnh/TP");
                if (per.Status != PromptStatus.OK || per.Value < 80) return;

                var point = psr.Value.TransformDatum(VN2000Transfer.TransDatum.WGS2LatlongToVN20000, per.Value);
                ed.WriteMessage("\nTọa độ Lat/Long chuyển đổi: " + point.X + "," + point.Y);
                ed.WriteMessage("\nwww.lisp.vn");
            }
        }
    }
}