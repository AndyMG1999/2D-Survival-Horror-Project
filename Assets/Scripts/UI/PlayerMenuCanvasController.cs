using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMenuCanvasController : MonoBehaviour
{
    public static PlayerMenuCanvasController Instance {get; private set;}

    [Header("Player Menu & Status Reference")]
    public GameObject playerMenuGameObject;
    public GameObject playerMenuPanelGameObject;
    public GameObject statusGameObject;
    public GameObject mapMenuGameObject;
    public GameObject itemsMenuGameObject;
    public GameObject notesMenuGameObject;

    [Header("Player Menu Buttons")]
    public Button itemButton;
    public TextMeshProUGUI mapButtonText;
    public TextMeshProUGUI itemsButtonText;
    public TextMeshProUGUI notesButtonText;
    public Color buttonSelectedColor;
    public Color buttonNotSelectedColor;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        // Sets UI to disabled after setting instance
        gameObject.SetActive(false);
    }
    
    public void OpenMapMenu()
    {
        gameObject.SetActive(true);
        statusGameObject.SetActive(true);
        playerMenuPanelGameObject.SetActive(true);
        
        mapMenuGameObject.SetActive(true);
        mapButtonText.color = buttonSelectedColor;
        itemsMenuGameObject.SetActive(false);
        itemsButtonText.color = buttonNotSelectedColor;
        notesMenuGameObject.SetActive(false);
        notesButtonText.color = buttonNotSelectedColor;
        
        playerMenuGameObject.SetActive(true);
    }

    public void OpenItemsMenu()
    {
        gameObject.SetActive(true);
        statusGameObject.SetActive(true);
        playerMenuPanelGameObject.SetActive(true);
        
        mapMenuGameObject.SetActive(false);
        mapButtonText.color = buttonNotSelectedColor;
        itemsMenuGameObject.SetActive(true);
        itemsButtonText.color = buttonSelectedColor;
        notesMenuGameObject.SetActive(false);
        notesButtonText.color = buttonNotSelectedColor;
        
        playerMenuGameObject.SetActive(true);
        // Sets Item Button as selected so controller users can navigate without a mouse
        EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
    }

    public void OpenNotesMenu()
    {
        gameObject.SetActive(true);
        statusGameObject.SetActive(true);
        playerMenuPanelGameObject.SetActive(true);

        mapMenuGameObject.SetActive(false);
        mapButtonText.color = buttonNotSelectedColor;
        itemsMenuGameObject.SetActive(false);
        itemsButtonText.color = buttonNotSelectedColor;
        notesMenuGameObject.SetActive(true);
        notesButtonText.color = buttonSelectedColor;

        playerMenuGameObject.SetActive(true);
    }

    public void ClosePlayerMenu()
    {
        gameObject.SetActive(false);
        statusGameObject.SetActive(false);
        playerMenuPanelGameObject.SetActive(false);
        playerMenuGameObject.SetActive(false);
        
        mapMenuGameObject.SetActive(false);
        mapButtonText.color = buttonNotSelectedColor;
        itemsMenuGameObject.SetActive(false);
        itemsButtonText.color = buttonNotSelectedColor;
        notesMenuGameObject.SetActive(false);
        notesButtonText.color = buttonNotSelectedColor;
    }
}
