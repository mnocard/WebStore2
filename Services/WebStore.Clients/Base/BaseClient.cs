using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected string Address { get; }

        protected HttpClient Http { get; }

        public BaseClient(IConfiguration Configuration, string ServiceAddress)
        {
            Address = ServiceAddress;

            Http = new HttpClient
            {
                BaseAddress = new Uri(Configuration["WebApiUrl"]),
                DefaultRequestHeaders =
                {
                    Accept = {new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }

        protected T Get<T>(string url) => GetAsync<T>(url).Result;
        protected async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            var response = await Http.GetAsync(url, cancellationToken);
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>(cancellationToken );
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync<T>(url, item).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken cancellationToken = default)
        {
            var response = await Http.PostAsJsonAsync(url, item, cancellationToken);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync<T>(url, item).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken cancellationToken = default)
        {
            var response = await Http.PutAsJsonAsync(url, item, cancellationToken);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken = default)
        {
            var response = await Http.DeleteAsync(url, cancellationToken);
            return response;
        }

        public void Dispose() => Dispose(true);
        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed) return;
            if (disposing)
            {
                // Очистка управляемых ресурсов
                Http.Dispose();
            }

            // Очистка неуправляемых ресурсов

            _Disposed = true;
        }
    }
}
