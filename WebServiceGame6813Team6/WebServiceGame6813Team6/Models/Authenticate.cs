namespace WebServiceGame6813Team6.Models;

using System.ComponentModel.DataAnnotations;

public class Authenticate
{

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}

