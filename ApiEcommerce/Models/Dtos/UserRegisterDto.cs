using System;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.Dtos;

public class UserRegisterDto
{
    public string? ID {get;set;} = string.Empty;
    public required string? Username {get;set;} = string.Empty;
    public required string? Password {get;set;} = string.Empty;
    public string? Name {get;set;} = string.Empty;
    public string? Role {get;set;} = string.Empty;
}
