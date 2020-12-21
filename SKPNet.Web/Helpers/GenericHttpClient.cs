using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SKPNet.Web.Helpers
{
    public class GenericHttpClient : HttpClient
    {
        //public async Task<T> GetJsonAsync<T>(string url)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var httpClient = new HttpClient();
        //        var content = httpClient.GetAsync(url).Result;
        //        string jsonContent = await content.Content.ReadAsStringAsync();
        //        T data = JsonConvert.DeserializeObject<T>(jsonContent);
        //        return data;
        //    }
        //}

        public async Task<IEnumerable<T>> GetJsonAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var httpClient = new HttpClient();
                var content = httpClient.GetAsync(url).Result;
                string jsonContent = await content.Content.ReadAsStringAsync();
                IEnumerable<T> data = JsonConvert.DeserializeObject<IEnumerable<T>>(jsonContent);
                return data;
            }
        }


        private IEnumerable<T> PagedIterator<T>(IEnumerable<T> objectList, int PageSize)
        {
            var page = 0;
            var recordCount = objectList.Count();
            var pageCount = (int)((recordCount + PageSize) / PageSize);

            if (recordCount < 1)
            {
                yield break;
            }

            while (page < pageCount)
            {
                var pageData = objectList.Skip(PageSize * page).Take(PageSize).ToList();

                foreach (var rd in pageData)
                {
                    yield return rd;
                }
                page++;
            }
        }
    }
}
