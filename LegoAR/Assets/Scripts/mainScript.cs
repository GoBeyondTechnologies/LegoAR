using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARTrackedImageManager))]
public class mainScript : MonoBehaviour
{
    //all the levels in which each brick is used
    int[] Brick4Array = new int[] { 11, 23, 24, 25, 26, 27, 28, 29, 30, 34, 51, 61, 65, 66, 67, 68, 69, 74};
    int[] Brick6Array = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 31, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 54, 55, 56, 57, 62, 63, 78, 82};
    int[] Brick8Array = new int[] {32, 33, 34, 35, 36, 37, 38, 39, 40, 58, 59, 60, 64, 72, 73, 77, 81, 85, 86};
    int[] Brick12Array = new int[] {52};
    int[] Brick14Array = new int[] {53};
    int[] BrickRoofArray = new int[] {70, 71, 75, 76, 79, 80, 83, 84, 87, 88, 89, 90, 91, 92 ,93, 94, 95, 96, 97, 98};
    public Sprite[] BrickImages;
    public Image imageofbrick;

    public Button rightBtn;
    public Button leftBtn;

    private int counter = 5;

    //public Canvas finishcanvas;
    public Text imagefoundtxt;

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    [SerializeField]
    private GameObject fatherObject; //we use this object to center the position of all the children

    [SerializeField]
    private Vector3 scaleFactor;// = new Vector3(1f, 1f, 1f);
    [SerializeField]
    private Vector3 align_positioning_X;
    [SerializeField]
    private Vector3 align_positioning_Z;
    [SerializeField]
    private Vector3 tempSumOfPosition;

    private ARTrackedImageManager m_TrackedImageManager;
    private ARTrackedImage trackedImage;

    private ARSessionOrigin m_SessionOrigin;



    private void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        //finishcanvas.enabled = false;

        m_SessionOrigin = GetComponent<ARSessionOrigin>();

        rightBtn.onClick.AddListener(rightfunc);
        leftBtn.onClick.AddListener(leftfunc);
        //fatherObject.transform.position += tempSumOfPosition;


        // setup all game objects in dictionary
        foreach (GameObject arObject in arObjectsToPlace)
        {
            //GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            //newARObject.name = arObject.name;
            //arObjects.Add(arObject.name, newARObject);
            arObject.SetActive(false);

        }

    }


    private void leftfunc()
    {
       if (counter > 1)
        counter = counter - 1;
        //tempSumOfPosition += align_positioning_X;
        //changeimages();
    }

    private void rightfunc()
    {
        counter = counter + 1;
        //tempSumOfPosition += align_positioning_Z;

        //finished everything
        if (counter == arObjectsToPlace.Length - 1)//change 25 to arObjectsToPlace.Length
        {
            //finishcanvas.enabled = true;
            finishFunc();
        }

        //changeimages();
    }

    private void finishFunc()
    {
        SceneManager.LoadScene("End");
    }



    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            //m_SessionOrigin.MakeContentAppearAt(fatherObject.transform, trackedImage.transform.position + tempSumOfPosition, trackedImage.transform.localRotation);
            m_SessionOrigin.MakeContentAppearAt(fatherObject.transform, trackedImage.transform.position, trackedImage.transform.localRotation);

            for (int i = 0; i < counter; i++)
            {
                arObjectsToPlace[i].SetActive(true);
                fatherObject.transform.position = trackedImage.transform.localPosition + tempSumOfPosition;
                fatherObject.transform.localScale = scaleFactor;
                fatherObject.transform.rotation = trackedImage.transform.rotation;
            }
            for (int i = counter; i < arObjectsToPlace.Length; i++)
            {
                arObjectsToPlace[i].SetActive(false);
            }
            imagefoundtxt.text = "Image Found";

        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //m_SessionOrigin.MakeContentAppearAt(fatherObject.transform, trackedImage.transform.position + tempSumOfPosition, trackedImage.transform.localRotation);

            m_SessionOrigin.MakeContentAppearAt(fatherObject.transform, trackedImage.transform.position, trackedImage.transform.localRotation);

            for (int i = 0; i < counter; i++)
            {
                arObjectsToPlace[i].SetActive(true);
                fatherObject.transform.position = trackedImage.transform.localPosition + tempSumOfPosition;
                fatherObject.transform.localScale = scaleFactor;
            }
            for (int i = counter; i < arObjectsToPlace.Length; i++)
            {
                arObjectsToPlace[i].SetActive(false);
            }
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            for (int i = 0; i < arObjectsToPlace.Length; i++)
            {
                arObjectsToPlace[i].SetActive(false);
            }
            imagefoundtxt.text = "Image Removed";

        }


        //changeimages();
    }

    //changes the brick image depending on the level
    private void changeimages()
    {
        if (checkArray(counter, Brick4Array))
        {
            imageofbrick.sprite = BrickImages[0];
            //return;
        }
        if (checkArray(counter, Brick6Array))
        {
            imageofbrick.sprite = BrickImages[1];
            //return;
        }
        if (checkArray(counter, Brick8Array))
        {
            imageofbrick.sprite = BrickImages[2];
            //return;
        }
        if (checkArray(counter, Brick12Array))
        {
            imageofbrick.sprite = BrickImages[3];
            //return;
        }
        if (checkArray(counter, Brick14Array))
        {
            imageofbrick.sprite = BrickImages[4];
            //return;
        }
        if (checkArray(counter, BrickRoofArray))
        {
            imageofbrick.sprite = BrickImages[5];
            //return;
        }
    }

    //checks if our counter is in a specific level array
    private bool checkArray (int num, int[] array)
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

