using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

     public int nextLevel;
     public Button myButton;

     private void Awake()
     {
          if (SceneManager.GetActiveScene().buildIndex == nextLevel)
               myButton.interactable = false;
     }

     public void SwitchScene()
     {
               SceneManager.LoadScene(nextLevel);
     }
}
