using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Configuration/System Configuration")]
public class SystemConfiguration : ScriptableObject
{
    public string CHAT_API;
    public string CHAT_Apikey;
    [Space]
    public string AZURE_Apikey;
    public string AZURE_Region;
    [Space]
    public List<Azure_LanguageSetting> AZURE_LanguagesSetting;

    [Header("Arabic")]
    public string AZURE_AR_SpeechSynthesisVoiceName;
    public string AZURE_AR_RecognizerLanguage;
    [Header("English")]
    public string AZURE_EN_SpeechSynthesisVoiceName;
    public string AZURE_EN_RecognizerLanguage;
    [Header("Turkish")]
    public string AZURE_TR_SpeechSynthesisVoiceName;
    public string AZURE_TR_RecognizerLanguage;

    public string GetSpeechSynthesisVoiceName(string languageName)
    {
        return AZURE_LanguagesSetting.First(l => l.LanguageName == languageName)?.SpeechSynthesisVoiceName;
    }

    public string GetRecognizerLanguage(string languageName)
    {
        return AZURE_LanguagesSetting.First(l => l.LanguageName == languageName)?.RecognizerLanguage;
    }

    [Serializable]
    public class Azure_LanguageSetting
    {
        public string LanguageName;
        public string SpeechSynthesisVoiceName;
        public string RecognizerLanguage;
    }

}
