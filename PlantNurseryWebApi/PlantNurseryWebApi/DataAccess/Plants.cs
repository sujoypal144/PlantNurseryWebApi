using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlantNurseryWebApi;

namespace PlantNurseryWebApi.Data
{
    public class PlantsData
    {
        private readonly PlantNurseryDbContext _context;

        public PlantsData(PlantNurseryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Plants> GetAvailablePlants()
        {
            return _context.Plants.Where(p => p.SaleStatus == 0).ToList();
        }

        public Plants GetPlantById(int id)
        {
            return _context.Plants.FirstOrDefault(p => p.Id == id);
        }

        public void MarkPlantAsSold(int plantId)
        {
            var plant = _context.Plants.Find(plantId);
            if (plant == null)
                throw new ArgumentException("Plant not found");

            if (plant.SaleStatus == SaleStatus.SOLD)
                throw new InvalidOperationException("Plant is already sold");

            plant.SaleStatus = SaleStatus.SOLD;
            plant.ModifiedOn = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void AddPlant(Plants plant)
        {
            if (plant == null)
                throw new ArgumentNullException(nameof(plant));

            _context.Plants.Add(plant);
            _context.SaveChanges();
        }

        public void EditPlant(Plants plant)
        {
            if (plant == null)
                throw new ArgumentNullException(nameof(plant));

            var existingPlant = _context.Plants.Find(plant.Id);
            if (existingPlant == null)
                throw new ArgumentException("Plant not found");

            existingPlant.Name = plant.Name;
            existingPlant.Price = plant.Price;
            existingPlant.ModifiedOn = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle any specific exception here, or re-throw with meaningful message.
                // Example: log the error, and re-throw a custom exception with a message for the client.
                throw new Exception("DB Update exception.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions that may occur during the save operation.
                // Example: log the error, and re-throw a custom exception with a generic message for the client.
                throw new Exception("An error occurred while saving changes.", ex);
            }
        }

    }
}

