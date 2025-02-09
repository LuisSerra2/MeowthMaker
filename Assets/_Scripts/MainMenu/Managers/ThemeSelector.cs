using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelector : MonoBehaviour
{
    public void SelectTheme(int themeIndex)
    {
        if (ThemeManager.Instance != null)
        {
            ThemeManager.Instance.SetTheme(themeIndex);
        }
    }
}
