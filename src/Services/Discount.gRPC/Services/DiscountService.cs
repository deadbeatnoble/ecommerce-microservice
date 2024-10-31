using AutoMapper;
using Discount.gRPC.Interfaces.Repository;
using Discount.gRPC.Models;
using Discount.gRPC.Protos;
using Grpc.Core;

namespace Discount.gRPC.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetCoupon(request.ProductId);
            
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductId={request.ProductId} is not found."));
            }
            _logger.LogInformation("Discount is retrieved for ProductName: {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.ProductId);

            //return new CouponRequest { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description };
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            var isSaved = await _couponRepository.CreateCoupon(coupon);
            if (isSaved)
            {
                _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName);
            }
            else
            {
                _logger.LogInformation("Discount is not created.");
            }
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool IsModified = await _couponRepository.UpdateCoupon(coupon);
            if (IsModified)
            {
                _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName);
            }
            else
            {
                _logger.LogInformation("Discount is not updated.");
            }
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var isDeleted = await _couponRepository.DeleteCoupon(request.ProductId);
            if (isDeleted)
            {
                _logger.LogInformation("Discount is successfully deleted. ProductId : {ProductId}", request.ProductId);
            }
            else
            {
                _logger.LogInformation("Discount is not deleted.");
            }
            return new DeleteDiscountResponse { Success = isDeleted };
        }
    }
}
