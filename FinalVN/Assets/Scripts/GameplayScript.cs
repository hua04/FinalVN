using UnityEngine;

public class GameplayScript : MonoBehaviour
{
    public int phaseCount = 0;
    public int[] playerInput = new int[5];
    public int stepCount = 0;

    public bool correct = true;

    public CharacterData character;

    private void Update()
    {
    }

    public void PressComplete()
    {

        playerInput[stepCount] = //Get pressure sensor number(we attach a number to each sensor)
        stepCount++;

        if (stepCount == 4)
        {
            if (phaseCount == 1)
            {
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
            }

        }

    }
}


