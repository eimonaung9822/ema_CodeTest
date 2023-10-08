using AutoMapper;
using AutoMapper.Execution;
using CodeTest.Controllers.API;
using CodeTest.DTO;
using CodeTest.IServices;
using CodeTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using TestCode.DataAccess;
using TestCode.DTO;
using TestCode.IServices;
using TestCode.Models;

namespace TestCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileApiController : ControllerBase
    {
        private readonly IPOSService _posService;

        private readonly IMobileService _mobileService;

        private IDistributedCache _distributedCache;

        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        public MobileApiController(IPOSService posService, IDistributedCache distributedCache, IMapper mapper, ITokenService tokenService, IMobileService mobileService)
        {
            _posService = posService;
            _distributedCache = distributedCache;
            _tokenService = tokenService;
            _mobileService = mobileService;
            _mapper = mapper;
        }

        [Route("Register")]
        [HttpGet]
        public async Task<ActionResult> RegisterWithOTP([FromQuery] AuthenticateOTPRequest authenticateOTPRequest)
        {
            if (authenticateOTPRequest.MobileNo == "09123456789" && authenticateOTPRequest.Otp == "1234")
            {
                return new JsonResult(new { status = "Registration Successful!", userName = authenticateOTPRequest.MobileNo, token = _tokenService.CreateToken(authenticateOTPRequest.MobileNo, out DateTime ExpireDate), expireDate = ExpireDate, refreshToken = _tokenService.RefreshToken(authenticateOTPRequest.MobileNo, Guid.NewGuid().ToString(), out DateTime RefreshExpireDate), refreshExpiryDatetime = RefreshExpireDate });
                
            }
            else
            {
                return BadRequest("Registration Fail!");
            }
        }

        [Route("CreateMember")]
        [HttpPost]
        public async Task<ActionResult> CreateMember([FromBody] MemberDetailDTO memberDetailDTO)
        {
            MemberDetail memberDetail = _mapper.Map<MemberDetail>(memberDetailDTO);

            return Ok(await _posService.SaveMember(memberDetail));
        }

        [Route("GetExchangeCoupons")]
        [HttpGet]
        public async Task<ActionResult> GetExchangeCouponsList([FromQuery] int memberId)
        {
            return Ok(await _mobileService.GetExchangedCouponsList(memberId));
        }

        [Route("GetUsedCoupons")]
        [HttpGet]
        public async Task<ActionResult> GetUsedCouponsList([FromQuery] int memberId)
        {
            return Ok(await _mobileService.GetUsedCouponsList(memberId));
        }

        [Route("GetMemberQR")]
        [HttpGet]
        public async Task<IActionResult> GenerateQRCode(int memberId)
        {
            var streamData = await _mobileService.GenerateQRCode(memberId);
            return File(streamData, "image/jpeg");
        }

        [Route("GetPurchasesbyMemberId")]
        [HttpGet]
        public async Task<IEnumerable<Purchase>> Get([FromQuery] int memberId)
        {
            List<Purchase> PurchaseList = await _posService.GetAllPurchases();


            return PurchaseList.Where(x => x.MemberId == memberId).ToList();
        }

        [Route("GetTotalPointbyMemberId")]
        [HttpGet]
        public async Task<ActionResult> GetTotalPointByMemberId([FromQuery] int memberId)
        {
            return Ok("Total Point: " + _distributedCache.GetString("Member" + memberId));
        }



        [Route("GetExchangePoint")]
        [HttpPost]
        public async Task<ActionResult> ExchangePoint([FromQuery] int memberId, int couponId, int requirePoint)
        {
            string result = await _mobileService.ExchangePoint(memberId, couponId, requirePoint);
            if (result != null)
            {
                return BadRequest(result);
            }
            return Ok("Exchange Successful!");
        }
    }
}
