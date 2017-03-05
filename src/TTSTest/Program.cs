using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Spellthis.Models;
using Spellthis.Services;
using System;
using System.IO;

namespace TTSTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            try
            {

                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                var configuration = configurationBuilder.Build();

                IOptions<TTSConfigurations> ttsConfiguration = Options.Create(new TTSConfigurations() { APIKey = configuration["APIKey"] });
                TextToSpeechService asdf = new TextToSpeechService(ttsConfiguration);

                asdf.CreateAudioFile("Great Googly Moogly", "test.mp3").Wait();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
