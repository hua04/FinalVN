using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    // Define the count variable
    private int holdCount = 0;

    // Store the current key being held down
    public KeyCode currentKey = KeyCode.None;

    private float timer = 0f;
    public float slowCount = 0.5f;
    public float targetTime = 2f;
    private float elapsedTime = 0f;

    public Image progressBar;

    public GameplayScript gameplayScript;

    void Update()
    {
        // Variable to check if a different key is pressed
        bool keyChanged = false;

        // Loop through all possible key codes
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            // Check if this key is being pressed
            if (Input.GetKey(key))
            {
                // If the key is different from the current key
                if (key != currentKey)
                {
                    // Set current key to the new key
                    currentKey = key;
                    // Reset the count since a different key is being pressed
                    holdCount = 0;
                    keyChanged = true;

                    // Reset timers
                    timer = 0f;
                    elapsedTime = 0f;
                }

                // Handle hold count
                timer += Time.deltaTime;
                if (timer >= slowCount)
                {
                    holdCount++;
                    timer = 0f;
                }

                // Increment elapsed time
                elapsedTime += Time.deltaTime;

                // Calculate the fill amount based on elapsed time and target time
                float fillAmount = elapsedTime / targetTime;

                // Clamp the fill amount between 0 and 1
                fillAmount = Mathf.Clamp01(fillAmount);

                // Update progress bar fill amount
                if (progressBar != null)
                {
                    progressBar.fillAmount = fillAmount;
                }

                // Check if the progress bar is full
                if (fillAmount >= 1f)
                {
                    Debug.Log("Progress bar is full!");
                    gameplayScript.PressComplete();

                    // Reset elapsed time
                    elapsedTime = 0f;
                }

                break; // Exit the loop once we find a pressed key
            }
        }

        // If no key is being pressed, reset the current key and timers
        if (!keyChanged && !Input.anyKey)
        {
            currentKey = KeyCode.None;
            timer = 0f;
            elapsedTime = 0f;
        }

        // Output the current hold count to the console (optional)
        Debug.Log($"Key: {currentKey}, Hold count: {holdCount}");
    }



}

