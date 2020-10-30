using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VuforiaMain : MonoBehaviour
{
    public Text steptext;
    public Material wireframeMaterial;
    public Button rightBtn;
    public Button leftBtn;
    public Vector3 scaleFactor; //this scale factor will help change the house size easily
    private int counter = 1;

    public GameObject[] arObjectsToPlace;//this has all the steps
    public GameObject LegoHouse; //we use this object to influence all the bricks together

    public Image imageofbrick;

    void Start()
    {
        changeMaterial();
        LegoHouse.transform.localScale = scaleFactor;
        rightBtn.onClick.AddListener(rightfunc);
        leftBtn.onClick.AddListener(leftfunc);


        // disable all game objects
        activatesteps();
        changeimages();
        indicatestep();


    }

    //changes the text that shows the level
    private void indicatestep()
    {
        if (counter <= arObjectsToPlace.Length)//if it's bigger then we dont want it to update
            steptext.text = "Step: " + counter + "/" + arObjectsToPlace.Length;
    }

    private void changeMaterial()
    {
        Renderer[] legoMaterials = LegoHouse.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in legoMaterials)
        {
            r.material = wireframeMaterial;
        }
    }

    private void leftfunc()
    {
        if (counter > 1)
            counter = counter - 1;
        activatesteps();
        changeimages();
        indicatestep();

    }

    private void rightfunc()
    {
        counter = counter + 1;

        if (counter == arObjectsToPlace.Length + 1)//we finished all the levels
        {
            finishFunc();
        }
        activatesteps();
        changeimages();
        indicatestep();
    }

    private void finishFunc()
    {
        SceneManager.LoadScene("End");
    }


    //this will activate only the current step
    private void activatesteps()
    {
        for (int i = 0; i < arObjectsToPlace.Length; i++)
        {
            arObjectsToPlace[i].SetActive(false);
            if (i == counter-1)//it is minus 1 because of array index
                arObjectsToPlace[i].SetActive(true);

        }
    }

    //changes the brick image depending on the level
    private void changeimages()
    {

        if (arObjectsToPlace[counter-1].GetComponent<ShowImage>().img != null)
        {
            imageofbrick.sprite = arObjectsToPlace[counter - 1].GetComponent<ShowImage>().img;
        }
        
    }

}
