using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerTasks.Common
{
    public class Utilities
    {
        public string ConvertEn(string strVietnamese, string searchString)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            //tìm chính xác nếu searchString là tiếng việt
            if (!CheckIsVietNamese(searchString))
            {
                while ((index = strVietnamese.IndexOfAny(FindText.ToCharArray())) != -1)
                {
                    int index2 = FindText.IndexOf(strVietnamese[index]);
                    strVietnamese = strVietnamese.Replace(strVietnamese[index], ReplText[index2]);
                }
            }
            return strVietnamese;           
        }

        public bool CheckIsVietNamese(string searchString)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            while (searchString.IndexOfAny(FindText.ToCharArray()) != -1)
            {
                return true;
            }
            return false;
        }
    }
}