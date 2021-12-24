using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _maper;
        public PostController(IPostService IpostService, IMapper Imaper)
        {
            _postService = IpostService;
            _maper = Imaper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPost()
        {

            var post = await _postService.GetAllPost();
            //forma de iteracion
            var postDto = _maper.Map<IEnumerable<PostDTO>>(post);
            var response = new ApiResponse<IEnumerable<PostDTO>>(postDto);

            return Ok(response);
        }
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
