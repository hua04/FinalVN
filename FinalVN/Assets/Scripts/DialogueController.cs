using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Yarn.Unity;

public class DialogueController : MonoBehaviour
{
    public int dialogueCount=0;
    public string[] dialogueList= { "Intro", "MaxwellFrontRoom" };
    public DialogueRunner dialogueRunner;
    public Animator animator; 
    public Sprite[] characters;
    public Sprite[] jinxExpression;
    public Sprite[] maxExpression;
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }
    public void Dialogue()
    {
        string nodeName = dialogueList[dialogueCount];

        dialogueRunner.StartDialogue(nodeName);
    }
    [YarnCommand("fade_out")]
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    [YarnCommand("set_scene")]
    public void NextScene(int x)
    {
        dialogueCount = x;
    }

    [YarnCommand("sprite_change")]
    public void SpriteChange()
    {

    }
    [YarnCommand("fade_in")]
    public void FadeIn()
    {

        animator.SetTrigger("FadeIn");
    }
}
