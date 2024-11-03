using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TypingGame : MonoBehaviour
{
    private string input;
    [SerializeField] List<string> names;
    private string goal_name;
    private MinigameScript my_minigame;
    [SerializeField] TextMeshProUGUI input_text;
    [SerializeField] TextMeshProUGUI disease_name;
    private bool game_over;
    [SerializeField] TMP_InputField input_field;

    private void Start()
    {
        //intiialize the word that needs to be types
        input = input_text.text;
        goal_name = names[Random.Range(0, names.Count)];
        disease_name.text = goal_name;
        my_minigame = GetComponent<MinigameScript>();
        game_over = false;
        input_field.ActivateInputField();
    }

    private void Update()
    {
        input = input_text.text.Substring(0, input_text.text.Length-1);
        if(input == goal_name && !game_over)
        {
            my_minigame.EndMinigame(0);
            game_over = true;
        }
    }
}
