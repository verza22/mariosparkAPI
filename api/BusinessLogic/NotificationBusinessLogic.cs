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
        private readonly string _fcmServerKey = "rbIF5pOWGDlFDgqoXNnNYo8EUN7b59Mn0UWtIMAmsXo";
        public NotificationBusinessLogic(FirebaseCloudMessagingService fcmService, IHttpClientFactory clientFactory)
        {
            _fcmService = fcmService;
            _clientFactory = clientFactory;
        }

        public string SendNotification(List<string> registrationTokens, string title, string body)
        {
            var fcmUrl = "https://fcm.googleapis.com/fcm/send";

            // Construir el objeto de mensaje FCM
            var message = new
            {
                registration_ids = registrationTokens, // Lista de tokens de los dispositivos
                notification = new
                {
                    title = title,
                    body = body
                }
            };

            // Convertir el objeto a formato JSON
            string jsonMessage = JsonConvert.SerializeObject(message);

            // Crear una solicitud HTTP POST hacia FCM
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, fcmUrl);
            requestMessage.Headers.TryAddWithoutValidation("Authorization", $"key={_fcmServerKey}");
            requestMessage.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            // Enviar la solicitud HTTP y obtener la respuesta
            var client = _clientFactory.CreateClient();

            var response = client.Send(requestMessage);

            // Verificar el estado de la respuesta
            if (response.IsSuccessStatusCode)
            {
                return "Notificación enviada correctamente a todos los dispositivos";
            }
            else
            {
                var errorResponse = response.Content.ReadAsStringAsync().Result;
                return "Error al enviar la notificación: " + errorResponse;
            }
        }
    }
}
