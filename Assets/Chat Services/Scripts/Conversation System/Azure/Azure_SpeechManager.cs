using Microsoft.CognitiveServices.Speech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azure_SpeechManager : SingletonBehaviour<Azure_SpeechManager>
{
    private SpeechConfig speechConfig;

    private void SetSpeechConfig()
    {
        speechConfig = SpeechConfig.FromSubscription(
            ConversationManager.Instance.configuration.AZURE_Apikey
            , ConversationManager.Instance.configuration.AZURE_Region);

        speechConfig.SpeechSynthesisVoiceName = ConversationManager.Instance.configuration
            .GetSpeechSynthesisVoiceName(MenuSettings.Instance.Languages.ToString());
    }

    public SpeechConfig GetSpeechConfig()
    {
        if (speechConfig == null)
        {
            SetSpeechConfig();
        }
        return speechConfig;
    }


}
