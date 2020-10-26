using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class oldmainScript : MonoBehaviour
{
    public Canvas finishcanvas;

    public Button rightBtn;
    public Button leftBtn;

    public int num_of_steps = 32;

    private int counter = 5;

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    [SerializeField]
    private GameObject fatherObjectCenterPoint; //we use this object to center the position of all the children

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


    private void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        //rend = GetComponent<Renderer>();
    }

    void Start()
    {

        finishcanvas.enabled = false;

        tempSumOfPosition = align_positioning_Z + align_positioning_X;
        rightBtn.onClick.AddListener(rightfunc);
        leftBtn.onClick.AddListener(leftfunc);


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
        counter = (counter - 1) % num_of_steps;
        if (counter <= 0) {
            counter = 0;
        }
        // fatherObjectCenterPoint.transform.rotation *= Quaternion.Euler(0, 10, 0);
        // tempSumOfPosition += align_positioning_X;

    }

    private void rightfunc()
    {
        counter = (counter + 1) % num_of_steps;
        // arObjectsToPlace[1];
        // tempSumOfPosition += align_positioning_Z;

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
            for (int i = 0; i < counter; i++)
            {
                arObjectsToPlace[i].SetActive(true);
                fatherObjectCenterPoint.transform.position = trackedImage.transform.localPosition + tempSumOfPosition;
                //arObjectsToPlace[i].transform.position = trackedImage.transform.position; //doesnt work properly
                //arObjectsToPlace[i].transform.localScale = scaleFactor;
                fatherObjectCenterPoint.transform.localScale = scaleFactor;
                fatherObjectCenterPoint.transform.rotation = trackedImage.transform.rotation;
                //fatherObjectCenterPoint.transform.localRotation = trackedImage.transform.localRotation;
                //fatherObjectCenterPoint.transform.Rotate ()
            }
            for (int i = counter; i < arObjectsToPlace.Length; i++)
            {
                arObjectsToPlace[i].SetActive(false);
            }
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            for (int i = 0; i < counter; i++)
            {
                arObjectsToPlace[i].SetActive(true);
                fatherObjectCenterPoint.transform.position = trackedImage.transform.localPosition + tempSumOfPosition;
                //arObjectsToPlace[i].transform.position = trackedImage.transform.position; //doesnt work properly
                //arObjectsToPlace[i].transform.localScale = scaleFactor;
                fatherObjectCenterPoint.transform.localScale = scaleFactor;
                //fatherObjectCenterPoint.transform.rotation = trackedImage.transform.rotation;

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
        }

        
    }


}

