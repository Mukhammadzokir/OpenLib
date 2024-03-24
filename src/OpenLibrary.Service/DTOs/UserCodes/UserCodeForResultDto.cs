namespace OpenLibrary.Service.DTOs.UserCodes;

public class UserCodeForResultDto
{
    public long UserId { get; set; }
    public long Code {  get; set; }
    public DateTime ExpireDate { get; set; }
}
