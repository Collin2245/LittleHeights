using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField]
    Button CreateLoadButton;
    [SerializeField]
    Button CharacterCreationButton;
    public void Start()
    {
        if (CreateLoadButton != null)
        {
            CreateLoadButton.onClick.AddListener(ToCreateLoad);
        }
        if (CharacterCreationButton != null)
        {
            CharacterCreationButton.onClick.AddListener(ToCharacterCreationButton);
        }

    }
    public void ToCreateLoad()
    {
        SceneManager.LoadScene("CreateLoad", LoadSceneMode.Single);
    }

    public void ToCharacterCreationButton()
    {
        SceneManager.LoadScene("CharacterCreation", LoadSceneMode.Single);
    }

}
