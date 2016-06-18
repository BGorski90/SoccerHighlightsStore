using SoccerHighlightsStore.Storefront.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerHighlightsStore.Storefront.Payments
{
    public class MockPaymentProcessor: IPaymentProcessor
    {

        public bool AuthorizePayment(PaymentViewModel paymentData)
        {
            if (paymentData.CreditCardValidUntil < DateTime.Now)
                return false;
            return true;
        }
    }
}