using AutoMapper;
using KS.Application.DTOs.Offer;
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

        [HttpGet("GetAllList")]
        public async Task<ActionResult<IEnumerable<OfferListDTO>>> GetAllList()
        {
            var offerDTOs = new List<OfferListDTO>();
            var offers = await _offerRepository.GetAll();
            if (offers.Count != 0) offerDTOs = _mapper.Map<List<OfferListDTO>>(offers);

            return Ok(offerDTOs);
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<OfferDetailsDTO>> GetById(long id)
        {
            var offer = await _offerRepository.GetById(id);
            if (offer == null) return NotFound();

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
