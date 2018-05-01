using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public static HUDManager instance = null;

    public bool isActive = false;
    public float timeLimit = 5f;
    public float timeActivated = 0f;

    private Text hudText;

    void Awake()
    {
        if (instance == null)
            instance = this;

    }

    private void Start()
    {
        timeActivated = timeLimit;
        hudText = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update () {
        if (isActive)
        {
            timeActivated -= Time.deltaTime;
            if(timeActivated <= 0)
            {
                isActive = false;
            }
        }
        if (!isActive) {
            timeActivated = timeLimit;
            hudText.text = "";
        }
	}

    public void NewNotification(string text)
    {
        hudText.text = text;
        isActive = true;
    }
}
