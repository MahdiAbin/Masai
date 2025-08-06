namespace Core.DTOs.UserDTO;

public class ProfileDTO
{
    public int ID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string MobileNumber { get; set; }


    public string FName { get; set; }
    public string LName { get; set; }
        
    public string? Sheba { get; set; }
    public string? NumberCart { get; set; }
    public string? CodePosti { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Job { get; set; }
    public string? NationalCode { get; set; }
    public string? CompanyName { get; set; }
    public DateTime RegisterDate { get; set; }
}