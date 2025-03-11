using AmarisApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace AmarisApi.DAL
{
    public class EmployeeApiService
    {
        private readonly HttpClient _httpClient;
        public EmployeeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "es-ES,es;q=0.9");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _httpClient.DefaultRequestHeaders.Add("Cookie", "humans_21909=1");
            _httpClient.DefaultRequestHeaders.Add("Host", "dummy.restapiexample.com");
            _httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/133.0.0.0 Safari/537.36");
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("http://dummy.restapiexample.com/api/v1/employees");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Contenido recibido: {content}");
            foreach (var header in response.Content.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
            return await HandleListResponse(response);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://dummy.restapiexample.com/api/v1/employee/{id}");
            foreach (var header in response.Content.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Contenido recibido: {content}");

            return await HandleObjectResponse(response);
        }

        private async Task<List<Employee>> HandleListResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                //var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Employee>>>(json);
                foreach (var emp in apiResponse.Data)
                { 
                    Console.WriteLine($"Empleado: {emp.employee_name}, Salario: {emp.employee_salary}");
                }

        
                return apiResponse?.Data ?? new List<Employee>();
            }
            else if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new Exception("Error 429: Demasiadas solicitudes. Intente más tarde.");
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new Exception("Error 409: Conflicto en la solicitud.");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Error 404: Recurso no encontrado.");
            }
            else
            {
                throw new Exception($"Error {response.StatusCode}: {response.ReasonPhrase}");
            }
        }
        private async Task<Employee> HandleObjectResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Employee>>(json);
                return apiResponse?.Data ?? throw new Exception("Empleado no encontrado.");
            }
            else if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new Exception("Error 429: Demasiadas solicitudes. Intente más tarde.");
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new Exception("Error 409: Conflicto en la solicitud.");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Error 404: Recurso no encontrado.");
            }
            else
            {
                throw new Exception($"Error {response.StatusCode}: {response.ReasonPhrase}");
            }
        }


    }
}
