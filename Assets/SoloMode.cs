using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SoloMode : MonoBehaviour
{
    public GameObject prefabImage;
    public Canvas myCanvas;
    private GameObject[] gameObjectImages;
    public Sprite[] animalsImages;
    public String[] animalsNames;
    private int numberOfImages;
    public GridLayoutGroup grid;
    IDictionary<Sprite, String> myAnimals = new Dictionary<Sprite, String>();
    // Start is called before the first frame update
    public void CreateGrid()
    {
        numberOfImages = GridThemeSolo.scaleGrid * GridThemeSolo.scaleGrid;
        for (int i = 0; i < numberOfImages; i++)
        {
            {
                switch(GridThemeSolo.themeNumber) //ImagesDependOnThemeChoosed
                {
                    case 1:  //ThemeIsAnimal
                    prefabImage.GetComponent<Image>().sprite = animalsImages[i];
                    break;   
                }
                GameObject image = Instantiate(prefabImage, transform.parent);
                image.transform.SetParent(myCanvas.transform);
                image.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            }
        }
        gameObjectImages = GameObject.FindGameObjectsWithTag("Image");
        switch (GridThemeSolo.scaleGrid) //ImagesSizeDependOnScalingChoosed
        {
            case 3:  //SizeIs 3x3
                grid.cellSize = new Vector2(300, 300);
                break;
            case 4: //SizeIs 4x4
                grid.cellSize = new Vector2(240, 240);
                break;
            case 5: //SizeIs 4x4
                grid.cellSize = new Vector2(180, 180);
                break;
        }
    }

    public void RotateGrid()
    {
        foreach (GameObject images in gameObjectImages)
        {
            images.transform.Rotate(0f, 90f, 0f);
            images.GetComponent<Image>().enabled = false;
            images.transform.Rotate(0f, 90f, 0f);
            images.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            switch (GridThemeSolo.scaleGrid) //ImagesSizeDependOnScalingChoosed
            {
                case 3:  //SizeIs 3x3
                    images.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    break;
                case 4: //SizeIs 4x4
                    images.transform.GetChild(0).localScale = new Vector3(0.85f, 0.85f, 0.85f);
                    break;
                case 5: //SizeIs 4x4
                    images.transform.GetChild(0).localScale = new Vector3(0.70f, 0.70f, 0.70f);
                    break;
            }
        }
    }
    public void DestroyGrid()
    {
        foreach (GameObject images in gameObjectImages)
        {
            Destroy(images);
        }
    }

    public void DictionnaryAnimals()
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            {
                myAnimals.Add(animalsImages[i], animalsNames[i]);
            }
        }
        Debug.Log(myAnimals[animalsImages[4]]);
    }
    public void DestroyDictionnary()
    {
        myAnimals.Clear();
    }

    public void ShuffleImages()
    {
        DestroyGrid();
        DestroyDictionnary();
        for (int i = 0; i < numberOfImages; i++)
        {
            string temp1 = animalsNames[i];
            Sprite temp2 = animalsImages[i];
            int randomIndex = Random.Range(i, numberOfImages);
            animalsNames[i] = animalsNames[randomIndex];
            animalsImages[i] = animalsImages[randomIndex];
            animalsNames[randomIndex] = temp1;
            animalsImages[randomIndex] = temp2;
        }
        CreateGrid();
        DictionnaryAnimals();
    }
}
