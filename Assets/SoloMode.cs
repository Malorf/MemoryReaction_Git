using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

public class SoloMode : MonoBehaviour
{
    public GameObject prefabImage;
    public Canvas myCanvas;
    public GameObject[] gameObjectImages;
    public Sprite[] animalsImages;
    public string[] animalsNames;
    private int numberOfImages;
    public GridLayoutGroup grid;
    IDictionary<Sprite, String> myAnimals = new Dictionary<Sprite, String>();
    IList<string> myListName = new List<string>();
    public TextMeshProUGUI nameOfImageToFind;
    private int randomName;
    public void CreateGrid()
    {
        numberOfImages = GridThemeSolo.scaleGrid * GridThemeSolo.scaleGrid;
        for (int i = 0; i < numberOfImages; i++)
        {
            {
                switch(GridThemeSolo.themeNumber) //ImagesDependOnThemeChoosed
                {
                    case 1:  //ThemeIsAnimal
                    prefabImage.GetComponent<Image>().sprite = animalsImages[i]; //ImagesAreAnimals
                        break;   
                }
                GameObject image = Instantiate(prefabImage, transform.parent);
                image.transform.SetParent(myCanvas.transform);
                image.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(OnClickImage);
            }
        }
        gameObjectImages = GameObject.FindGameObjectsWithTag("Image"); // stocking every instantiate items
        switch (GridThemeSolo.scaleGrid) //ImagesSizeDependOnScalingChoosed
        {
            case 3:  //GridSizeIs 3x3
                grid.cellSize = new Vector2(300, 300);
                break;
            case 4: //GridSizeIs 4x4
                grid.cellSize = new Vector2(240, 240);
                break;
            case 5: //GridSizeIs 5x5
                grid.cellSize = new Vector2(180, 180);
                break;
        }
    }

    public void RotateGrid()
    {
        foreach (GameObject goImages in gameObjectImages)
        {
            if (goImages != null) //PreventMinorIssueOn"gameObjectImages"
            {
                Animator images = goImages.GetComponent<Animator>(); 
                images.SetBool("Rotate90°", true); //ImagesFlipAt90° All bricks button are up
                switch (GridThemeSolo.scaleGrid) //ImagesSizeDependOnScalingChoosed
                {
                    case 3:  //SizeIs 3x3
                        goImages.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f, 1.1f);
                        break;
                    case 4: //SizeIs 4x4
                        goImages.transform.GetChild(0).localScale = new Vector3(0.85f, 0.85f, 0.85f);
                        break;
                    case 5: //SizeIs 4x4
                        goImages.transform.GetChild(0).localScale = new Vector3(0.70f, 0.70f, 0.70f);
                        break;
                }
            }
        }
    }
    public void DestroyGrid()
    {
        foreach (GameObject goImages in gameObjectImages)
        {
            Destroy(goImages);
        }
    }

    public void DictionnaryAnimals() //ImageAssociatedWithName
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            {
                myAnimals.Add(animalsImages[i], animalsNames[i]);
            }
        }
    }
    public void DestroyDictionnary()
    {
        myAnimals.Clear();
    }

    public void ShuffleImages() //StartTheGame
    {
        DestroyGrid();
        DestroyDictionnary();
        myListName.Clear();
        for (int i = 0; i < numberOfImages; i++) //Shuffle
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
        listNames();
        NameApparition();  //temporary (it will be replaced by an other function which leave some time to memorize)
    }
    public void listNames()
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            myListName.Add(animalsNames[i]);
        }
    }
    public void NameApparition() //ARandomNameAppeared
    {
        randomName = Random.Range(0, myListName.Count);
        Debug.Log(randomName);
        nameOfImageToFind.text = myListName[randomName];
    }
    void OnClickImage()
    {
        ButtonPrefab.prefab.GetComponent<Animator>().SetBool("Rotate90°", false);
        Debug.Log("imageName : " + myAnimals[ButtonPrefab.mySpritePrefabImage] + "  name :" + animalsNames[randomName]);
        if (myAnimals[ButtonPrefab.mySpritePrefabImage] == myListName[randomName])
        {
            Debug.Log("good");
            myAnimals.Remove(ButtonPrefab.mySpritePrefabImage); //NEED to remove or clear the animalsName 
            myListName.Remove(myListName[randomName]);
            NameApparition();
        }
        else
        {
            StartCoroutine(DelayBeforeReturninBrick());
            Debug.Log("bad");
        }
    }
    IEnumerator DelayBeforeReturninBrick()
    {
        yield return new WaitForSeconds(1f);
        ButtonPrefab.prefab.GetComponent<Animator>().SetBool("Rotate90°", true);
    }
}
