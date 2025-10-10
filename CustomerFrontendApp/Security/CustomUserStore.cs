using Microsoft.AspNetCore.Identity;
using Workshop.UseCases.ViewModels;

namespace CustomerFrontendApp.Security
{
  public class CustomUserStore : IUserStore<UserViewModel>
  {
    public Task<IdentityResult> CreateAsync(UserViewModel user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(UserViewModel user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public Task<UserViewModel?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<UserViewModel?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedUserNameAsync(UserViewModel user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetUserIdAsync(UserViewModel user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string?> GetUserNameAsync(UserViewModel user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(UserViewModel user, string? normalizedName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetUserNameAsync(UserViewModel user, string? userName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(UserViewModel user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
