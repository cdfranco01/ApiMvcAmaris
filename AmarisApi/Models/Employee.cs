namespace AmarisApi.Models
{
    public class Employee
    {

         public int Id { get; set; }
         public string employee_name { get; set; }
         public decimal employee_salary { get; set; }
         public decimal employee_age { get; set; }
         public string profile_image { get; set; }
         public decimal AnnualSalary => employee_salary * 12;
        
    }
        

    
}
