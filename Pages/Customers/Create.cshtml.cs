using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace CRMAPP.Pages.Customers
{
    public class Create : PageModel
    {

    
        [BindProperty,Required(ErrorMessage = "The first Name is required")]
        public string Firstname { get; set; } = "";
        [BindProperty,Required(ErrorMessage = "The last Name is required")]
        public string Lastname  {get; set; } = "";
        [BindProperty,Required,EmailAddress]
        public string Email { get; set; } = "";
        [BindProperty,Phone]
        public string? Phone { get; set; } = "";
        [BindProperty]
        public string? Address { get; set; } = "";
        [BindProperty,Required]
        public string Company { get; set; } = "";
        [BindProperty]
        public string? Notes { get; set; } = "";
        

        public void OnGet()
        {
        }

        public string ErrorMessage { get; set; } = "";

         public void OnPost()
        {
            if(!ModelState.IsValid){
                return;
            }
            if(Phone ==null) Phone = "";
            if(Address ==null) Phone = "";
            if(Notes ==null) Phone = "";

            //create new customer
            try{
                string connectionString = "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                 using (SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "INSERT INTO customers " +
                        "(firstname, lastname, email, phone, address, company, notes) VALUES" +
                        "(@firstname, @lastname, @email, @phone, @address, @company, @notes)";
                
                    using (SqlCommand command = new SqlCommand(sql,connection)){
                        command.Parameters.AddWithValue("@firstname",Firstname);
                        command.Parameters.AddWithValue("@lastname",Lastname);
                        command.Parameters.AddWithValue("@email",Email);
                        command.Parameters.AddWithValue("@phone",Phone);
                        command.Parameters.AddWithValue("@address",Address);
                        command.Parameters.AddWithValue("@company",Company);
                        command.Parameters.AddWithValue("@notes",Notes);

                        command.ExecuteNonQuery();
                    }
                }
                                      
            }catch(Exception ex){
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Customers/Index");
        }
    }
}