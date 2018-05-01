using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{

    public GameObject puffCoins;

    public void OnCoinClicked()
    {
        puffCoins.transform.position = gameObject.transform.position;
        Instantiate(puffCoins);


        GameController.instance.CollectCoin();

        HUDManager.instance.NewNotification("Coin collected: " + GameController.instance.coins + "/" + GameController.instance.coinsInScene);
        Destroy(gameObject, 0.2f);
    }

}
