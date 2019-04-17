using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    //opening scene to play
    public void PlayGame() => SceneManager.LoadScene("TileCreation");
    
    //quiting game
    public void QuitGame() => Application.Quit();
}
