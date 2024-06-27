using api.BusinessLogic.Interface;
using Azure.Core;
using Google.Apis.FirebaseCloudMessaging.v1;
using Google.Apis.FirebaseCloudMessaging.v1.Data;
using Newtonsoft.Json;
using System.Text;

namespace api.BusinessLogic
{
    public class NotificationBusinessLogic : INotificationBusinessLogic
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly FirebaseCloudMessagingService _fcmService;
        public NotificationBusinessLogic(FirebaseCloudMessagingService fcmService, IHttpClientFactory clientFactory)
        {
            _fcmService = fcmService;
            _clientFactory = clientFactory;
        }

        public string SendNotification(List<string> registrationTokens, string title, string body)
        {
            string fcmUrl = "https://fcm.googleapis.com/v1/projects/mariospark-182a8/messages:send";

            var client = _clientFactory.CreateClient();
            var responses = new List<string>();

            foreach (var token in registrationTokens)
            {
                var message = new
                {
                    message = new
                    {
                        token = token, // Enviar notificación a un único token
                        notification = new
                        {
                            title = title,
                            body = body
                        }
                    }
                };

                // Convertir el objeto a formato JSON
                string jsonMessage = JsonConvert.SerializeObject(message);

                // Crear una solicitud HTTP POST hacia FCM
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, fcmUrl);
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _fcmService.ApiKey);
                requestMessage.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                // Enviar la solicitud HTTP y obtener la respuesta
                var response = client.Send(requestMessage);

                // Verificar el estado de la respuesta
                if (response.IsSuccessStatusCode)
                {
                    responses.Add($"Notificación enviada correctamente a {token}");
                }
                else
                {
                    var errorResponse = response.Content.ReadAsStringAsync().Result;
                    responses.Add($"Error al enviar la notificación a {token}: {errorResponse}");
                }
            }

            return string.Join("\n", responses);
        }
    }
}
