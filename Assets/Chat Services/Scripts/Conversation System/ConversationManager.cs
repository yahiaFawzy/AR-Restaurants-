using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;
using static ConversationManager;

public class ConversationManager : SingletonBehaviour<ConversationManager>
{
    public enum ConversationStage { Idle, Listening, Thinking, Talking }

    public SystemConfiguration configuration;
    [Space]
    public Azure_Recognizer recognizer;
    public Azure_Synthesizer synthesizer;
    public ChatBot chatBot;

    private ConversationStage _conversationStage;
    public ConversationStage conversationStage
    {
        get => _conversationStage;
        private set
        {
            _conversationStage = value;
            onConversationStageChanged?.Invoke(value);
        }
    }

    public Action<ConversationStage> onConversationStageChanged;
    public Action<AudioClip> onTalkingClipLoaded;


    private void Start()
    {
        AskPermissions();
        ActivateChat();
    }

    private void AskPermissions()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }

    public void ActivateChat()
    {
        conversationStage = ConversationStage.Idle;
    }

    public async void AskChat()
    {
        if (conversationStage != ConversationStage.Idle) return;

        try
        {
            SpeechRecognitionResult recognitionResult = await ListeningStage();
            string chatMessage = await SendToChatStage(recognitionResult);
            AudioClip clip = await SynthesizeToClipStage(chatMessage);
            StartCoroutine(TalkingStage(clip));

        }
        catch
        {
            conversationStage = ConversationStage.Idle;
        }
    }

    private async Task<SpeechRecognitionResult> ListeningStage()
    {
        try
        {
            conversationStage = ConversationStage.Listening;
            return await recognizer.RecognizeFromMicrophoneInput();
        }
        catch (Exception e)
        {
            Debug.Log("Error in Recognizer : (" + e + ")");
            throw e;
        }
    }

    private async Task<string> SendToChatStage(SpeechRecognitionResult result)
    {
        try
        {
            conversationStage = ConversationStage.Thinking;

            ChatBot.ChatResponse botReply = await chatBot.SendChatMessage(result.Text);

            if (botReply.answer == "اسف لا استطيع الإجابة عن هذا السؤال.") // Temp
            {
                botReply.answer = "نحن نتكلم عن الوجبة التي اخترتوها ربما سؤالكم عن وجبة ثانية يمكنكم طرحه عند الدخول عليها";
            }

            return botReply.answer;
        }
        catch (Exception e)
        {
            Debug.Log("Error in Chat : (" + e + ")");
            throw e;
        }
    }

    private async Task<AudioClip> SynthesizeToClipStage(string chatMessage)
    {
        try
        {
            conversationStage = ConversationStage.Thinking;
            return await synthesizer.SynthesizeToClip(chatMessage);
        }
        catch (Exception e)
        {
            Debug.Log("Error in Synthesize : (" + e + ")");
            throw e;
        }
    }

    private IEnumerator TalkingStage(AudioClip clip)
    {
        conversationStage = ConversationStage.Talking;
        yield return new WaitForSeconds(1f);

        onTalkingClipLoaded.Invoke(clip);

        yield return new WaitForSeconds(clip.length + 1);

        conversationStage = ConversationStage.Idle;
    }
}
