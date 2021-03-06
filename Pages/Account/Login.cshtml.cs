using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace  MEMA_Planning_Schedule.Pages.Account
{
    public class Login : PageModel
    {
        [BindProperty]
        public LoginForm Form { get; set; } 

        public void OnGet(string returnUrl)
        {
            Form = new LoginForm {ReturnUrl = returnUrl};
        }

        public async Task<IActionResult> OnPostAsync([FromServices] SignInManager<IdentityUser> signInManager)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await signInManager.PasswordSignInAsync(Form.Username, Form.Password, true, false);

            if(result.Succeeded)
            {
                return Redirect(Form.ReturnUrl);
            }

            return Page();

        }
        public class LoginForm
        {
            [Required]
            public string ReturnUrl = "http://localhost:3000";
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}