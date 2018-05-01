using UnityEngine;
using UnityEngine.VR;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public int coinsInScene = 5;

    public GameObject banner;

    void Awake()
    {
        if (instance == null)
            instance = this;
        
    }

    public bool keyCollected = false;
    public int coins;

    private void Start()
    {
        HUDManager.instance.NewNotification("Hi!, the goal is the temple");
        banner.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void CollectKey()
    {
        keyCollected = true;
    }

    public void CollectCoin()
    {
        coins++;
    }

    public void CheckFinish()
    {
        if(coins == coinsInScene)
        {
            HUDManager.instance.NewNotification("Go to the Temple!.");
            if (!banner.activeSelf)
            {
                banner.SetActive(true);
            }
        } else
        {
            HUDManager.instance.NewNotification("Please, collect the other coins");
        }
    }
}
