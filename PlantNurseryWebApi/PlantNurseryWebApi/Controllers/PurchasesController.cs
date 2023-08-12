using Microsoft.AspNetCore.Mvc;
using PlantNurseryWebApi.Data;
using PlantNurseryWebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlantNurseryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly PurchasesData _purchaseData;

        public PurchasesController(PurchasesData purchaseData)
        {
            _purchaseData = purchaseData;
        }


        // GET: api/purchases/GetPurchasesDetails
        [HttpGet("GetPurchasesDetails")]
        public ActionResult<IEnumerable<Purchases>> GetPurchasedPlantsWithCustomerDetails()
        {
            var purchasedPlants = _purchaseData.GetPurchasedPlantsWithCustomerDetails();
            return Ok(purchasedPlants);
        }

    }
}
