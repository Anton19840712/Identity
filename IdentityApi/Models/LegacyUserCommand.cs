using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Models;

public class LegacyUserCommand
{
	private readonly ILogger _logger;
	private readonly UserManager<ApplicationUser> _userManager;

	private readonly ApplicationUser _user;
	private readonly string _password;
	private readonly bool _shouldLockout;

	public LegacyUserCommand(ILogger logger, UserManager<ApplicationUser> userManager, ApplicationUser user,
		string password, bool shouldLockout)
	{
		_logger = logger;
		_userManager = userManager;
		_user = user;
		_password = password;
		_shouldLockout = shouldLockout;
	}

	//public async Task<SignInResult> Execute()
	//{
	//	try
	//	{
	//		if (_password.EncodePassword(_user.LegacyPasswordSalt) == _user.LegacyPasswordHash)
	//		{
	//			_logger.LogInformation(LoggingEvents.LegacyUserCommand,
	//				"Legacy User {_user.Id} migrating password.", _user.Id);
	//			await _userManager.AddPasswordAsync(_user, _password);
	//			_user.SecurityStamp = Guid.NewGuid().ToString();
	//			_user.LegacyPasswordHash = null;
	//			_user.LegacyPasswordSalt = null;
	//			await _userManager.UpdateAsync(_user);
	//			return await new CheckTwoFactorCommand(_logger, _userManager, _user).Execute();
	//		}

	//		if (_shouldLockout)
	//		{
	//			_user.SecurityStamp = Guid.NewGuid().ToString();
	//			await _userManager.UpdateAsync(_user);
	//			_logger.LogInformation(LoggingEvents.LegacyUserCommand,
	//				"Login failed for Legacy user {_user.Id} invalid password. (LockoutEnabled)", _user.Id);
	//			await _userManager.AccessFailedAsync(_user);
	//			if (await _userManager.IsLockedOutAsync(_user))
	//				return SignInResult.LockedOut;
	//		}

	//		_logger.LogInformation(LoggingEvents.LegacyUserCommand,
	//			"Login failed for Legacy user {_user.Id} invalid password", _user.Id);
	//		return SignInResult.Failed;
	//	}
	//	catch (Exception e)
	//	{
	//		_logger.LogError(LoggingEvents.LegacyUserCommand,
	//			"LegacyUserCommand Failed for [_user.Id: {_user.Id}]  [Error Message: {e.Message}]", _user.Id,
	//			e.Message);
	//		_logger.LogTrace(LoggingEvents.LegacyUserCommand,
	//			"LegacyUserCommand Failed for [_user.Id: {_user.Id}] [Error: {e}]", _user.Id, e);
	//		return SignInResult.Failed;
	//	}
	//}
}