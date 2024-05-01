using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    // Array of progress bar images (A0–A4)
    public Image[] progressBars;

    // Array to track the hold duration for each sensor (A0–A4)
    private float[] sensorTimers = new float[5];

    // Target time for progress bar to fill up fully (e.g., 5 seconds)
    private float targetTime = 5f;

    // Threshold for sensor values to consider the sensor pressed
    private const float sensorThreshold = 50f;

    // Reference to the GameplayScript
    public GameplayScript gameplayScript;

    void Update()
    {
        // Iterate through each sensor and its corresponding progress bar
        for (int i = 0; i < progressBars.Length; i++)
        {
            // Get the current sensor value from GameplayScript
            float sensorValue = gameplayScript.GetSensorValue(i);

            // Check if the sensor value is greater than or equal to the threshold
            if (sensorValue >= sensorThreshold)
            {
                // Reset the sensor timer
                sensorTimers[i] = 0f;

                // Reset the progress bar fill amount to 0
                progressBars[i].fillAmount = 0f;
            }
            else
            {
                // Increment the timer for the current sensor
                sensorTimers[i] += Time.deltaTime;

                // Calculate the fill amount based on the sensor timer and target time
                float fillAmount = sensorTimers[i] / targetTime;

                // Clamp the fill amount between 0 and 1
                fillAmount = Mathf.Clamp01(fillAmount);

                // Update the corresponding progress bar
                progressBars[i].fillAmount = fillAmount;

                // If the progress bar is full (fillAmount >= 1.0f), call PressComplete()
                if (fillAmount >= 1.0f)
                {
                    gameplayScript.PressComplete();

                    // Reset the timer and progress bar fill amount to 0
                    sensorTimers[i] = 0f;
                    progressBars[i].fillAmount = 0f;
                }
            }
        }
    }

    public void UpdateProgressBar(int index, float fillAmount)
    {
        // Check if the index is within bounds of the progressBars array
        if (index >= 0 && index < progressBars.Length)
        {
            // Update the fill amount of the progress bar at the specified index
            progressBars[index].fillAmount = fillAmount;
        }
        else
        {
            // Log an error message if the index is out of bounds
            Debug.LogError($"Index {index} is out of bounds. Valid range is 0 to {progressBars.Length - 1}.");
        }
    }

    public void ActivateScript()
    {
        if (Input.GetKeyDown(KeyCode.P) && gameplayScript.enabled == false)
        {
            gameplayScript.enabled = true;
            Debug.Log("enabling script");
        }
    }

}