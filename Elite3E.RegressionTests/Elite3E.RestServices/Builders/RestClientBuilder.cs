using Elite3E.Infrastructure.Configuration;
using Elite3E.RestServices.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Elite3E.RestServices.Builders
{
    public class RestClientBuilder
    {
        private RestClient _client;
        private RestRequest _request;
        private string _content;

        public RestClientBuilder Create(Uri uri = null)
        {
            var username = ApplicationConfigurationBuilder.Instance.ApiUserName;
            var password = ApplicationConfigurationBuilder.Instance.ApiUserPassword;
            var credentials = new Credentials("dentons/" + username, username, password);
            _client = new RestClient(uri ?? ApplicationConfigurationBuilder.Instance.ApiBaseUrl);
            _client.Timeout = -1;
            _client.Authenticator = new NtlmAuthenticator(credentials.Username, credentials.Password);
            return this;
        }

        public RestClientBuilder ForResource(string endpoint, Method httpVerb)
        {
            _request = new RestRequest(endpoint, httpVerb);
            return this;
        }

        public RestClientBuilder ForResource(Method httpVerb)
        {
            _request = new RestRequest(httpVerb);
            return this;
        }

        public RestClientBuilder WithHeader(string headerKey, string headerValue)
        {
            if (_request == null)
                throw new ArgumentNullException(nameof(_request), $"The request must be initialised using the {nameof(ForResource)} method");

            _request.AddHeader(headerKey, headerValue);
            return this;
        }

        public RestClientBuilder WithParameter(string parameterKey, string ParameterValue)
        {
            _request.AddParameter(parameterKey, ParameterValue);
            return this;
        }

        public RestClientBuilder WithJsonContent(string content)
        {
            _content = content;
            _request.AddParameter("application/json", _content, ParameterType.RequestBody);
            return this;
        }

        public RestClientBuilder WithFormData()
        {
            _request.AlwaysMultipartFormData =  true;
            return this;
        }

        public async Task<IRestResponse> ExecuteRequestAsync()
        {
            return await _client?.ExecuteAsync(_request ?? throw new InvalidOperationException());
        }

    }
}
