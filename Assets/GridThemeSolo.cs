using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridThemeSolo : MonoBehaviour
{
    public TextMeshProUGUI theme1;
    public TextMeshProUGUI theme2;
    public TextMeshProUGUI grid;
    public TextMeshProUGUI back;
    public int scaleGrid = 4;
    public int themeNumber = 1;
    public void TextUpdate()
    {
        if (themeNumber == 1)
        {
            theme1.enabled = true;
            theme2.enabled = false;
        }
        if (themeNumber == 2)
        {
            theme2.enabled = true;
            theme1.enabled = false;
        }
        if (Langage.french == true)
        {
            back.text = "Retour";
            theme1.text = "Animaux";
            theme2.text = "Aliments";
            grid.text = scaleGrid + "x" + scaleGrid;
        }
        else
        {
            back.text = "Back";
            theme1.text = "Animals";
            theme2.text = "Food";
            grid.text = scaleGrid + "x" + scaleGrid;
        }
    }
    public void UpGrid()
    {
        if (scaleGrid < 5)
        {
            scaleGrid += 1;
            TextUpdate();
        }
    }

    public void DownGrid()
    {
        if (scaleGrid > 3)
        {
            scaleGrid -= 1;
            TextUpdate();
        }
    }

    public void UpTheme()
    {
        if (themeNumber < 2)
        {
            themeNumber += 1;
            TextUpdate();
        }
    }

    public void DownTheme()
    {
        if (themeNumber > 1)
        {
            themeNumber -= 1;
            TextUpdate();
        }
    }
}
