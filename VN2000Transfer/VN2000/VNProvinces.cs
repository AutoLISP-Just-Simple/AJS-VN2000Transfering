using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VN2000Transfer
{
    public static class VNProvinces
    {
        public static string FindProvinceByLong(double lon)
            => Datas.OrderBy(x => Math.Abs(x.Split(spls, StringSplitOptions.RemoveEmptyEntries).LastOrDefault().ToDouble() - lon))
                    .FirstOrDefault();

        public static double GetNaturalLong(string VNProvs)
        {
            if (string.IsNullOrEmpty(VNProvs) || !VNProvinces.Datas.Contains(VNProvs)) VNProvs = "auto";
            var mcs = (VNProvs == "" ? Datas.FirstOrDefault() : VNProvs).Split(spls, StringSplitOptions.RemoveEmptyEntries);
            if (mcs.Length > 1 && mcs.LastOrDefault().ToDouble() > 10) return mcs.LastOrDefault().ToDouble();
            return 107.5;
        }

        private static string[] spls = new List<string>() { "\t", " ", "," }.ToArray();


        public static List<string> Datas = new List<string>()
        {
            "1. An Giang  104.75",
            "2. Bà Rịa - Vũng Tàu 107.75",
            "3. Bắc Giang 107",
            "4. Bắc Kạn 106.5",
            "5. Bạc Liêu  105",
            "6. Bắc Ninh 105.5",
            "7. Bến Tre 105.75",
            "8. Bình Định 108.25",
            "9. Bình Dương  105.75",
            "10. Bình Phước 106.25",
            "11. Bình Thuận 108.5",
            "12. Cà Mau  104.5",
            "13. Cần Thơ 105",
            "14. Cao Bằng 105.75",
            "15. Đà Nẵng 107.75",
            "16. Đắc Lắc 108.5",
            "17. Đắc Nông 108.5",
            "18. Điện Biên 103",
            "19. Đồng Nai 107.75",
            "20. Đồng Tháp 105",
            "21. Gia Lai 108.5",
            "22. Hà Giang  105.5",
            "23. Hà Nam 105",
            "24. Hà Nội  105",
            "25. Hà Tĩnh 105.5",
            "26. Hải Dương  105.5",
            "27. Hải Phòng 105.75",
            "28. Hậu Giang 105",
            "29. Hòa Bình 106",
            "30. Hưng Yên 105.5",
            "31. Khánh Hòa 108.25",
            "32. Kiên Giang  104.5",
            "33. Kon Tum 107.5",
            "34. Lai Châu 103",
            "35. Lâm Đồng 107.75",
            "36. Lạng Sơn 107.25",
            "37. Lào Cai  104.75",
            "38. Long An 105.75",
            "39. Nam Định 105.5",
            "40. Nghệ An  104.75",
            "41. Ninh Bình 105",
            "42. Ninh Thuận 108.25",
            "43. Phú Thọ 104.75",
            "44. Phú Yên 108.5",
            "45. Quảng Bình 106",
            "46. Quảng Nam 107.75",
            "47. Quảng Ngãi 108",
            "48. Quảng Ninh 107.75",
            "49. Quảng Trị 106.25",
            "50. Sóc Trăng  105.5",
            "51. Sơn La  104",
            "52. Tây Ninh 105.5",
            "53. Thái Bình 105.5",
            "54. Thái Nguyên 106.5",
            "55. Thanh Hóa 105",
            "56. TP Hồ Chí Minh 105.75",
            "57. Thừa Thiên Huế  107",
            "58. Tiền Giang 105.75",
            "59. Trà Vinh 105.5",
            "60. Tuyên Quang  106",
            "61. Vĩnh Long 105.5",
            "62. Vĩnh Phúc  105",
            "63. Yên Bái  104.75"
        };
    }
}
