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



    //all the levels in which each brick is used. there is a shift because of an index error in the steps
    int[] Brick4Array = new int[] { 34, 35, 46, 47, 58, 59, 72, 82};
    int[] BrickRoofArray = new int[] { 86, 87, 88, 89, 92, 93, 94, 95, 98, 99, 100, 101, 103, 104, 105, 106, 108, 109, 110, 111, 112, 113, 114, 115, 116};//added 116 so last image will not change
    public Sprite[] BrickImages;
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

    private void activatesteps()
    {
        //this will activate only the current step
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
        if (checkArray(counter-1, Brick4Array))
        {
            imageofbrick.sprite = BrickImages[0];
        }else if (checkArray(counter-1, BrickRoofArray))
        {
            imageofbrick.sprite = BrickImages[2];
        }
        else
            imageofbrick.sprite = BrickImages[1];
    }

    //checks if our counter is in a specific level array
    private bool checkArray(int num, int[] array)
    {
        bool flag = false;
        for (int i = 0; i < array.Length; i++)
        {
            if (num == array[i])
                flag = true;
        }

        return flag;
    }
}
