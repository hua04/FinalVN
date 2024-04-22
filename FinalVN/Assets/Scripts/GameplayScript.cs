using UnityEngine;
using System;

public class GameplayScript : MonoBehaviour
{
    public int phaseCount = 1;
    public int[] playerInput = new int[5];
    public int stepCount = 0;

    public bool correct = true;

    public CharacterData character;

    public PlayerInput playerInputScript;

	public KeyCode aKey = KeyCode.A;
	public KeyCode sKey = KeyCode.S;
	public KeyCode dKey = KeyCode.D;
	public KeyCode fKey = KeyCode.F;
	public KeyCode gKey = KeyCode.G;

	private int currentKey = 0;

	private void Update()
    {
		if(aKey == playerInputScript.currentKey)
        {
			currentKey = 1;
        }

		if (sKey == playerInputScript.currentKey)
        {
			currentKey = 2;
        }

		if (dKey == playerInputScript.currentKey)
        {
			currentKey = 3;
        }

		if (fKey == playerInputScript.currentKey)
        {
			currentKey = 4;
        }

		if (gKey == playerInputScript.currentKey)
        {
			currentKey = 5;
        }
    }

    public void PressComplete()
    {

		playerInput[stepCount] = currentKey; //Get pressure sensor number(we attach a number to each sensor)
        stepCount++;

		if(stepCount == 5){
			phaseCount++;

			if (phaseCount == 1){
				for (int i = 0; i < 4; i++)
				{
					if (playerInput[i] == character.phaseOne[i] && correct == true)
					{
						correct = true;
					}
					else
					{
						correct = false;
					}
				}
				Array.Clear(playerInput, 0, playerInput.Length);
				stepCount = 0;
				//Debug.Log("Phase");
				//phaseCount++;
				//go to story module;
			}


			if (phaseCount == 2){
				for (int i = 0; i < 4; i++)
				{
					if (playerInput[i] == character.phaseTwo[i] && correct == true)
					{
						correct = true;
					}
					else
					{
						correct = false;
					}
				}
				Array.Clear(playerInput, 0, playerInput.Length);
				stepCount = 0;
				//Debug.Log("Phase");
				//phaseCount++;
				//go to story module;
			}

			if (phaseCount == 3)
			{
				for (int i = 0; i < 4; i++)
				{
					if (playerInput[i] == character.phaseThree[i] && correct == true)
					{
						correct = true;
					}
					else
					{
						correct = false;
					}
				}
				Array.Clear(playerInput, 0, playerInput.Length);
				stepCount = 0;
				//Debug.Log("Phase");
				//phaseCount++;
				//go to story module;
			}

		}





	}
}


