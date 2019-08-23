using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;

namespace AdobeLaunch.ApiProxy.Models
{
    public class RequestData
    {
        private readonly HttpRequest _request;
        private readonly Uri _originUri;

        public RequestData(HttpRequest request, Uri originUri)
        {
            _request = request;
            _originUri = originUri;
        }

        private string ConvertToOriginData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return "";
            return data.Replace(_request.Host.Value, Host.Value);
        }

        private Dictionary<string, StringValues> ConvertToOriginData(IEnumerator<KeyValuePair<string, StringValues>> original)
        {
            Dictionary<string, StringValues> newData = new Dictionary<string, StringValues>();
            while (original.MoveNext())
                newData.Add(ConvertToOriginData(original.Current.Key), (StringValues)original.Current.Value.Select(ConvertToOriginData));

            return newData;
        }
        public async Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var formData = await _request.ReadFormAsync(cancellationToken).ConfigureAwait(false);
            return new FormCollection(ConvertToOriginData(formData.GetEnumerator()));
        }

        public HttpRequest OriginalRequest => _request;
        public Uri OriginUrl => new Uri($"https://{_originUri.Host}{Path.ToUriComponent()}{QueryString.ToUriComponent()}");
        public HttpContext HttpContext => _request.HttpContext;
        public string Method => _request.Method;
        public string Scheme => _request.Scheme;
        public bool IsHttps => _request.IsHttps;
        public HostString Host => new HostString(_originUri.Host, _originUri.Port);
        public PathString PathBase => _request.PathBase;
        public PathString Path => _request.Path;
        public QueryString QueryString
        {
            get {
                var ret = ConvertToOriginData(_request.QueryString.Value);

                return new QueryString(!string.IsNullOrEmpty(ret) ?  ret : "");

            }
        }

        public IQueryCollection Query => new QueryCollection(ConvertToOriginData(_request.Query.GetEnumerator()));
        public string Protocol => _request.Protocol;
        public Dictionary<string, string> Headers => _request.Headers.ToDictionary(x => x.Key, x => ConvertToOriginData(string.Join(",", x.Value.ToArray())));
        public Dictionary<string, string> Cookies => _request.Cookies.ToDictionary(x => x.Key, x => ConvertToOriginData(x.Value));
        public long? ContentLength => _request.ContentLength;
        public string ContentType => _request.ContentType;
        public Stream Body => _request.Body;
        public bool HasFormContentType => _request.HasFormContentType;
        public Dictionary<string, string> Form => _request.Form.ToDictionary(x => x.Key, x => ConvertToOriginData(string.Join(",", x.Value.ToArray())));


    }
}
