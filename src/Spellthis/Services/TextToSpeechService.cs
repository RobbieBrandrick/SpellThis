using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spellthis.Services
{

 

    public class TextToSpeechService
    {

        /// <summary>
        /// Retrieve an audio file of the text that was provided
        /// </summary>
        /// <param name="text">Text to convert into speech audio file</param>
        /// <param name="audioFileLocation">Location of where the audio file will be created</param>
        public async Task CreateAudioFile(string text, string audioFileLocation)
        {
            var client = new HttpClient();

            HttpResponseMessage httpTask = await client.GetAsync("https://api.voicerss.org/?key=644aa7709daa4352831fe9219fed4bda&hl=en-us&src=" + text);
            byte[] response = await httpTask.Content.ReadAsByteArrayAsync();
            
            File.WriteAllBytes(audioFileLocation, response);
            
        }

    }
}
