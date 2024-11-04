using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KS.Application.Offers;
using KS.Application.Offers.Create;
using KS.Application.Offers.Update;
using KS.Application.Response;
using KS.Domain.Offers;
using Microsoft.AspNetCore.Mvc;

namespace KS.API.Controllers
{
    [ApiController]
    [Route("api/offer")]
    public class OfferController : ControllerBase
    {
        private IOfferRepository _offerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<OfferCreateDTO> _createValidator;
        private readonly IValidator<OfferUpdateDTO> _updateValidator;

        public OfferController(IOfferRepository offerRepository, IMapper mapper, IValidator<OfferUpdateDTO> updateValidator, IValidator<OfferCreateDTO> createValidator)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }

        [HttpGet("getAllList")]
        public async Task<ActionResult<PagedResultDTO<OfferListDTO>>> GetAllList(int page = 1, int pageSize = 10)
        {
            if (page < 1) return BadRequest(new { error = "page not greter than 0!" });

            var offerDTOs = new List<OfferListDTO>();
            var result = await _offerRepository.GetAll(page, pageSize);
            if (result.Items.Count != 0) offerDTOs = _mapper.Map<List<OfferListDTO>>(result.Items);

            return Ok(new PagedResultDTO<OfferListDTO>
            {
                Data = offerDTOs,
                TotalCount = result.TotalCount
            });
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<OfferDetailsDTO>> GetById(long id)
        {
            if (id < 1) return BadRequest(new { error = "Id not greter than 0!" });

            var offer = await _offerRepository.GetById(id);
            if (offer == null) return NotFound(null);

            var offerDTO = _mapper.Map<OfferDetailsDTO>(offer);
            return Ok(offerDTO);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] OfferCreateDTO offerDTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(offerDTO);

            if (!validationResult.IsValid) { 
            
                var errors = new List<string>();
                validationResult.Errors.ForEach(e => errors.Add(e.ErrorMessage));

                return BadRequest(new { errors });
            }
            
            var offer = _mapper.Map<Offer>(offerDTO);
            await _offerRepository.Create(offer);

            return CreatedAtAction(nameof(GetById), new { id = offer.Id }, new { offer.OfferNumber });
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] OfferUpdateDTO offerDTO)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(offerDTO);

            if (!validationResult.IsValid) {
                var errors = new List<string>();
                validationResult.Errors.ForEach(e => errors.Add(e.ErrorMessage));

                return BadRequest(new { errors });
            }

            var offer = _mapper.Map<Offer>(offerDTO);
            var result = await _offerRepository.Update(offer);
            if (!result) return NoContent();

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            if (id < 1) return BadRequest(new { error = "Id not greter than 0!" });

            var result = await _offerRepository.Delete(id);
            if (!result) return NoContent();

            return Ok("Offer deleted");
        }
    }
}
