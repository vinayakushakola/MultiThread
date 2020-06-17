//
// Author: Vinayak Ushakola
// Date: 12/06/2020
//

using System;

namespace MultiThreadCommonLayer.ResponseModels
{
    public class ImageResponse
    {
        public int ImageID { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }
    }
}
