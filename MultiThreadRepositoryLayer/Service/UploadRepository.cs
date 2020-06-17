//
// Author: Vinayak Ushakola
// Date: 12/06/2020
//

using Microsoft.Extensions.Configuration;
using MultiThreadCommonLayer.RequestModels;
using MultiThreadCommonLayer.ResponseModels;
using MultiThreadRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MultiThreadRepositoryLayer.Service
{
    public class UploadRepository : IUploadRepository
    {
        private readonly IConfiguration _configuration;

        private readonly string sqlConnectionString;

        public UploadRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnectionString = _configuration.GetConnectionString("MultiThreadDBConnection");

        }

        /// <summary>
        /// It Fetches List of Images from the Database
        /// </summary>
        /// <returns>If Data Found return Response Data else null or Exception</returns>
        public List<ImageResponse> ListOfImages()
        {
            try
            {
                List<ImageResponse> responseList = null;
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    responseList = new List<ImageResponse>();
                    using(SqlCommand cmd = new SqlCommand("spGetAllImages", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            ImageResponse response = new ImageResponse
                            {
                                ImageID = Convert.ToInt32(dataReader["ID"]),
                                ImagePath = dataReader["ImagePath"].ToString(),
                                CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]),
                                ModifiedDate = Convert.ToDateTime(dataReader["ModifiedDate"])
                            };
                            responseList.Add(response);
                        }
                    }
                }
                return responseList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// It Adds Data into the Database
        /// </summary>
        /// <param name="image">Image Link</param>
        /// <returns>If Data Inserted Successfully return true else false or Exception</returns>
        public bool AddImage(ImageRequest image)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spAddImage", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ImagePath", image.ImagePath);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        conn.Open();
                        var count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// It Fetches Data from the Database
        /// </summary>
        /// <returns>If Data Found return Response Data else null or Exception</returns>
        public List<VideoResponse> ListOfVideos()
        {
            try
            {
                List<VideoResponse> responseList = null;
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    responseList = new List<VideoResponse>();
                    using (SqlCommand cmd = new SqlCommand("spGetAllVideos", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            VideoResponse response = new VideoResponse
                            {
                                VideoID = Convert.ToInt32(dataReader["ID"]),
                                VideoPath = dataReader["VideoPath"].ToString(),
                                CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]),
                                ModifiedDate = Convert.ToDateTime(dataReader["ModifiedDate"])
                            };
                            responseList.Add(response);
                        }
                    }
                }
                return responseList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// It Adds Data into the Database
        /// </summary>
        /// <param name="video"></param>
        /// <returns>If Data Added Successfully return true else false or Exception</returns>
        public bool AddVideo(VideoRequest video)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spAddVideo", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VideoPath", video.VideoPath);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        conn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
