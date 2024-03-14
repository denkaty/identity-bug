using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly UserManager<StaffUser> _userManager;
        private readonly RoleManager<StaffRole> _roleManager;

        public WeatherForecastController(UserManager<StaffUser> userManager, RoleManager<StaffRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var email = "test5@abv.bg";
            var password = "Test1234$";
            var role = "Admin";
            var existingStudentUserWithProvidedEmail = await _userManager.FindByEmailAsync(email);
            if (existingStudentUserWithProvidedEmail != null) { return BadRequest("Email address is already in use"); }
            StaffUser staffUser = new StaffUser()
            {
                Email = email,
                Id = Guid.NewGuid().ToString(),
                UserName = email
            };

            IdentityResult result1 = await _userManager.CreateAsync(staffUser, password);
            if(result1.Succeeded)
            {
                IdentityResult result2 = await _roleManager.CreateAsync(new StaffRole(role));
                if(result2.Succeeded)
                {
                    IdentityResult result3 = await _userManager.AddToRoleAsync(staffUser, role); //Error
                    if (result3.Succeeded)
                    {
                        await Console.Out.WriteLineAsync();
                    }
                }
            }
            return Created();
        }
    }
}
