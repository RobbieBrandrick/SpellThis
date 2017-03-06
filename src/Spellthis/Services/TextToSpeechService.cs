using Microsoft.Extensions.Options;
using Spellthis.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spellthis.Services
{
    public interface ITextToSpeechService
    {

        /// <summary>
        /// Create an audio file of the text that was provided
        /// </summary>
        /// <param name="text">Text to convert into speech audio file</param>
        /// <param name="audioFileLocation">Location of where the audio file will be created</param>
        Task CreateAudioFile(string text, string audioFileLocation);

    }

    public class TextToSpeechService : ITextToSpeechService
    {

        private TTSConfigurations _configurations;

        /// <summary>
        /// Sets up the services dependencies
        /// </summary>
        public TextToSpeechService(IOptions<TTSConfigurations> configurations)
        {

            if (configurations == null)
                throw new InvalidOperationException("configurations cannot be null");

            _configurations = configurations.Value;

        }

        /// <summary>
        /// Create an audio file of the text that was provided
        /// </summary>
        /// <param name="text">Text to convert into speech audio file</param>
        /// <param name="audioFileLocation">Location of where the audio file will be created</param>
        public async Task CreateAudioFile(string text, string audioFileLocation)
        {

            if(string.IsNullOrWhiteSpace(text))
                throw new InvalidOperationException("text cannot be null");

            if (string.IsNullOrWhiteSpace(audioFileLocation))
                throw new InvalidOperationException("audioFileLocation cannot be null");

            var client = new HttpClient();

            string ttsUrl = $"https://api.voicerss.org/?key={_configurations.APIKey}&hl=en-us&src=" + text;
            HttpResponseMessage httpTask = await client.GetAsync(ttsUrl);
            string responseMessage = (await httpTask.Content.ReadAsStringAsync());

            if (responseMessage.ToLower().Contains("error"))
                throw new InvalidOperationException($"Error has occurred while retrieving TTS audio file: {responseMessage}");

            byte[] response = await httpTask.Content.ReadAsByteArrayAsync();
            
            File.WriteAllBytes(audioFileLocation, response);
            
        }

    }
}
