//
// Author: Vinayak Ushakola
// Date: 12/06/2020
//

using MultiThreadBusinessLayer.Interface;
using MultiThreadCommonLayer.RequestModels;
using MultiThreadCommonLayer.ResponseModels;
using MultiThreadRepositoryLayer.Interface;
using System.Collections.Generic;

namespace MultiThreadBusinessLayer.Service
{
    public class UploadBusiness : IUploadBusiness
    {
        private readonly IUploadRepository _uploadRepository;

        public UploadBusiness(IUploadRepository uploadRepository)
        {
            _uploadRepository = uploadRepository;
        }

        public List<ImageResponse> ListOfImages()
        {
            var response = _uploadRepository.ListOfImages();
            return response;
        }

        public bool AddImage(ImageRequest image)
        {
            var response = _uploadRepository.AddImage(image);
            return response;
        }

        public List<VideoResponse> ListOfVideos()
        {
            var response = _uploadRepository.ListOfVideos();
            return response;
        }

        public bool AddVideo(VideoRequest video)
        {
            var response = _uploadRepository.AddVideo(video);
            return response;
        }
    }
}
