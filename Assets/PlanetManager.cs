using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlanetManager : MonoBehaviour
{

    public GameObject Sun;
    private List<GameObject> Planets = new List<GameObject>();
    public GameObject defaultPlanet;

    //Distance between planets
    private const float planetGap = 300f; 

    public InputField nameBox;
    public Slider sizeSlider;
    public InputField infoBox;


    private void Start()
    {
        if (!Sun)
            Sun = GameObject.Find("Sun");
        Planets.Add(Sun);
    }

    public void AddPlanet()
    {
        int planetCount = Planets.Count;
        //Create planet object
        GameObject newPlanet = Instantiate(defaultPlanet, null);


        //Get info component (name, size, speed.. Anything I have time to add)
        PlanetInfo planetInfo = newPlanet.GetComponent<PlanetInfo>();


        //Fixed distance between planets (const) + diameter of all planets + Radius of sun + radius of new planet
        float startXPos = (planetCount * planetGap) + (Sun.transform.localScale.x / 2f);

        //Set user variables from game window
        planetInfo.m_radius = Mathf.Round(sizeSlider.value);
        planetInfo.m_name = nameBox.text;
        planetInfo.m_info = infoBox.text;


        newPlanet.name = planetInfo.m_name; //Rename in hierarchy
        newPlanet.transform.localScale = new Vector3(planetInfo.m_radius * 2, planetInfo.m_radius * 2, planetInfo.m_radius * 2);
        newPlanet.transform.localPosition = (new Vector3(startXPos, 0, 0));



        //Add to list
        Planets.Add(newPlanet);

        //Sun is 0
        planetInfo.planetNumber = planetCount;
    }

    public void DeletePlanet(GameObject planet)
    {
        Planets.Remove(planet);
        Destroy(planet);


        for (int i = 1; i < Planets.Count; i++)
        {
            float newXPos = (i * planetGap) + (Sun.transform.localScale.x / 2f);

            //Implement a proper reset planet function when orbits are implemented
            Planets[i].transform.localPosition = new Vector3(newXPos, 0, 0);

        }


    }

    void Update()
    {
        Debug.Log(Planets.Count);
    }
}
