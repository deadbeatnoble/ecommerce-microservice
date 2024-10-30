using Dapper;
using Discount.gRPC.Interfaces.Repository;
using Discount.gRPC.Models;
using Npgsql;

namespace Discount.gRPC.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var affected = await connection.ExecuteAsync(
                "INSERT INTO Coupon (ProductId, ProductName, Description, Amount) VALUES (@ProductId, @ProductName, @Description, @Amount)",
                new { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (affected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCoupon(string productId)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var affected = connection.Execute(
                "DELETE FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });
            if (affected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Coupon> GetCoupon(string productId)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });
            if (coupon == null)
            {
                return new Coupon() { ProductName = "No Discount", Amount = 0};
            }
            return coupon;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var affected = connection.Execute(
                "UPDATE Coupon SET ProductId = @ProductId, ProductName = @ProductName, Description = @Description, Amount = @Amount",
                new { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount});
            if (affected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
