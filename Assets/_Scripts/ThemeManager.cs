using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance;

    private const string ThemePrefKey = "SelectedTheme";

    [Header("Theme Settings")]
    public List<ThemeAssets> themes; 
    public int currentThemeIndex = 0; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadTheme();
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ApplyTheme();
    }

    public void SetTheme(int themeIndex)
    {
        if (themeIndex < 0 || themeIndex >= themes.Count)
        {
            Debug.LogError("Invalid theme index.");
            return;
        }

        currentThemeIndex = themeIndex;
        ApplyTheme();
        SaveTheme();
    }

    public void ApplyTheme()
    {
        if (themes.Count == 0)
        {
            Debug.LogError("No themes available.");
            return;
        }

        ThemeAssets assets = themes[currentThemeIndex];

        UpdateSprites("Background", assets.Background);
        UpdateSprites("BackgroundAnimal", assets.animalBackground);
        UpdateSprites("Button", assets.Buttons);
        UpdateSprites("PanelBackground", assets.PanelBackground);
        UpdateSprites("Exit", assets.ExitButtons);
        UpdateSprites("Tier1", assets.tierAnimal[0]);
        UpdateSprites("Tier2", assets.tierAnimal[1]);
        UpdateSprites("Tier3", assets.tierAnimal[2]);
        UpdateSprites("Tier4", assets.tierAnimal[3]);
        UpdateSprites("Tier5", assets.tierAnimal[4]);
        UpdateSprites("Tier6", assets.tierAnimal[5]);
        UpdateSprites("Tier7", assets.tierAnimal[6]);
        UpdateSprites("Tier8", assets.tierAnimal[7]);
        UpdateSprites("Tier9", assets.tierAnimal[8]);
        UpdateSprites("Tier10", assets.tierAnimal[9]);
    }

    private void UpdateSprites(string tag, Sprite sprite)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        if (objects == null) return;

        foreach (GameObject obj in objects)
        {
            if (obj.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = sprite;
            } else if (obj.TryGetComponent(out Image image))
            {
                image.sprite = sprite;
                image.SetNativeSize();
            }
        }
    }

    public Sprite GetSpriteForTag(string tag)
    {
        if (themes.Count == 0 || currentThemeIndex < 0 || currentThemeIndex >= themes.Count)
        {
            Debug.LogError("Theme is not properly set.");
            return null;
        }

        ThemeAssets assets = themes[currentThemeIndex];

        return tag switch
        {
            "Background" => assets.Background,
            "BackgroundAnimal" => assets.animalBackground,
            "Button" => assets.Buttons,
            "PanelBackground" => assets.PanelBackground,
            "Exit" => assets.ExitButtons,
            "Tier1" => assets.tierAnimal[0],
            "Tier2" => assets.tierAnimal[1],
            "Tier3" => assets.tierAnimal[2],
            "Tier4" => assets.tierAnimal[3],
            "Tier5" => assets.tierAnimal[4],
            "Tier6" => assets.tierAnimal[5],
            "Tier7" => assets.tierAnimal[6],
            "Tier8" => assets.tierAnimal[7],
            "Tier9" => assets.tierAnimal[8],
            "Tier10" => assets.tierAnimal[9],
            _ => null,
        };
    }


    private void SaveTheme()
    {
        PlayerPrefs.SetInt(ThemePrefKey, currentThemeIndex);
        PlayerPrefs.Save();
    }

    private void LoadTheme()
    {
        if (PlayerPrefs.HasKey(ThemePrefKey))
        {
            currentThemeIndex = PlayerPrefs.GetInt(ThemePrefKey);
        } else
        {
            currentThemeIndex = 0; 
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyTheme();
    }
}


[System.Serializable]
public class ThemeAssets
{
    public string themeName;
    public Sprite Background;
    public Sprite animalBackground;
    public Sprite Buttons;
    public Sprite PanelBackground;
    public Sprite ExitButtons;
    public Sprite[] tierAnimal;
}

