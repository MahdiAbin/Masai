using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.UserDTO;

public class SignUpDTO
{
    public string UserName { get; set; }
    public string MobileNumber { get; set; }
    [EmailAddress(ErrorMessage = "ایمیل معتبر وارد کنید")]
    public string Email { get; set; }
    
    public string Password { get; set; }
    [Compare("Password")]
    public string RePassword { get; set; }

    [Required(ErrorMessage = "گزینه قوانین را انتخاب کنید")]
    public bool Is { get; set; }
}