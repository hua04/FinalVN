using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Yarn.Unity;
using Unity.VisualScripting;
using UnityEngine.UI;


public class DialogueController : MonoBehaviour
{
    public static int dialogueCount = 0;
    public static string[] dialogueList = { "Intro", "MFrontRoomOne", "MHealingZero" };
    public DialogueRunner dialogueRunner;
    public Animator animator;

    public Sprite[] jExpressionArray;
    public GameObject jExpression;
    public SpriteRenderer jExpressionSprite;

    public Sprite[] mExpressionArray;
    public GameObject mExpression;
    public SpriteRenderer mExpressionSprite;

    public Sprite[] mRExpressionArray;
    public GameObject mRExpression;
    public SpriteRenderer mRExpressionSprite;

    public GameplayScript gameplayScript;

    private GameObject originalObject;

    /*public void Awake(){
        
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Check");

        if (objectsWithTag.Length > 1)
        {
            Destroy(gameObject);
        }
    }*/
    public void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        jExpressionSprite = jExpression.GetComponent<SpriteRenderer>();
        mExpressionSprite = mExpression.GetComponent<SpriteRenderer>();
        mExpression.SetActive(false);
    }

    [YarnCommand("check")]
    public bool Check()
    {
        return gameplayScript.correct;
    }
    public void Dialogue()
    {
        if (SceneManager.GetActiveScene().name == "FrontRoom")
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

    [YarnCommand("jinx_change")]
    public void JinxSpriteChange(string expression)
    {
        if (mExpression.activeSelf)
        {
            mExpression.SetActive(false);
            jExpression.SetActive(true);
        }
        for (int i = 0; i < jExpressionArray.Length; i++)
        {
            if (jExpressionArray[i].name == expression)
            {
                jExpressionSprite.sprite = jExpressionArray[i];
            }
        }
    }

    [YarnCommand("max_change")]
    public void MaxSpriteChange(string expression)
    {
        if (jExpression.activeSelf)
        {
            jExpression.SetActive(false);
            mExpression.SetActive(true);
        }

        for (int i = 0; i < mExpressionArray.Length; i++)
        {
            if (mExpressionArray[i].name == expression)
            {
                mExpressionSprite.sprite = mExpressionArray[i];
            }
        }
    }


    [YarnCommand("max_room_change")]
    public void MaxRoomChange(string expression)
    {

        for (int i = 0; i < mRExpressionArray.Length; i++)
        {
            if (mRExpressionArray[i].name == expression)
            {
                mRExpressionSprite.sprite = mRExpressionArray[i];
            }
        }
    }

    
    [YarnCommand("max_grab")]
    public void MaxLinking()
    {
        mRExpression = GameObject.Find("CharacterSprite");
        mRExpressionSprite = mRExpression.GetComponent<SpriteRenderer>();
    }
/*
    [YarnCommand("m_grab")]
    public void MaxLinkingFront()
{
    Debug.Log("MaxLinkingFront() called");
    GameObject maxObject = GameObject.Find("MaxSprite");

    if (maxObject != null)
    {
        Debug.Log("MaxSprite found");
        mExpressionSprite = maxObject.GetComponent<SpriteRenderer>();

        if (mExpressionSprite != null)
        {
            Debug.Log("SpriteRenderer component found");
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on MaxSprite");
        }
    }
    else
    {
        Debug.LogError("MaxSprite not found");
    }
}


    [YarnCommand("j_grab")]
    public void JinxLinkingFront()
    {
        Debug.Log("Jinx grabbed");
        jExpression = GameObject.Find("JinxSprite");
        jExpressionSprite = jExpression.GetComponent<SpriteRenderer>();
    }
*/


    [YarnCommand("fade_in")]
    public void FadeIn()
    {

        animator.SetTrigger("FadeIn");
    }
}
