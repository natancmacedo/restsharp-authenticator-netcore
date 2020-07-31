# RestSharp.Authenticators

Dotnet standard library to help authenticate in REST APIs.

Is a nuget library to extend the current avaliable authenticators in RestSharp nuget (v106)

## Installation

Use the NugetPackage Manager (substitute X with the version number):

```bash
Install-Package RestSharp.Authenticators -Version X
```
The list of versions is avaliable in: https://www.nuget.org/packages/RestSharp.Authenticators/

## Usage

After the nuget installation, import the namespaces like:
```csharp
using RestSharp;
using RestSharp.Authenticators;
```

Then you can use the authenticators avaliables in RestSharp and RestSharp.Authenticators nugets.
- Sensedia OAuth2

```csharp
            var client = new RestClient(Uri);

            client.Authenticator = new SensediaOAuth2Authenticator
            {
                ClientId = "2fe3da57-a5a1-44f7-9b9b-a3a4190ff087",
                ClientSecret = "241124c7-589c-4218-a175-6cbada3f49f1",
                AuthResource = "oauth/access-token",
                EndPoint = "https://apigateway.mycompany.com"
            };

            var request = new RestRequest($"api/v1/clients/1", Method.GET);

            var response = client.Execute(request);
```

- Sensedia Client
```csharp
            client.Authenticator = new SensediaClientAuthenticator
            {
                ClientId = "2fe3da57-a5a1-44f7-9b9b-a3a4190ff087"
            };
```


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## Thanks

Thank you guys for your contribution:
@marcotalles 

## Credits

<div>Icons made by <a href="https://www.flaticon.com/authors/eucalyp" title="Eucalyp">Eucalyp</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>


## License
[MIT](https://choosealicense.com/licenses/mit/)