using Microsoft.AspNetCore.Identity;
using Workshop.UseCases.ViewModels;

namespace CustomerFrontendApp.Security
{
  public class CustomRoleStore : IRoleStore<RoleViewModel>
  {
    public Task<IdentityResult> CreateAsync(RoleViewModel role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(RoleViewModel role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public Task<RoleViewModel?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<RoleViewModel?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedRoleNameAsync(RoleViewModel role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(RoleViewModel role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string?> GetRoleNameAsync(RoleViewModel role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetNormalizedRoleNameAsync(RoleViewModel role, string? normalizedName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetRoleNameAsync(RoleViewModel role, string? roleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(RoleViewModel role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
