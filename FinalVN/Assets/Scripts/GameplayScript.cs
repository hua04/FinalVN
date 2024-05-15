using System;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class GameplayScript : MonoBehaviour
{
	public SerialPort serialPort;
	public DialogueRunner dialogueRunner;

	public int phaseCount = 1;
	public int[] playerInput = new int[5];
	public TextMeshProUGUI[] phaseOneDisplay = new TextMeshProUGUI[5];
	public TextMeshProUGUI[] phaseTwoDisplay = new TextMeshProUGUI[5];
	public TextMeshProUGUI[] phaseThreeDisplay = new TextMeshProUGUI[5];
	public int stepCount = 0;

	public bool correct = true;
	public bool listened = false;
	public bool choice = false;
	public CharacterData character;

	public PlayerInput playerInputScript;

	private int currentKey = 0;

	private int[] sensorValues = new int[5];

	private void Start()
	{
		serialPort = new SerialPort("COM5", 9600);
		serialPort.Open();
		DisableScript();
		dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
		choice=false;
	}

	private void Update()
	{

		ArduinoCode();

		const int threshold = 100;

		//AO
		if (sensorValues[0] < threshold)
		{
			currentKey = 1;
		}

		//A1
		if (sensorValues[1] < threshold)
		{
			currentKey = 2;

			if (choice == true){
				dialogueRunner.StartDialogue("MHealingTwoReject");
				choice=false;
			}

		}

		//A2
		if (sensorValues[2] < threshold)
		{
			currentKey = 3;
			if (choice == true){
				dialogueRunner.StartDialogue("MHealingTwoListen");
				choice=false;
			}
		}

		//A3
		if (sensorValues[3] < threshold)
		{
			currentKey = 4;
		}

		//A4
		if (sensorValues[4] < threshold)
		{
			currentKey = 5;
		}


	}



	public void PressComplete()
	{
		playerInput[stepCount] = currentKey; //Get pressure sensor number(we attach a number to each sensor)



		if (phaseCount == 1)
		{
			//disable progressbar script


			Debug.Log(playerInput[stepCount]);
			phaseOneDisplay[stepCount].text = playerInput[stepCount].ToString();
			if (playerInput[stepCount] == character.phaseOne[stepCount] && correct == true)
			{
				correct = true;
				//Debug.Log(playerInput[i] + "=" + character.phaseOne[i]);
			}
			else
			{
				correct = false;
				//Debug.Log("False");

			}
			stepCount++;
			if (stepCount == 5)
			{
				DisableScript();
				if (correct)
				{
					dialogueRunner.StartDialogue("MHealingOneCorrect");
				}
				else
				{
					dialogueRunner.StartDialogue("MHealingOne");
				}
				stepCount = 0;
				phaseCount++;
				Array.Clear(playerInput, 0, playerInput.Length);

			}


		}


		if (phaseCount == 2)
		{
			Debug.Log(playerInput[stepCount]);
			if (playerInput[stepCount] != 0)
			{
				phaseTwoDisplay[stepCount].text = playerInput[stepCount].ToString();
				if (playerInput[stepCount] == character.phaseTwo[stepCount] && correct == true)
				{
					correct = true;
					//Debug.Log(playerInput[i] + "=" + character.phaseOne[i]);
				}
				else
				{
					correct = false;
					//Debug.Log("False");

				}
				stepCount++;
                if (stepCount == 5)
                {
                    DisableScript();
                    if (correct)
                    {
                        dialogueRunner.StartDialogue("MHealingTwoCorrect");
                    }
                    else
                    {
                        dialogueRunner.StartDialogue("MHealingTwo");
                    }
                    stepCount = 0;
                    phaseCount++;
                    Array.Clear(playerInput, 0, playerInput.Length);

                }
            }

		}

		if (phaseCount == 3)
		{
			Debug.Log(playerInput[stepCount]);
			if (playerInput[stepCount] != 0)
			{
				phaseThreeDisplay[stepCount].text = playerInput[stepCount].ToString();
				if (playerInput[stepCount] == character.phaseThree[stepCount] && correct == true)
				{
					correct = true;
					//Debug.Log(playerInput[i] + "=" + character.phaseOne[i]);
				}
				else
				{
					correct = false;
					//Debug.Log("False");

				}
				stepCount++;
				if (stepCount == 5)
				{
					 DisableScript();

                    if (correct && listened)
                    {
                        dialogueRunner.StartDialogue("MHealingThreeCorrectListened");
                    }
                    else if (correct && !listened)
                    {
                        dialogueRunner.StartDialogue("MHealingThreeCorrect");
                    }
					else if (!correct && listened)
                    {
                        dialogueRunner.StartDialogue("MHealingThreeWrongListened");
                    }
					else if (!correct && !listened)
                    {
                        dialogueRunner.StartDialogue("MHealingThreeWrong");
                    }
                    stepCount = 0;
                    phaseCount++;
                    Array.Clear(playerInput, 0, playerInput.Length);

                }

				
			}
		}

	}

[YarnCommand("choice")]
	public void Choice()
	{
		Debug.Log("Choice");
		choice = true;
	}

 [YarnCommand("listen")]
    public void Listening(bool listen)
    {
        listened = listen;
		Debug.Log (listened);
    }

	void ArduinoCode()
	{
		if (serialPort.IsOpen)
		{
			try
			{
				// Read a line of data from the serial port
				string data = serialPort.ReadLine();

				// Split the data into an array of strings using a comma as the delimiter
				string[] dataValues = data.Split(',');

				// Ensure there are exactly 5 values in the data (for pins A0 to A4)
				if (dataValues.Length == 5)
				{
					// Parse each value as an integer and store in sensorValues array
					for (int i = 0; i < 5; i++)
					{
						if (int.TryParse(dataValues[i], out int intValue))
						{
							sensorValues[i] = intValue;
						}
						else
						{
							Debug.LogError($"Failed to parse value at index {i}: {dataValues[i]}");
							return; // Exit the function if parsing fails
						}
					}
					// Log values if desired
					LogValues(sensorValues);
				}
				else
				{
					Debug.LogError("Unexpected number of values received. Expected 5 values.");
				}
			}
			catch (Exception e)
			{
				Debug.LogError("Error reading data from serial port: " + e.Message);
			}
		}
	}

	void LogValues(int[] values)
	{
		// Log all values with their corresponding pin names (A0, A1, A2, A3, A4)
		//Debug.Log($"A0: {values[0]}, A1: {values[1]}, A2: {values[2]}, A3: {values[3]}, A4: {values[4]}");

		// Define a threshold value to determine if a sensor is being pressed
		int threshold = 100;

		// Define a target press duration (in seconds) to fully fill the progress bar
		float targetPressDuration = 5.0f;

		// Array to keep track of the press time for each sensor
		float[] pressTimes = new float[5];

		// Check each value and update the corresponding progress bar in PlayerInput
		for (int i = 0; i < values.Length; i++)
		{
			// Check if the sensor is being pressed (value is under the threshold)
			if (values[i] < threshold)
			{
				// Increment the press time for this sensor
				pressTimes[i] += Time.deltaTime;
			}
			else
			{
				// Reset the press time for this sensor if it's not being pressed
				pressTimes[i] = 0f;
			}

			// Calculate the fill amount for the progress bar based on the press time
			float fillAmount = pressTimes[i] / targetPressDuration;

			// Clamp the fill amount between 0 and 1
			fillAmount = Mathf.Clamp01(fillAmount);

			// Update the corresponding progress bar in PlayerInput
			playerInputScript.UpdateProgressBar(i, fillAmount);
		}
	}

	public int GetSensorValue(int index)
	{
		// Check if index is within bounds of sensorValues array
		if (index >= 0 && index < sensorValues.Length)
		{
			// Return the sensor value at the specified index
			return sensorValues[index];
		}
		else
		{
			Debug.LogError($"Index {index} out of bounds. Valid range is 0 to {sensorValues.Length - 1}.");
			return -1; // Return an invalid value if index is out of bounds
		}
	}

	/*private bool CompareArrays(int[] array1, int[] array2)
	{
		// Ensure both arrays have the same length
		if (array1.Length != array2.Length)
		{
			return false;
		}

		// Compare each element in the arrays
		for (int i = 0; i < array1.Length; i++)
		{
			// If any element doesn't match, return false
			if (array1[i] != array2[i])
			{
				return false;
			}
		}

		// If all elements match, return true
		return true;
	}*/

	[YarnCommand("sensor_on")]
	public void ActivateScript()
	{
		if (playerInputScript.enabled == false)
		{
			playerInputScript.enabled = true;
			Debug.Log("enabling script");
		}
	}

	[YarnCommand("sensor_off")]
	public void DisableScript()
	{
		if (playerInputScript.enabled == true)
		{
			playerInputScript.enabled = false;
			Debug.Log("disabling script");
		}
	}
	




}


