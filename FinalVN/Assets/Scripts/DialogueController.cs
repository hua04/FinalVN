using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Yarn.Unity;

public class DialogueController : MonoBehaviour
{
    public static int dialogueCount=0;
    public static string[] dialogueList= { "Intro", "MFrontRoomOne", "MHealingZero" };
    public DialogueRunner dialogueRunner;
    public Animator animator; 
    public Sprite[] characters;
    public Sprite[] jinxExpression;
    public Sprite[] maxExpression;
    public GameplayScript gameplayScript;
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    [YarnCommand("check")]
    public bool Check()
    {
        return gameplayScript.correct;
    }
    public void Dialogue()
    {
        if(SceneManager.GetActiveScene().name== "FrontRoom")
        {
            string nodeName = dialogueList[dialogueCount];

            dialogueRunner.StartDialogue(nodeName);
        }
       
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
