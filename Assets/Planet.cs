using UnityEngine;

public class Planet : MonoBehaviour
{


    private PlanetInfo pInfo;
    [SerializeField] private Material unselectedSource; //Do not modify
    [SerializeField] private Material selectedSource;   //Do not modify
    private Material selected;
    private Material unselected;
    private Renderer rend;

    private Color color;

    private void Start()
    {

        rend = GetComponent<Renderer>();
        pInfo = GetComponent<PlanetInfo>();

        //Create copies of the 2 shaders
        unselected = new Material(unselectedSource);
        selected = new Material(selectedSource);
        //Fix their names in inspector
        selected.name = "Selected";
        unselected.name = "Unselected";
        //Set default to unselected
        rend.material = unselected;

        //Random planet colours for now, could change to sliders in future?
        color.r = Random.Range(0.0f, 1.0f);
        color.g = Random.Range(0.0f, 1.0f);
        color.b = Random.Range(0.0f, 1.0f);
        
        unselected.color = color;
        selected.color = color;

        {
            //Calculate and set outer glow
            //Radius is between 20 and 100.
            //Larger glow for smaller planets
            //Scales between 1.1 and 1.26. Small adjustments have a big impact
            float width = (100 - pInfo.m_radius) * 0.002f + 1.05f;

            selected.SetFloat("_OutlineWidth", width);
        }

    }

    public void PlanetSelected()
    {
        rend.material = selected;
    }

    public void PlanetUnselected()
    {
        rend.material = unselected;
    }


}
