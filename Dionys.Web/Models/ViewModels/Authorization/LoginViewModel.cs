using System.ComponentModel.DataAnnotations;

namespace Dionys.Web.Models.ViewModels.Authorization
{
    public class AuthenticationRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
