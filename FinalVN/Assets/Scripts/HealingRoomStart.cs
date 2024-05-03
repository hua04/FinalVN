using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class HealingRoomStart : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
 
    // Start is called before the first frame update

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }
    void Start()
    {


        DialogueController.dialogueCount++;
        string nodeName = DialogueController.dialogueList[DialogueController.dialogueCount];

        dialogueRunner.StartDialogue(nodeName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
