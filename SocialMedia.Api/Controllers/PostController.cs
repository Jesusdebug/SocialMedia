using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public PostController(IPostRepository postRepository)
        {
            _IpostRepository = postRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
            var post =await _IpostRepository.GetPosts();
            return Ok(post);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _IpostRepository.GetPosts(id);
            return Ok(post);
        }
    }
}
