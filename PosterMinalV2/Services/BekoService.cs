using PosterMinalV2.Models;

namespace PosterMinalV2.Services
{
    public class BekoService
    {
        public ApiResponse TestConnection()
        {
            // TODO: Beko cihaz bağlantı testi entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Beko test başarılı (stub)." };
        }

        public ApiResponse Pair(PairRequest request)
        {
            // TODO: Beko cihaz eşleştirme entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Beko pair başarılı (stub)." };
        }

        public ApiResponse CashierLogin(CashierLoginRequest request)
        {
            // TODO: Beko kasiyer giriş entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Beko cashier login başarılı (stub)." };
        }

        public ApiResponse CashierLogout()
        {
            // TODO: Beko kasiyer çıkış entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Beko cashier logout başarılı (stub)." };
        }

        public ApiResponse PrintReceipt(ReceiptRequest request)
        {
            // TODO: Beko fiş basım entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Beko receipt başarılı (stub)." };
        }

        public ApiResponse PrintXReport()
        {
            // TODO: Beko X raporu entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Beko X report başarılı (stub)." };
        }

        public ApiResponse PrintZReport()
        {
            // TODO: Beko Z raporu entegrasyonu eklenecek.
            return new ApiResponse { Success = true, Message = "Beko Z report başarılı (stub)." };
        }
    }
}
