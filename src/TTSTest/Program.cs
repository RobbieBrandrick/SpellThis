using Spellthis.Services;
using System;
namespace TTSTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                TextToSpeechService asdf = new TextToSpeechService();

                asdf.CreateAudioFile("Great Googly Moogly", "test.mp3").Wait();
            }
            catch (Exception ex)
            {

                throw ex; 
            }

        }
    }
}
