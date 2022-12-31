using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloMode : MonoBehaviour
{
    public GameObject PrefabImage;
    public Canvas myCanvas;
    // Start is called before the first frame update
    public void CreateGrid()
    {
        var numberOfImages = GridThemeSolo.scaleGrid * GridThemeSolo.scaleGrid;
        for (int i = 0; i < numberOfImages ; i++)
        {
            GameObject image = Instantiate(PrefabImage, transform.parent);
            image.transform.SetParent(myCanvas.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
