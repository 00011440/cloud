using _11440_DSCC_API.Data;
using _11440_DSCC_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _11440_DSCC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        public CarsDbContext CarsDbContext { get; set; }

        public CarsController(CarsDbContext dbContext) 
        {
            CarsDbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCars()
        {
            return CarsDbContext.Cars.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetCar(int id)
        {
            return CarsDbContext.Cars.Find(id);
        }

        [HttpPut("{id}")]
        public ActionResult EditCar(int id, Car car)
        {
            Car oldCar = CarsDbContext.Cars.Find(id);
            CarsDbContext.Entry(oldCar).State = EntityState.Modified;
            oldCar.Model = car.Model;
            oldCar.Manufacturer = car.Manufacturer;
            oldCar.Price = car.Price;

            CarsDbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateCar(Car car)
        {
            CarsDbContext.Cars.Add(car);
            CarsDbContext.SaveChanges();
            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id)
        {
            CarsDbContext.Cars.Remove(CarsDbContext.Cars.Find(id));
            CarsDbContext.SaveChanges();
            return NoContent();
        }
    }
}
