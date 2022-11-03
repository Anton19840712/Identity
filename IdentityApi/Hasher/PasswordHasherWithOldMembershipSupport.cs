using System.Security.Cryptography;
using IdentityApi.Extensions;
using IdentityApi.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Hasher;

public class PasswordHasherWithOldMembershipSupport : IPasswordHasher<ApplicationUser>
{
	readonly IPasswordHasher<ApplicationUser> _identityPasswordHasher = new PasswordHasher<ApplicationUser>();
	internal static string HashPasswordInOldFormat(string password)
	{
		return password.ToHashString(MD5.Create());
	}
	public string HashPassword(ApplicationUser user, string password)
	{
		return _identityPasswordHasher.HashPassword(user, password);
	}
	public PasswordVerificationResult VerifyHashedPassword(
		ApplicationUser user,
		string hashedPassword,
		string providedPassword)
	{
		string pwdHash2 = HashPasswordInOldFormat(providedPassword);
		
		if (hashedPassword == pwdHash2)
		{
			return PasswordVerificationResult.Success;
		}
		return _identityPasswordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
	}
}