using AutoMapper;
using CodeTest.Controllers.API;
using CodeTest.DTO;
using CodeTest.IServices;
using CodeTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace TestCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly ILogger<POSApiController> _logger;

        private readonly IMapper _mapper;

        private readonly IPointService _pointService;

        private IDistributedCache _distributedCache;

        public PointController(ILogger<POSApiController> logger, IMapper mapper, IPointService pointService, IDistributedCache distributedCache)
        {
            _logger = logger;
            _mapper = mapper;
            _pointService = pointService;
            _distributedCache = distributedCache;
        }

        [Route("CalculatePoint")]
        [HttpPost]
        public async Task<ActionResult> CalculatePoint([FromBody] PurchaseDTO purchaseDTO)
        {
            Purchase purchase = _mapper.Map<Purchase>(purchaseDTO);
            var memberDetail = await _pointService.CalculatePoint(purchase);
            _distributedCache.SetString("Member" + memberDetail.Id, memberDetail.TotalPoint + "");
            return Ok(memberDetail);
        }



    }
}
