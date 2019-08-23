# Adobe Launch API Client

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
			"CLIENT_SECRET");

var securityKey = GetSecurityKey("CERTIFICATE_PATH");
var reactorApi = new ReactorApi(accountOptions, securityKey);

await _reactorApi.Client.Companies().ConfigureAwait(false);
```
## Creating a certificate
```
openssl req -x509 -sha256 -nodes -days 365 -newkey rsa:2048 -keyout adobe_launch_console.key -out adobe_launch_console.crt
```
```
openssl pkcs12 -in adobe_launch_console.crt -inkey adobe_launch_console.key -export -out adobe_launch_console.pfx
```
## Available methods 

https://github.com/chancity/Adobe-ReactorApi-CSharp/blob/master/AdobeReactorBase/IReactorClient.cs
