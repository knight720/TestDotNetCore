namespace WebAPI.Services
{
    public class XmlService : IDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public XmlService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public string GetData()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("gov");
            var response = httpClient.GetAsync("/dataset/4b1ac662-71b7-4917-978b-e8aa7763ed40/resource/2330c49d-0c93-4d4e-8b9c-48f5c5ce958a/download/opendata.xml").GetAwaiter().GetResult();
            string str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return str;
        }
    }
}