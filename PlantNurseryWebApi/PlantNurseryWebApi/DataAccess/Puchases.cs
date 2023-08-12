using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlantNurseryWebApi;

namespace PlantNurseryWebApi.Data
{
    public class PurchasesData
    {
        private readonly PlantNurseryDbContext _context;

        public PurchasesData(PlantNurseryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Purchases> GetPurchasedPlantsWithCustomerDetails()
        {
            return _context.Purchases;
        }

        public void AddPurchase(Purchases purchase)
        {
            if (purchase == null)
                throw new ArgumentNullException(nameof(purchase));

            _context.Purchases.Add(purchase);
            _context.SaveChanges();
        }
    }
}