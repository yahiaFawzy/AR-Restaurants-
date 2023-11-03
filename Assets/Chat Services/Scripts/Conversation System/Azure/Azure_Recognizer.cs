using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Azure_Recognizer : MonoBehaviour
{
    private SpeechRecognizer recognizer;

    private void Start()
    {
        SpeechConfig speechConfig = Azure_SpeechManager.Instance.GetSpeechConfig();

        AudioConfig audioConfig = AudioConfig.FromDefaultMicrophoneInput();

        SourceLanguageConfig sourceLanguageConfig = SourceLanguageConfig.FromLanguage(
            ConversationManager.Instance.configuration.GetRecognizerLanguage(MenuSettings.Instance.Languages.ToString()));

        recognizer = new SpeechRecognizer(speechConfig
            , sourceLanguageConfig, audioConfig);

        recognizer.Properties.SetProperty(PropertyId.AudioConfig_PlaybackBufferLengthInMs
              , "0");
        recognizer.Properties.SetProperty(PropertyId.Conversation_Initial_Silence_Timeout
              , "0");
        recognizer.Properties.SetProperty(PropertyId.SpeechServiceConnection_EndSilenceTimeoutMs
              , "0");
        recognizer.Properties.SetProperty(PropertyId.SpeechServiceConnection_InitialSilenceTimeoutMs
             , "0");
        recognizer.Properties.SetProperty(PropertyId.SpeechServiceResponse_RecognitionLatencyMs
             , "0");

        recognizer.Recognized += OnRecognitionEnd;

        Debug.Log("Speech Recognizer Setup Finished");
    }

    private void OnRecognitionEnd(object sender, SpeechRecognitionEventArgs e)
    {
        if (e.Result.Reason == ResultReason.RecognizedSpeech)
        {
            Debug.Log("Speech Recognition Finish Result (" + e.Result.Text + ")");
        }
        else if (e.Result.Reason == ResultReason.NoMatch)
        {
            Console.WriteLine($"NOMATCH: Speech could not be recognized.");
        }
    }

    public async Task<SpeechRecognitionResult> RecognizeFromMicrophoneInput()
    {
        Debug.Log("Speak into your microphone.");
        return await recognizer.RecognizeOnceAsync();
    }
}
