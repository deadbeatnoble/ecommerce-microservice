using Discount.gRPC.Protos;

namespace Basket.API.gRPCServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountService)
        {
            _discountService = discountService;
        }

        public async Task<CouponRequest> GetDiscount(string productId)
        {
            var getDiscountData = new GetDiscountRequest() { ProductId = productId };
            return await _discountService.GetDiscountAsync(getDiscountData);
        }
    }
}
