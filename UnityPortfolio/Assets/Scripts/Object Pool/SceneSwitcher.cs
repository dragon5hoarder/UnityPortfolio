using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

     public bool firstLevel;

	public void SwitchScene() {
          if (firstLevel)
          {
               int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
               SceneManager.LoadScene(nextLevel);
          }
          else
          {
               int nextLevel = SceneManager.GetActiveScene().buildIndex - 1;
               SceneManager.LoadScene(nextLevel);
          }
     }
}
