using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConversationController : MonoBehaviour
{
    public ConversationManager conversationManager;
    [Space]
    public Animator animator;
    public AudioSource audioSource;

    private void Start()
    {
        conversationManager.onConversationStageChanged += onConversationStageChanged;
        conversationManager.onTalkingClipLoaded += onTalkingClipLoaded;
    }

    private void onConversationStageChanged(ConversationManager.ConversationStage stage)
    {
        animator.SetBool("IsListening", stage == ConversationManager.ConversationStage.Listening);
        animator.SetBool("IsThinking", stage == ConversationManager.ConversationStage.Thinking);
        animator.SetBool("IsTalking", stage == ConversationManager.ConversationStage.Talking);
    }
    private void onTalkingClipLoaded(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

}
