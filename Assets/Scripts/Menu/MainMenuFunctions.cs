using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    public void ToCreateLoad()
    {
        SceneManager.LoadScene("CreateLoad", LoadSceneMode.Single);
    }

    public void ToCharacterCreationButton()
    {
        SceneManager.LoadScene("CharacterCreation", LoadSceneMode.Single);
    }

    public void ToCharacterLoadButton()
    {
        SceneManager.LoadScene("CharacterLoad", LoadSceneMode.Single);
    }

    public void ToWorldCreateButton()
    {
        SceneManager.LoadScene("WorldCreate", LoadSceneMode.Single);
    }
    public void ToWorldLoadButton()
    {
        SceneManager.LoadScene("WorldLoad", LoadSceneMode.Single);
    }
    public void ToLittleHeightsWorldView()
    {
        SceneManager.LoadScene("LittleHeightsWorldView", LoadSceneMode.Single);
    }
}
