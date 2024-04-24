using UnityEngine;
using System;
using System.IO.Ports;
using System.Collections.Generic;

public class GameplayScript : MonoBehaviour
{
	public SerialPort serialPort;

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

	private void Start()
	{
		serialPort = new SerialPort("/dev/cu.usbmodem11401", 9600);
		serialPort.Open();
	}

	private void Update()
	{
		ArduinoCode();

		if (aKey == playerInputScript.currentKey)
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

		if (stepCount == 5)
		{
			phaseCount++;

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
				Array.Clear(playerInput, 0, playerInput.Length);
				stepCount = 0;
				//Debug.Log("Phase");
				//phaseCount++;
				//go to story module;
			}


			if (phaseCount == 2)
			{
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

	public void ArduinoCode()
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
					// Initialize an array to hold the integer values
					int[] intValues = new int[5];

					// Try to parse each value as an integer
					for (int i = 0; i < 5; i++)
					{
						if (int.TryParse(dataValues[i], out int intValue))
						{
							intValues[i] = intValue;
						}
						else
						{
							Debug.LogError($"Failed to parse value at index {i}: {dataValues[i]}");
							return; // Exit the function if parsing fails
						}
					}

					// Log the values with their corresponding pin names
					LogValues(intValues);
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
		Debug.Log($"A0: {values[0]}, A1: {values[1]}, A2: {values[2]}, A3: {values[3]}, A4: {values[4]}");

		// Check each value and log whether it is less than 50
		for (int i = 0; i < values.Length; i++)
		{
			string pinName = $"A{i}"; // Pin name (A0 to A4)
			int value = values[i]; // Value corresponding to the pin
			if (value < 50)
			{
				Debug.Log($"{pinName} value {value} < 50.");
			}
			else
			{
				Debug.Log($"{pinName} value {value} > 50.");
			}
		}
	}

}


