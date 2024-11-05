using AutoMapper;
using KS.Application.Articles;
using KS.Domain.Articles;
using Microsoft.AspNetCore.Mvc;

namespace KS.API.Controllers
{
    [ApiController]
    [Route("api/article")]
    public class ArticleController : ControllerBase
    {
        private IArticleRepository _articleRepository;
        private readonly IMapper _mapper;


        public ArticleController(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        [HttpGet("getAllList")]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetAllList()
        {
            var articleDTOs = new List<ArticleDTO>();
            var articles = await _articleRepository.GetAll();
            if (articles.Count != 0) articleDTOs = _mapper.Map<List<ArticleDTO>>(articles);
            
            return Ok(articleDTOs);
        }
    }
}
