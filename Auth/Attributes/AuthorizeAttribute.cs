﻿namespace Auth.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute
{
    public string? Instance { get; set; } = null;
}