namespace Data.Models;

public class Customer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Address { get; set; }
    public string Phone { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}