using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.UserDTO;

public class LoginDTO
{
    [Required(ErrorMessage = "نام کاربری اجباری است")]
    public string UserName { get; set; }
    
    
    
    [Required(ErrorMessage = "رمز عبور اجباری است")]
    public string Password { get; set; }

    public bool IsRememberMe { get; set; }
}