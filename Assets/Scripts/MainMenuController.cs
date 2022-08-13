using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;
    public Label CurrentScore;
    public Label BestScore;
    public VisualElement MainMenuVE;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Time.timeScale = 0;

        StartButton = root.Q<Button>("StartButton");
        QuitButton = root.Q<Button>("QuitButton");
        CurrentScore = root.Q<Label>("CurrentScore");
        BestScore = root.Q<Label>("BestScore");
        MainMenuVE = root.Q<VisualElement>("MainMenuVE");

        StartButton.clicked += StartButtonPressed;
        QuitButton.clicked += QuitButtonPressed;

        CurrentScore.text = string.Format("Previous Score : {0:f0}", PlayerPrefs.GetFloat("Score"));
        BestScore.text = string.Format("Best Score : {0:f0}", PlayerPrefs.GetFloat("BestScore"));
    }

    void StartButtonPressed()
    {
        MainMenuVE.visible = false;
        Time.timeScale = 1;
    }
    void QuitButtonPressed()
    {
        Application.Quit();
    }
}
