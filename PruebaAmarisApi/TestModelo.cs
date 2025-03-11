using AmarisApi.Models;
using Xunit;

namespace PruebaAmarisApi
{
    public class TestModelo
    {
        [Fact]
        public void Employee_Should_Set_And_Get_Properties_Correctly()
        {
            var employee = new Employee
            {
                Id = 1,
                employee_name = "John Doe",
                employee_salary = 5000m,
                employee_age = 30,
                profile_image = "https://example.com/profile.jpg"
            };
            Assert.Equal(1, employee.Id);
            Assert.Equal("John Doe", employee.employee_name);
            Assert.Equal(5000m, employee.employee_salary);
            Assert.Equal(30m, employee.employee_age);
            Assert.Equal("https://example.com/profile.jpg", employee.profile_image);
        }

        [Fact]
        public void AnnualSalary_Should_Calculate_Correctly()
        {
            var employee = new Employee { employee_salary = 5000m };
            var annualSalary = employee.AnnualSalary;
            Assert.Equal(60000m, annualSalary); // 5000 * 12
        }

        [Theory]
        [InlineData(1000, 12000)]
        [InlineData(2500, 30000)]
        [InlineData(0, 0)]
        [InlineData(-1000, -12000)] 
        public void AnnualSalary_Should_Handle_Various_Salary_Values(decimal salary, decimal expectedAnnualSalary)
        {
          
            var employee = new Employee { employee_salary = salary };
            var annualSalary = employee.AnnualSalary;
            Assert.Equal(expectedAnnualSalary, annualSalary);
        }
        [Fact]
        public void ApiResponse_ShouldStoreStatusAndData()
        {
            // Arrange
            var expectedStatus = "success";
            var expectedData = new List<int> { 1, 2, 3 };

            // Act
            var response = new ApiResponse<List<int>> { Status = expectedStatus, Data = expectedData };

            // Assert
            Assert.Equal(expectedStatus, response.Status);
            Assert.Equal(expectedData, response.Data);
        }

        [Fact]
        public void ApiResponse_ShouldHandleNullData()
        {
            // Arrange
            var expectedStatus = "error";

            // Act
            var response = new ApiResponse<object> { Status = expectedStatus, Data = null };

            // Assert
            Assert.Equal(expectedStatus, response.Status);
            Assert.Null(response.Data);
        }
    }
}