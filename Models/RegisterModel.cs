﻿using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
	[Required]
	public string FirstName { get; set; }

	[Required]
	public string LastName { get; set; }

	[Required, EmailAddress]
	public string Email { get; set; }

	[Required]
	public string Password { get; set; }

	public string PhoneNumber { get; set; } 
	public string Gender { get; set; }     
}
