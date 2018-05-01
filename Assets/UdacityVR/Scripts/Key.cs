using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    public GameObject puffEffect;
    public Door door;

	public void OnKeyClicked()
	{
        puffEffect.transform.position = gameObject.transform.position;
        Instantiate(puffEffect);

        door.Unlock();
        GameController.instance.CollectKey();

        HUDManager.instance.NewNotification("Key collected");
        Destroy(gameObject, 0.2f);
    }

}
