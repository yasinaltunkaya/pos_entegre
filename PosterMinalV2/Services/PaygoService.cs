using PosterMinalV2.Models;

namespace PosterMinalV2.Services
{
    public class PaygoService
    {
        public ApiResponse TestConnection()
        {
            // TODO: Paygo cihaz bağlantı testi entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Paygo test başarılı (stub)." };
        }

        public ApiResponse Pair(PairRequest request)
        {
            // TODO: Paygo cihaz eşleştirme entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Paygo pair başarılı (stub)." };
        }

        public ApiResponse CashierLogin(CashierLoginRequest request)
        {
            // TODO: Paygo kasiyer giriş entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Paygo cashier login başarılı (stub)." };
        }

        public ApiResponse CashierLogout()
        {
            // TODO: Paygo kasiyer çıkış entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Paygo cashier logout başarılı (stub)." };
        }

        public ApiResponse PrintReceipt(ReceiptRequest request)
        {
            // TODO: Paygo fiş basım entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Paygo receipt başarılı (stub)." };
        }

        public ApiResponse PrintXReport()
        {
            // TODO: Paygo X raporu entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Paygo X report başarılı (stub)." };
        }

        public ApiResponse PrintZReport()
        {
            // TODO: Paygo Z raporu entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Paygo Z report başarılı (stub)." };
        }
    }
}
