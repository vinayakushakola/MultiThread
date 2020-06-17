//
// Author: Vinayak Ushakola
// Date: 12/06/2020
//

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MultiThreadBusinessLayer.Interface;
using MultiThreadCommonLayer.RequestModels;
using MultiThreadCommonLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThread.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadBusiness _uploadBusiness;

        private readonly IConfiguration _configuration;

        public UploadController(IUploadBusiness uploadBusiness, IConfiguration configuration)
        {
            _configuration = configuration;
            _uploadBusiness = uploadBusiness;
        }

        /// <summary>
        /// It Shows Uploaded Image Path
        /// </summary>
        /// <returns>If Data Found Return Ok else null or Bad Request</returns>
        [HttpGet]
        [Route("Images")]
        public IActionResult GetListOfImages()
        {
            try
            {
                bool success = false;
                string message;
                List<ImageResponse> data = null;
                Thread myThread = new Thread(() =>
                {
                    data = _uploadBusiness.ListOfImages();
                })
                { IsBackground = true };
                myThread.Start();
                myThread.Join();

                if (data != null)
                {
                    success = true;
                    message = "Images Data Fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Data Found";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Upload Image
        /// </summary>
        /// <param name="file">Select Image</param>
        /// <returns>If Data Found return Ok else null or Bad Request</returns>
        [HttpPost]
        [Route("Image")]
        public IActionResult AddImage(IFormFile file)
        {
            try
            {
                bool success = false;
                string message;
                ImageRequest image = null;

                Thread myThread = new Thread(() =>
                {
                    string imageLink = UploadImageToCloudinary(file);
                    image = new ImageRequest()
                    {
                        ImagePath = imageLink
                    };
                    success = _uploadBusiness.AddImage(image);
                })
                { IsBackground = true };
                myThread.Start();
                myThread.Join();

                if (success)
                {
                    message = "Image Uploaded Successfully";
                    return Ok(new { success, message, image.ImagePath });
                }
                else
                {
                    message = "Image Uploading failed";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It Shows Uploaded Video Path
        /// </summary>
        /// <returns>If Data Found Return Ok else null or Bad Request</returns>
        [HttpGet]
        [Route("Videos")]
        public IActionResult GetListOfVideos()
        {
            try
            {
                bool success = false;
                string message;
                List<VideoResponse> data = null;
                Thread myThread = new Thread(() =>
                {
                    data = _uploadBusiness.ListOfVideos();
                })
                { IsBackground = true };
                myThread.Start();
                myThread.Join();

                if (data != null)
                {
                    success = true;
                    message = "Videos Data Fethed Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Data Found";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Upload Video
        /// </summary>
        /// <param name="videoFile">Select Video</param>
        /// <returns>If Data Found Return Ok else null or Bad Request</returns>
        [HttpPost]
        [Route("Video")]
        public IActionResult AddVideo(IFormFile videoFile)
        {
            try
            {
                bool success = false;
                string message;
                VideoRequest video = null;

                Thread myThread = new Thread(() =>
                {
                    string videoLink = UploadVideoToCloudinary(videoFile);
                    video = new VideoRequest
                    {
                        VideoPath = videoLink
                    };
                    success = _uploadBusiness.AddVideo(video);
                })
                { IsBackground = true };
                myThread.Start();
                myThread.Join();

                if (success)
                {
                    message = "Video Uploaded Successfully";
                    return Ok(new { success, message, video.VideoPath });
                }
                else
                {
                    message = "Video Uploading failed";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Upload image into the Cloudinary
        /// </summary>
        /// <param name="image">Image Path</param>
        /// <returns>If Image Uploaded Successfully return Image Link else Exception</returns>
        private string UploadImageToCloudinary(IFormFile image)
        {
            try
            {
                var myAccount = new Account(_configuration["Cloudinary:CloudName"], _configuration["Cloudinary:ApiKey"], _configuration["Cloudinary:ApiSecret"]);

                Cloudinary _cloudinary = new Cloudinary(myAccount);

                var imageUpload = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };

                var uploadResult = _cloudinary.Upload(imageUpload);

                return uploadResult.SecureUri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// It is used to Upload Video into the Cloudinary
        /// </summary>
        /// <param name="video">Video Path</param>
        /// <returns>If Video Uploaded Successfully return Video Link else Exception</returns>
        private string UploadVideoToCloudinary(IFormFile video)
        {
            try
            {
                var myAccount = new Account(_configuration["Cloudinary:CloudName"], _configuration["Cloudinary:ApiKey"], _configuration["Cloudinary:ApiSecret"]);

                Cloudinary _cloudinary = new Cloudinary(myAccount);

                var videoUpload = new VideoUploadParams()
                {
                    File = new FileDescription(video.FileName, video.OpenReadStream()),
                };
                var uploadResult = _cloudinary.Upload(videoUpload);

                return uploadResult.SecureUri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}