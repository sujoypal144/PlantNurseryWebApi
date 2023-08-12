using Microsoft.AspNetCore.Mvc;
using PlantNurseryWebApi.Data;
using PlantNurseryWebApi;

namespace PlantNurseryWebApi.Controllers
{
    public class PlantsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly PlantsData _plantData;
        private readonly PurchasesData _purchaseData;

        public PlantsController(PlantsData plantData, PurchasesData purchaseData)
        {
            _plantData = plantData;
            _purchaseData = purchaseData;
        }

        // GET: api/plants/GetAvailablePlants
        [HttpGet("GetAvailablePlants")]
        public IActionResult GetAvailablePlants()
        {
            var availablePlants = _plantData.GetAvailablePlants();
            if (availablePlants == null || !availablePlants.Any())
                return NotFound();

            return Ok(availablePlants);
        }


        // GET: api/plants/GetPlantById/{id}
        [HttpGet("GetPlantById/{id}")]
        public ActionResult<Plants> GetPlantByIdOnly(int id)
        {
            var plant = _plantData.GetPlantById(id);
            if (plant == null)
                return NotFound();

            return Ok(new Plants { Id = plant.Id, Name = plant.Name, Price = plant.Price });
        }

        // POST: api/plants/AddNewPlant
        [HttpPost("AddNewPlant")]
        public IActionResult AddPlant([FromBody] Plants plant)
        {
            if (plant == null)
            {
                return BadRequest("Plant data is missing.");
            }

            // Perform additional input validation
            if (string.IsNullOrWhiteSpace(plant.Name))
            {
                return BadRequest("Plant name must be provided.");
            }

            try
            {
                _plantData.AddPlant(plant);
                return Ok("Plant added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add plant: {ex.Message}");
            }
        }

        // PUT: api/plants/MarkPlantAsSold/{id}
        [HttpPut("MarkPlantAsSold/{id}")]
        public IActionResult MarkPlantAsSold(int id, int customerId)
        {
            var plant = _plantData.GetPlantById(id);
            if (plant == null)
            {
                return NotFound();
            }

            if (plant.SaleStatus == SaleStatus.SOLD)
            {
                return BadRequest("Plant is already sold.");
            }

            try
            {
                _plantData.MarkPlantAsSold(id);
                var purchase = new Purchases
                {
                    PlantId = plant.Id,
                    CustomerId = customerId,
                    IsActive = true,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                };
                _purchaseData.AddPurchase(purchase);
                return Ok("Plant marked as sold successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to mark plant as sold: {ex.Message}");
            }
        }


        // PUT: api/plants/EditPlant/{id}
        [HttpPut("EditPlant/{id}")]
        public IActionResult UpdatePlant(int id, [FromBody] Plants updatedPlant)
        {
            if (updatedPlant == null || updatedPlant.Id != id)
            {
                return BadRequest("Invalid input data or mismatched IDs.");
            }

            var existingPlant = _plantData.GetPlantById(id);
            if (existingPlant == null)
            {
                return NotFound();
            }

            // Update the existing plant details
            existingPlant.Name = updatedPlant.Name;
            existingPlant.Price = updatedPlant.Price;

            // Save the changes to the database
            try
            {
                _plantData.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the plant.");
            }

            return Ok();
        }
    }
}

