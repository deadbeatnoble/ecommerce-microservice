using Discount.gRPC.Models;

namespace Discount.gRPC.Interfaces.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCoupon(string productId);
        Task<bool> CreateCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string productId);
    }
}
