using AutoMapper;
using KS.Application.DTOs.Offer;
using KS.Application.DTOs.Response;
using KS.Domain.Offers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KS.API.Controllers
{
    [ApiController]
    [Route("api/offer")]
    public class OfferController : ControllerBase
    {
        private IOfferRepository _offerRepository;
        private readonly IMapper _mapper;

        public OfferController(IOfferRepository offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        [HttpGet("getAllList")]
        public async Task<ActionResult<PagedResult<OfferListDTO>>> GetAllList(int page = 1, int pageSize = 10)
        {
            var offerDTOs = new List<OfferListDTO>();
            var result = await _offerRepository.GetAll(page, pageSize);
            if (result.Items.Count != 0) offerDTOs = _mapper.Map<List<OfferListDTO>>(result.Items);

            return Ok(new PagedResult<OfferListDTO>
            {
                Data = offerDTOs,
                TotalCount = result.TotalCount
            });
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<OfferDetailsDTO>> GetById(long id)
        {
            var offer = await _offerRepository.GetById(id);
            if (offer == null) return NotFound(null);

            var offerDTO = _mapper.Map<OfferDetailsDTO>(offer);
            return Ok(offerDTO);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] OfferCreateDTO offerDTO)
        {
            var offer = _mapper.Map<Offer>(offerDTO);
            await _offerRepository.Create(offer);

            return CreatedAtAction(nameof(GetById), new { id = offer.Id }, new { offer.OfferNumber });
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] OfferUpdateDTO offerDTO)
        {
            var offer = _mapper.Map<Offer>(offerDTO);
            var result = await _offerRepository.Update(offer);
            if (!result) return NoContent();

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var result = await _offerRepository.Delete(id);
            if (!result) return NoContent();

            return Ok("Offer deleted");
        }
    }
}
