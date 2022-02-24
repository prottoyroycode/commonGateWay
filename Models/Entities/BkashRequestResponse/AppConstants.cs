namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class AppConstants
    {
        //ctrl +k ,ctrl+d to format the code 
        //public const string AppKey = "5tunt4masn6pv2hnvte1sb5n3j";
        //public const string AppSecret = "1vggbqd4hqk9g96o9rrrp2jftvek578v7d2bnerim12a87dbrrka";
        //public const string UserName = "sandboxTestUser";
        //public const string Password = "hWD@8vtzw0";
        //

        public const string tokenize_app_key = "4f6o0cjiki2rfm34kfdadl1eqq";
        public const string tokenize_app_secret = "2is7hdktrekvrbljjh44ll3d9l1dtjo4pasmjvs5vl5qr3fug4b";
        public const string tokenize_username = "sandboxTokenizedUser02";
        public const string tokenize_pass = "sandboxTokenizedUser02@12345";
        public const string Mode = "0000";
        public const string PayerReference = "01877722345";

        public const string app_keyForAgreementCancel = "4f6o0cjiki2rfm34kfdadl1eqq";
        public const string app_secretForTokenCancel = "2is7hdktrekvrbljjh44ll3d9l1dtjo4pasmjvs5vl5qr3fug4b";


        public const string agreementID = "TokenizedMerchant02RIEQQQ41639231028012";

        public const string TokenUrl = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/token/grant";
        public const string CancelAgreementTokenURL = " https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/token/grant";

        public const string CheckAgreementStatusURL = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/agreement/status";
        public const string GrantTokenForPaymentWithAgreement = " https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/token/grant";

        public const string CreateAgreementUrl = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/create";
        public const string CreatePaymentWithAgreementIdURL = " https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/create";


        public const string ExexAgreementUrl = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/execute";
        public const string ExecutePaymentURL = " https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/execute";
        public const string BkashCancelAgreementURL = " https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/agreement/cancel";

        //live

         public const string PaymentCallback = "http://partnersIntregation.techapi24.com/api/v1/ApiTest/transaction";
         public const string CreatePaymentCallBackURL = "http://partnersIntregation.techapi24.com/api/v1/ApiTest/paymentCallback";
        //local

     //  public const string PaymentCallback = "https://localhost:44306/api/v1/ApiTest/transaction";
      // public const string CreatePaymentCallBackURL = "https://localhost:44306/api/v1/ApiTest/paymentCallback";
        //local
        //  public const string PaymentCallback = "https://api.evouchers.store/api/BkashCallBack/transection";
    }
}
