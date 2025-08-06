using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.UserDTO;

public class UpdatePassDTO
{
    public string Password { get; set; }
    [Compare("Password",ErrorMessage = "رمز عبور باهم مطابقت ندارد")]
    public string RePassword { get; set; }
}