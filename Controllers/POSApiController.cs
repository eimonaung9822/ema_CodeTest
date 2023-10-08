using AutoMapper;
using CodeTest.DTO;
using CodeTest.IServices;
using CodeTest.Models;
using CodeTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;

namespace CodeTest.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class POSApiController : ControllerBase
    {
        private readonly ILogger<POSApiController> _logger;

        private readonly IMapper _mapper;

        private readonly IPOSService _posService;

        private IDistributedCache _distributedCache;

        public POSApiController(ILogger<POSApiController> logger, IMapper mapper, IPOSService posService, IDistributedCache distributedCache)
        {
            _logger = logger;
            _mapper = mapper;
            _posService = posService;
            _distributedCache = distributedCache;
        }

        [Route("GetPurchases")]
        [HttpGet]
        public async Task<IEnumerable<PurchaseDTO>> Get()
        {

            List<Purchase> PurchaseList = await _posService.GetAllPurchases();
            List<PurchaseDTO> PurchaseDtoList = new List<PurchaseDTO>();
            PurchaseList.ForEach(x => PurchaseDtoList.Add(_mapper.Map<PurchaseDTO>(x)));

            return PurchaseDtoList;
        }

        [Route("CreatePurchase")]
        [HttpPost]
        public async Task<ActionResult> CreatePurchase([FromBody] PurchaseDTO purchaseDTO)
        {
            Purchase purchase = _mapper.Map<Purchase>(purchaseDTO);

            return Ok(await _posService.SavePurchase(purchase));
        }        

    }
}
