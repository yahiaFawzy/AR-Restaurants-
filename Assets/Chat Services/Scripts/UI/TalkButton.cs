using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TalkButton : MonoBehaviour
{
    [Space]
    public GameObject talkButton;
    public GameObject listeningIcon;
    public GameObject loadingIcon;

    private void Start()
    {
        ConversationManager.Instance.onConversationStageChanged += onConversationStageChanged;
    }

    private void onConversationStageChanged(ConversationManager.ConversationStage stage)
    {
        talkButton.SetActive(stage == ConversationManager.ConversationStage.Idle);
        listeningIcon.SetActive(stage == ConversationManager.ConversationStage.Listening);
        loadingIcon.SetActive(stage == ConversationManager.ConversationStage.Thinking
            || stage == ConversationManager.ConversationStage.Talking);
    }

    public void Talk()
    {
        ConversationManager.Instance.AskChat();
    }
}
