using UnityEngine;
using UnityEngine.SceneManagement;

public class SignPost : MonoBehaviour
{	
	public void ResetScene() 
	{
        // Reset the scene when the user clicks the sign post
        SceneManager.LoadScene(0);
	}
}