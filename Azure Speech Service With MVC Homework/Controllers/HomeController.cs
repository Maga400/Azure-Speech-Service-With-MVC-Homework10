using Azure_Speech_Service_With_MVC_Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using System.Diagnostics;

namespace Azure_Speech_Service_With_MVC_Homework.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> TranscribeAudio()
        {
            string subscriptionKey = _configuration["AzureSpeech:SubscriptionKey"];
            string region = _configuration["AzureSpeech:Region"];

            var config = SpeechConfig.FromSubscription(subscriptionKey, region);

            using var recognizer = new SpeechRecognizer(config);

            var result = await recognizer.RecognizeOnceAsync();

            var speechResult = new SpeechResutViewModel
            {
                RecognizedText = result.Text
            };

            return Json(speechResult);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
