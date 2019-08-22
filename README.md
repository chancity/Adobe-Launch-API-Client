# Adobe-Launch-API-Client

```
private static SecurityKey GetSecurityKey(string filePath)
{
	var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(filePath));
	var rsaPrivateKey = certificate.GetRSAPrivateKey();
	return new RsaSecurityKey(rsaPrivateKey);
}

var accountOptions = new AccountOptions(
						"ORGANIZATION_ID",
						"TECHNICAL_ACCOUNT_ID",
						"CLIENT_ID",
						"CLIENT_SECRET",
						GetSecurityKey("CERTIFICATE_PATH"));

var reactorApi = new ReactorApi(accountOptions);

await _reactorApi.Client.Companies().ConfigureAwait(false);
```

## Available methods 

https://github.com/chancity/Adobe-Launch-API-Client/blob/master/AdobeLaunch.Client/Internals/ReactorClient.cs#L35-L283
