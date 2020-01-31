using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    [SerializeField] public string m_name;
    [SerializeField] public float m_radius;
    [SerializeField, TextArea(minLines: 3, maxLines: 6)]
    public string m_info;
    [SerializeField] public int planetNumber;


}
