using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth;

public class FlexiUserStore : UserStore<User>
{
    private readonly DbContext _context;
    
    public FlexiUserStore(DbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
        _context = context;
    }
    
    public override async Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await base.FindByIdAsync(userId, cancellationToken);
        if (user != null)
        {
            await _context.Entry(user)
                .Reference(u => u.Organization)
                .Query()
                .Include(o => o.Instance)
                .LoadAsync(cancellationToken);
        }
        
        return user;
    }
}