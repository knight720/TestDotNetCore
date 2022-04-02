namespace WebAPI.Services
{
    public class JsonService : IDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public JsonService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
        }

        public string GetData()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var domain = this._configuration.GetValue<string>("GOV_API:DomainName");
            httpClient.BaseAddress = new Uri(domain);
            var response = httpClient.GetAsync("/dataset/4b1ac662-71b7-4917-978b-e8aa7763ed40/resource/a9a9bf14-b3a8-4c2b-80db-7d81649670c9/download/opendata.json").GetAwaiter().GetResult();
            string str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return str;
        }
    }
}