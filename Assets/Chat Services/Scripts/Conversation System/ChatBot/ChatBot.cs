using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

public class ChatBot : MonoBehaviour
{
    public struct ChatRequest
    {
        public string message;
    }

    public class ChatResponse
    {
        public string answer;
    }

    public async Task<ChatResponse> SendChatMessage(string message = null)
    {
        Debug.Log("Send To ChatBot (" + message + ")");

        string url = ConversationManager.Instance.configuration.CHAT_API + message;
        url += "&apikey=" + ConversationManager.Instance.configuration.CHAT_Apikey;
        url += "&lang=" + MenuSettings.Instance.Languages.ToString();
        url += "&key=";

        // Temp
        switch (MenuSettings.Instance.Languages)
        {
            case Languages.Arabic:
                url += "Menu_Burger_AR";
                break;
            case Languages.English:
                url += "Menu_Burger_EN";
                break;
            case Languages.Turkish:
                url += "Menu_Burger_TR";
                break;
        }

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.method = "GET";
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                throw new System.Exception(request.error);

            string responceText = request.downloadHandler.text;

            ChatResponse response = JsonUtility.FromJson<ChatResponse>(responceText);

            response.answer = FixText(response.answer);
            Debug.Log("ChatBot Respons Message (" + response.answer + ")");

            return response;
        }
    }

    private static string FixText(string text)
    {
        var builder = new StringBuilder(text);
        builder
            .Replace("\\n", "\n")
            .Replace("َ", "")
            .Replace("ً", "")
            .Replace("ِ", "")
            .Replace("ٍ", "")
            .Replace("ُ", "")
            .Replace("ٌ", "")
            .Replace("ّ", "")
            .Replace("ّ", "")
            .Replace("ْ", "");
        return builder.ToString();
    }
}
