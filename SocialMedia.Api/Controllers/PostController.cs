using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IPostRepository _IpostRepository;
        private readonly IMapper _maper;
        public PostController(IPostRepository postRepository, IMapper Imaper)
        {
            _IpostRepository = postRepository;
            _maper = Imaper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
           
            var post =await _IpostRepository.GetPosts();
            //forma de iteracion
            var postDto = _maper.Map <IEnumerable<PostDTO>>(post);
            return Ok(postDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _IpostRepository.GetPosts(id);
            var postDto = _maper.Map<PostDTO>(post);
            return Ok(postDto);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostDTO postDto )
        {
          
            var post = _maper.Map<Post>(postDto);
             await _IpostRepository.crear(post);
            return Ok("ok");
        }
    }
}
