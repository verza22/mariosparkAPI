using api.Models;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace api.Lib
{
    public static class Util
    {
        public static void RemoveImage(string imageUrl) 
        {
            string imagePath = Path.Combine("wwwroot", imageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        public static Dictionary<string, object> ValidateRequest(JsonElement requestBody, Dictionary<string, Type> expectedParameters)
        {
            var parameters = new Dictionary<string, object>();

            foreach (var param in expectedParameters)
            {
                if (!requestBody.TryGetProperty(param.Key, out var element))
                {
                    throw new ArgumentException($"Invalid JSON format. '{param.Key}' is required.");
                }

                try
                {
                    object value;
                    if (param.Value == typeof(int))
                    {
                        value = element.GetInt32();
                    }
                    else if (param.Value == typeof(bool))
                    {
                        value = element.GetBoolean();
                    }
                    else if (param.Value == typeof(string))
                    {
                        value = element.GetString();
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported parameter type for '{param.Key}'.");
                    }

                    parameters[param.Key] = value;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error parsing '{param.Key}': {ex.Message}");
                }
            }

            return parameters;
        }
    }
}
