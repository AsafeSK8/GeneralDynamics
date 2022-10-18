using Blazored.LocalStorage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Services.Generic
{
    public class GenericService : IGenericService
    {

        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorageService;

        public GenericService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        #region Main Methods

        public async Task<T> Get<T>(string path)
        {
            var token = await GetToken();
            HttpRequestMessage request = GenerateRequestWithAuthorization(HttpMethod.Get, path, token);
            return await ReceiveParsedObject<T>(request);
        }

        public async Task<T> Post<T>(string path, object obj)
        {
            var token = await GetToken();
            HttpRequestMessage request = GenerateRequestWithAuthorization(HttpMethod.Post, path, token);
            return await sendAndReceiveParsedObject<T>(request, obj);

        }

        #endregion



        #region Auxiliary Methods

        private async Task<string> GetToken()
        {
            return await _localStorageService.GetItemAsync<string>("token");
        }

        private HttpRequestMessage GenerateRequestWithAuthorization(HttpMethod method, string path, string token)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, path);
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return requestMessage;
        }

        private async Task<T> ReceiveParsedObject<T>(HttpRequestMessage requestMessage)
        {
            var request = await _httpClient.SendAsync(requestMessage);
            var requestBody = request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(requestBody);
        }

        private async Task<T> sendAndReceiveParsedObject<T>(HttpRequestMessage requestMessage, object obj)
        {
            requestMessage.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(obj),Encoding.UTF8, "application/json");
            var request = await _httpClient.SendAsync(requestMessage);
            var requestBody = request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(requestBody);
        }

        #endregion


        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~CoberturaService()
        // {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
