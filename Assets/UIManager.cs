using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private PlanetManager planetManager;

    private Vector2 mousePos;

    private Ray ray;
    private RaycastHit hit;


    
    [SerializeField] private freeMoveCam cam;

    //Toggled on right-click
    private bool UIActive;

    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject infoBox;

    [SerializeField] private Text nameText;
    [SerializeField] private Text radiusText;
    [SerializeField] private Text infoText;

    [HideInInspector] public GameObject SelectedPlanet;

    /// <summary>
    /// Graphics raycasting for UI elements
    /// This is to stop UI elements from disappearing when they're clicked
    /// By default, any click will hide all elements (Except create planet panel)
    /// Right-clicking hides *all* elements* and enables camera
    /// </summary>
    GraphicRaycaster UIray;
    EventSystem eventSystem;
    PointerEventData pointerEventData;

    private void Awake()
    {
        UIray = UI.GetComponentInParent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        pointerEventData = new PointerEventData(eventSystem);
    }

    private void Start()
    {
        UIActive = true;
        cam.enabled = false;
        Cursor.visible = true;

        planetManager = this.GetComponent<PlanetManager>();
        if (!cam)
            cam = Camera.main.GetComponent<freeMoveCam>();
        if (!infoBox)
            infoBox = GameObject.Find("PlanetInfo");
        if (!deleteButton)
            deleteButton = GameObject.Find("DeletePlanetPanel");

        infoBox.SetActive(false);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //Right-click show/hide UI elements
        {
            UIActive = !UIActive;

            //Hide 'temporary' UI elements
            infoBox.SetActive(false);
            deleteButton.SetActive(false);
            //Toggle 'permanent' UI elements
            UI.SetActive(UIActive);
            
            //Toggle camera script + mouse
            cam.enabled = !UIActive;
            Cursor.visible = UIActive;
            if (UIActive)
                Cursor.lockState = CursorLockMode.None;
                
        }
        
        if(UIActive)
        {
            mousePos = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(mousePos);

            if (Input.GetMouseButtonDown(0))
            {

                if (Physics.Raycast(ray, out hit))
                {

                    
                    SelectedPlanet = hit.collider.gameObject;
                    DisplayPlanetInfo(SelectedPlanet);
                    deleteButton.SetActive(true);
                }
                else //De-select planet
                {

                    pointerEventData.position = mousePos; 

                    List<RaycastResult> results = new List<RaycastResult>();
                    UIray.Raycast(pointerEventData, results);

                    foreach(RaycastResult result in results)
                    {
                        if (result.gameObject.name == "DeletePlanetPanel")
                            return;
                    }

                    //Only runs if delete button wasn't clicked
                    deleteButton.SetActive(false);
                    infoBox.SetActive(false);
                 
                    
                }
            }         
        }
    }

    void DisplayPlanetInfo(GameObject planet)
    {
        Planet planetInfo = planet.GetComponent<Planet>();
        nameText.text = planetInfo.m_name;
        //Multiply by 1000 so it sounds more realistic/scaled
        string size = (planetInfo.m_radius * 1000).ToString("N0");
        radiusText.text = size + "km";
        infoText.text = planetInfo.m_info;
        infoBox.SetActive(true);
    }

    public void DeletePlanetClick()
    {
        if (SelectedPlanet.GetComponent<Planet>().m_name != "The Sun")
        {
            planetManager.DeletePlanet(SelectedPlanet);
            SelectedPlanet = null;
            infoBox.SetActive(false);
            deleteButton.SetActive(false);
        }

    }

}
