using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RNGImage : MonoBehaviour
{
    public Texture[] animalsImages;
    public String[] animalsNames;
    IDictionary<Texture, String> myAnimals = new Dictionary<Texture, String>();
    // Start is called before the first frame update
    public void DictionnaryAnimals()
    {
        var numberOfImages = GridThemeSolo.scaleGrid * GridThemeSolo.scaleGrid;
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
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
