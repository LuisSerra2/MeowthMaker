using UnityEngine;
using UnityEngine.UI;

public class UpdateThemeSprites : MonoBehaviour
{
    public string spriteTag; 
    private SpriteRenderer spriteRenderer;
    private Image image;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    public void ApplyTheme(Sprite newSprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = newSprite;
        } else if (image != null)
        {
            image.sprite = newSprite;
            image.SetNativeSize();
        }
    }
}
