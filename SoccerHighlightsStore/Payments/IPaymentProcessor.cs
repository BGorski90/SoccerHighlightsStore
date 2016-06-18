using SoccerHighlightsStore.Storefront.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHighlightsStore.Storefront.Payments
{
    public interface IPaymentProcessor
    {
        bool AuthorizePayment(PaymentViewModel paymentData);
    }
}
