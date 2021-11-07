﻿namespace DeemZ.Services.PromoCodeServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DeemZ.Data.Models;
    using DeemZ.Models.FormModels.PromoCode;

    public interface IPromoCodeService
    {
        PromoCode GetPromoCode(string promoCode);
        void MarkPromoCodeAsUsed(string pcid);
        bool ValidatePromoCode(string uid, string promoCode);
        int GetPromoCodesCount();
        IEnumerable<T> GetPromoCodes<T>(string promoCode);
        IEnumerable<T> GetPromoCodes<T>(int page = 1 , int quantity = 20);
        Task<string> GeneratePromoCodeText();
        bool IfExists(string promoCode);
        void AddPromoCode(AddPromoCodeFormModel promoCode);
    }
}
