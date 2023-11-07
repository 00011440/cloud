using _11440_DSCC_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace _11440_DSCC_MVC.Controllers
{
    public class CarController : Controller
    {
        private const string baseUrl = "http://localhost:5087/";
        private readonly Uri baseAddress = new Uri(baseUrl);
        private readonly HttpClient client;

        public CarController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        private void HeaderClearing()
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Car
        public async Task<ActionResult> Index()
        {
            List<Car> cars = new List<Car>();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync("api/Cars");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                cars = JsonConvert.DeserializeObject<List<Car>>(responseMessage);
            }
            return View(cars);
        }

        // GET: Car/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Car car = new Car();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/Cars/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<Car>(responseMessage);
            }
            return View(car);
        }

        // GET: Car/Create
        public async Task<ActionResult> CreateAsync()
        {
            Car car = new Car();
            HeaderClearing();
            return View(car);
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                string createGenreInfo = JsonConvert.SerializeObject(car);
                StringContent stringContentInfo = new StringContent(createGenreInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage createHttpResponseMessage = client.PostAsync(client.BaseAddress + "api/Cars", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Car car = new Car();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/Cars/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<Car>(responseMessage);
            }

            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Car car)
        {
            if (ModelState.IsValid)
            {
                string createCarInfo = JsonConvert.SerializeObject(car);
                StringContent stringContentInfo = new StringContent(createCarInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = client.PutAsync(client.BaseAddress + $"api/Cars/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Car car = new Car();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/Cars/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<Car>(responseMessage);
            }
            return View(car);
        }
    }
}
