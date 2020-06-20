//
// Author: Vinayak Ushakola
// Date: 12/06/2020
//

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using MultiThread.Controllers;
using MultiThreadBusinessLayer.Interface;
using MultiThreadBusinessLayer.Service;
using MultiThreadCommonLayer.RequestModels;
using MultiThreadRepositoryLayer.Interface;
using MultiThreadRepositoryLayer.Service;
using System;
using System.IO;
using Xunit;

namespace MultiThreadUnitTesting
{
    public class UploadTesting
    {
        private readonly IUploadRepository _uploadRepository;
        
        private readonly IUploadBusiness _uploadBusiness;

        private readonly IConfiguration _configuration;

        private readonly UploadController controller;

        public UploadTesting()
        {
            IConfigurationBuilder configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("appsettings.json");
            _configuration = configuration.Build();
            _uploadRepository = new UploadRepository(_configuration);
            _uploadBusiness = new UploadBusiness(_uploadRepository);
            controller = new UploadController(_uploadBusiness, _configuration);
        }

        /// <summary>
        /// If Data Found return Ok else Not Found or Bad Request
        /// </summary>
        [Fact]
        public void Get_List_Of_Images_Path_Return_Ok_Result()
        {
            var data = controller.GetListOfImages();
            try
            {
                if (_uploadBusiness.ListOfImages() == null)
                {
                    Assert.IsType<NotFoundObjectResult>(data);
                }
                Assert.IsType<OkObjectResult>(data);
            }
            catch(Exception ex)
            {
                Assert.IsType<BadRequestObjectResult>(ex.Message);
            }
        }

        /// <summary>
        /// If Data Found return Ok else Not Found or Bad Request
        /// </summary>
        [Fact]
        public void Get_List_Of_Videos_Path_Return_Ok_Result()
        {
            var data = controller.GetListOfVideos();
            try
            {
                if (_uploadBusiness.ListOfVideos() == null)
                {
                    Assert.IsType<NotFoundObjectResult>(data);
                }
                Assert.IsType<OkObjectResult>(data);
            }
            catch (Exception ex)
            {
                Assert.IsType<BadRequestObjectResult>(ex.Message);
            }
        }
    }
}
