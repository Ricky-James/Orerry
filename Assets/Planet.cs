using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] public string m_name;
    [SerializeField] public float m_radius;
    [SerializeField, TextArea(minLines:3, maxLines:6)]
    public string m_info;
    [SerializeField] public int planetNumber;

    [SerializeField] private Material unselectedSource; //Do not modify
    [SerializeField] private Material selectedSource;   //Do not modify
    private Material selected;
    private Material unselected;
    private Renderer render;

    private Color color;

    private void Start()
    {
        unselected = new Material(unselectedSource);
        selected = new Material(selectedSource);
        render = GetComponent<Renderer>();
        render.material = unselected;

        if(this.tag != "Sun")
        {
            color.r = Random.Range(0.0f, 1.0f);
            color.g = Random.Range(0.0f, 1.0f);
            color.b = Random.Range(0.0f, 1.0f);
            unselected.color = color;
            selected.color = color;
        }
    }

    public void PlanetSelected()
    {
        render.material = selected;
    }

    public void PlanetUnselected()
    {
        render.material = unselected;
    }


}
