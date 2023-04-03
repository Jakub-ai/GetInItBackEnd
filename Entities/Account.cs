namespace GetInItBackEnd.Entities;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? SubEndsAt { get; set; }
    public Role Role { get; set; }
    public Company? Company { get; set; }
    
    public virtual  List<Payment>? Payments { get; set; }
}