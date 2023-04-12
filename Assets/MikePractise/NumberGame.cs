using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberGame : MonoBehaviour
{
    public TMP_Text secretNumberText;
    public TMP_InputField guessInput;
    public TMP_Text resultText;

    private int secretNum;
    private int randomNum;
    private int guessCount;

    void Start()
    {
        
            // Generate a random number
            secretNum = Random.Range(1, 101);

            // Set the text of the SecretNumberText object
            secretNumberText.text = "Secret Number: " + secretNum;

            guessCount = 0;
            randomNum = Random.Range(1, 101);
        
    }

    //public void CheckGuess()
    //{
    //    int guess;

    //    if (int.TryParse(guessInput.text, out guess))
    //    {
    //        guessCount++;

    //        if (guess == randomNum)
    //        {
    //            resultText.text = "You guessed it! It took you " + guessCount + " tries.";
    //        }
    //        else if (guess < randomNum)
    //        {
    //            resultText.text = "Too low! Try again.";
    //        }
    //        else
    //        {
    //            resultText.text = "Too high! Try again.";
    //        }

    //        guessInput.text = "";
    //        guessCount++;
    //    }
    //}
    public void CheckGuess()
    {
        int guess = 0;

        string[] guessDigits = guessInput.text.Split('\n');

        for (int i = 0; i < guessDigits.Length; i++)
        {
            if (!string.IsNullOrEmpty(guessDigits[i]))
            {
                guess += int.Parse(guessDigits[i]);
                guess *= 10;
            }
        }

        guess /= 10;

        guessCount++;

        if (guess == randomNum)
        {
            resultText.text = "You guessed it! It took you " + guessCount + " tries.";
        }
        else if (guess < randomNum)
        {
            resultText.text = "Too low! Try again.";
        }
        else
        {
            resultText.text = "Too high! Try again.";
        }

        guessInput.text = "";
    }
}
