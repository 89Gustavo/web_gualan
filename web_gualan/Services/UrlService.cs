namespace web_gualan.Services
{
    public class UrlService
    {
        private readonly IHttpContextAccessor _http;

        public UrlService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string BaseUrl
        {
            get
            {
                var request = _http.HttpContext.Request;
                return $"{request.Scheme}://{request.Host}{request.PathBase}";
            }
        }

        public string Build(string path)
        {
            return $"{BaseUrl}/{path.TrimStart('/')}";
        }
    }
}
