//
// Author: Vinayak Ushakola
// Date: 12/06/2020
//

using System;

namespace MultiThreadCommonLayer.ResponseModels
{
    public class VideoResponse
    {
        public int VideoID { get; set; }

        public string VideoPath { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
