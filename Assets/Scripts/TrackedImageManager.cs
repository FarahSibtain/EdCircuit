using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageManager : MonoBehaviour
{
    //[SerializeField]
    //private Text imageTrackedText;

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f, 0.1f, 0.1f);

    private ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();   

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        // setup all game objects in dictionary
        //foreach (GameObject arObject in arObjectsToPlace)
        //{
        //    GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
        //    newARObject.SetActive(false);
        //    newARObject.name = arObject.name;
        //    arObjects.Add(arObject.name, newARObject);
        //}
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    //void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    //{
    //    foreach (ARTrackedImage trackedImage in eventArgs.added)
    //    {
    //        UpdateARImage(trackedImage);
    //    }

    //    foreach (ARTrackedImage trackedImage in eventArgs.updated)
    //    {
    //        UpdateARImage(trackedImage);
    //    }

    //    foreach (ARTrackedImage trackedImage in eventArgs.removed)
    //    {
    //        arObjects[trackedImage.name].SetActive(false);
    //    }
    //}

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            foreach (GameObject go in arObjectsToPlace)
            {
                if (go.name == trackedImage.referenceImage.name)
                {                    
                    GameObject GO = Instantiate(go, trackedImage.transform.position, Quaternion.identity);
                    GO.transform.localScale = scaleFactor;
                    //newARObject.SetActive(false);
                    //GO.name = arObject.name;
                    arObjects.Add(trackedImage.referenceImage.name, GO);
                }
            }
            //UpdateARImage(trackedImage);            
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //UpdateARImage(trackedImage);
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                GameObject GO; 
                if (arObjects.TryGetValue(trackedImage.referenceImage.name, out GO))
                {
                    GO.transform.position = trackedImage.transform.position;
                    GO.SetActive(true);
                }               
            }
            else
            {
                GameObject GO;
                if (arObjects.TryGetValue(trackedImage.referenceImage.name, out GO))
                {
                    GO.SetActive(false);
                }                
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // arObjects[trackedImage.name].SetActive(false);
            GameObject GO;
            if (arObjects.TryGetValue(trackedImage.referenceImage.name, out GO))
            {
                Destroy(GO);
            }
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Display the name of the tracked image in the canvas
        //imageTrackedText.text = trackedImage.referenceImage.name;

        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);

        //Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        if (arObjectsToPlace != null)
        {
            GameObject goARObject = arObjects[name];
            goARObject.SetActive(true);
            //goARObject.transform.SetPositionAndRotation(imageTransform.position, imageTransform.rotation);
            goARObject.transform.position = newPosition;
            goARObject.transform.localScale = scaleFactor;
            foreach (GameObject go in arObjects.Values)
            {
                //Debug.Log($"Go in arObjects.Values: {go.name}");
                if (go.name != name)
                {
                    go.SetActive(false);
                    //Verifications comp = go.GetComponent<Verifications>();
                    //comp.ResetInstruments();

                    //go.transform.position = new Vector3(500f, 500f, 500f);
                }
            }
        }
    }

    public void OnResetButtonClicked()
    {
        foreach (GameObject go in arObjects.Values)
        {
            if (go.activeSelf)
            {                 
                switch (go.name)
                {
                    case "bookFinal(Clone)":
                        ConectionVerfication verification2 = go.GetComponent<ConectionVerfication>();
                        verification2.ResetInstruments();
                        break;

                    case "Circuit1(Clone)":
                        Circuit1Verification verification1 = go.GetComponent<Circuit1Verification>();
                        verification1.ResetInstruments();
                        break;

                    case "Circuit3(Clone)":
                        Circuit3Verification verification3 = go.GetComponent<Circuit3Verification>();
                        verification3.ResetInstruments();
                        break;
                }
            }
        }
    }
}

