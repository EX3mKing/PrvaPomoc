using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "I_", menuName = "ScriptableObjects/Information", order = 1)]
public class Information : ScriptableObject
{
    public List<string> information;
    public List<Sprite> image;
}
