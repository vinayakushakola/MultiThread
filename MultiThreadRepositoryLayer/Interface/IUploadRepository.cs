//
// Author: Vinayak Ushakola
// Date: 12/06/2020
//

using MultiThreadCommonLayer.RequestModels;
using MultiThreadCommonLayer.ResponseModels;
using System.Collections.Generic;

namespace MultiThreadRepositoryLayer.Interface
{
    public interface IUploadRepository
    {
        List<ImageResponse> ListOfImages();

        bool AddImage(ImageRequest image);

        List<VideoResponse> ListOfVideos();

        bool AddVideo(VideoRequest video);
    }
}
