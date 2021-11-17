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
    [SerializeField]
    Button CharacterLoadButton;

    //todo: remove this
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
        if (CharacterLoadButton != null)
        {
            CharacterLoadButton.onClick.AddListener(ToCharacterLoadButton);
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
}
