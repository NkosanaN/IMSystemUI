﻿using System.ComponentModel.DataAnnotations;

namespace IMSystemUI.Domain;

public class Login
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}