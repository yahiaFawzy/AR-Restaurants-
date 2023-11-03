using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Azure_Synthesizer : MonoBehaviour
{
    private SpeechSynthesizer synthesizer;

    private void Awake()
    {
        SpeechConfig speechConfig = Azure_SpeechManager.Instance.GetSpeechConfig();
        speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Raw16Khz16BitMonoPcm);

        synthesizer = new SpeechSynthesizer(speechConfig, null);

        Debug.Log("Speech Synthesizer Setup Finished");
    }

    public async Task<AudioClip> SynthesizeToClip(string text)
    {
        Debug.Log("Synthesize Start for (" + text + ")");
        using (var result = await synthesizer.SpeakTextAsync(text))
        {
            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                var sampleCount = result.AudioData.Length / 2;
                var audioData = new float[sampleCount];
                for (var i = 0; i < sampleCount; ++i)
                {
                    audioData[i] = (short)(result.AudioData[i * 2 + 1] << 8 | result.AudioData[i * 2]) / 32768.0F;
                }

                AudioClip audioClip = AudioClip.Create("SynthesizedAudio", sampleCount, 1, 16000, false);
                audioClip.SetData(audioData, 0);

                Debug.Log("Synthesize End for (" + text + ")");

                return audioClip;
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            }
        }

        throw new Exception();
    }
}
