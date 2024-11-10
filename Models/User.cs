using System.ComponentModel.DataAnnotations;

public class User
{
	public int Id { get; set; }

	[Required]
	public string FirstName { get; set; }

	[Required]
	public string LastName { get; set; }

	[Required, EmailAddress]
	public string Email { get; set; }

	[Required]
	public string PasswordHash { get; set; }
	public string PhoneNumber { get; set; }  
	public string Gender { get; set; }      
}
