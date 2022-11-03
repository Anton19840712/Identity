using System.Security.Cryptography;
using System.Text;

namespace IdentityApi.Extensions;

public static class HashExtensions
{
	public static string ToHashString(this string text, HashAlgorithm hasher)
	{
		return ToHashString(Encoding.Default.GetBytes(text), hasher);
	}

	public static string ToHashString(this byte[] bytes, HashAlgorithm hasher)
	{
		var data = hasher.ComputeHash(bytes);

		var builder = new StringBuilder();

		foreach (var t in data)
		{
			builder.Append(t.ToString("x2"));
		}

		return builder.ToString();
	}

	public static string ToMd5HashString(this string text)
	{
		return text.ToHashString(MD5.Create());
	}
}
