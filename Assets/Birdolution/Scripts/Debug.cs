using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Debug : MonoBehaviour
{
    int currentFPS;

    [SerializeField] private TextMeshProUGUI fpsText;

    [SerializeField] private Button fpsToggleButton;

    private float textRefreshRate = 0.1f;
    private float timer = 0;

    private bool fpsLocked = true;

    private void Start()
    {
        Application.targetFrameRate = 30;

        fpsToggleButton.onClick.AddListener(ToggleFPS);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= textRefreshRate)
        {
            currentFPS = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + currentFPS.ToString();
            timer = 0;
        }
    }

    public void ToggleFPS()
    {
        if (fpsLocked)
            Application.targetFrameRate = -1;
        else Application.targetFrameRate = 30;

        fpsLocked = !fpsLocked;
    }
}
