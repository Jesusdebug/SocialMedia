using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Response;
using SocialMedia.Core.CustormerEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaz;
using SocialMedia.Infraestructure.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Produces("Application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _maper;
        private readonly IUriService _uriService;
        public PostController(IPostService IpostService, IMapper Imaper, IUriService uriService)
        {
            _postService = IpostService;
            _maper = Imaper;
            _uriService = uriService;
        }
        /// <summary>
        /// Retrieve all post
        /// </summary>
        /// <param name="postQueryFilter"> Filter to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPost))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type=typeof(ApiResponse<IEnumerable<PostDTO>>))]
        public  IActionResult GetPost([FromQuery]PostQueryFilter postQueryFilter)
        {

            var post =  _postService.GetAllPost( postQueryFilter);
            //forma de iteracion
            var postDto = _maper.Map<IEnumerable<PostDTO>>(post);
            var metadata = new Metadata
            {
                TotalCount = post.TotalCount,
                PageSize = post.PageSize,
                CurrentPage = post.CurrentPage,
                TotalPages = post.TotalPages,
                HasNextiusPages = post.HasNextiusPages,
                HasPreviusPages = post.HasPreviusPages,
                NextiusPagesUrl = _uriService.GetPostPaginationUri(postQueryFilter, Url.RouteUrl(nameof(GetPost))).ToString()
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            var response = new ApiResponse<IEnumerable<PostDTO>>(postDto) { 
            Metadata=metadata,
            };
            return Ok(response);
        }
        /// <summary>
        /// Obtenemos el post por el id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPostById(id);
            var postDto = _maper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDto);

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostDTO postDto)
        {

            var post = _maper.Map<Post>(postDto);
            await _postService.CreatePost(post);
            postDto = _maper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDto);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDTO postDto)
        {
            var post = _maper.Map<Post>(postDto);
            post.Id = id;
         var result=   await _postService.UpdatePost(post);
            var esponse = new ApiResponse<bool>(result);

            return Ok(esponse);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delet(int id)
        {
            var result=  await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
