using CoreApiResponse;
using Discount.API.Interfaces.Repository;
using Discount.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CouponController : BaseController
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoupon(string productId)
        {
            try
            {
                var coupon = await _couponRepository.GetCoupon(productId);
                return CustomResult("Data load successful.", coupon);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                var result = await _couponRepository.CreateCoupon(coupon);
                if (result)
                {
                    return CustomResult("Data created successfully.", coupon);
                }
                return CustomResult("Data not saved.", coupon, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                var result = await _couponRepository.UpdateCoupon(coupon);
                if (result)
                {
                    return CustomResult("Data updated successfully.", coupon);
                }
                return CustomResult("Data not updated.", coupon, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCoupon(string productId)
        { 
            try
            {
                var result = await _couponRepository.DeleteCoupon(productId);
                if (result)
                {
                    return CustomResult("Data deleted successfully.");
                }
                return CustomResult("Data not deleted.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
