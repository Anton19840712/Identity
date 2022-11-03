//using Microsoft.AspNetCore.Identity;

//namespace IdentityApi.Models;

//public class LoginCommand
//{
//	private readonly ILogger _logger;
//	private readonly UserManager<ApplicationUser> _userManager;
//	private readonly ApplicationUser _user;
//	private readonly string _password;
//	private readonly bool _shouldLockout;

//	public LoginCommand(ILogger logger, UserManager<ApplicationUser> userManager, ApplicationUser user, string password, bool shouldLockout)
//	{
//		_logger = logger;
//		_userManager = userManager;
//		_user = user;
//		_password = password;
//		_shouldLockout = shouldLockout;
//	}

//	public async Task<SignInResult> Execute()
//	{
//		_logger.LogInformation($"Found User: {_user.UserName}");
//		if (_user.IsLegacy)
//			return await new LegacyUserCommand(_logger, _userManager, _user, _password, _shouldLockout).Execute();
//		if (await _userManager.CheckPasswordAsync(_user, _password))
//			return await new CheckTwoFactorCommand(_logger, _userManager, _user).Execute();
//		if (_shouldLockout)
//		{
//			return await new CheckLockoutCommand(_logger, _userManager, _user).Execute();
//		}
//		_logger.LogDebug($"Login failed for user {_user.Email} invalid password");
//		return SignInResult.Failed;
//	}
//}