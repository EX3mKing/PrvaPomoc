using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "I_", menuName = "ScriptableObjects/Information", order = 1)]

// ova se klasa koristi za upis podataka o koracima prve pomoci
public class Information : ScriptableObject
{
    public List<string> title;
    public List<string> information;
    public List<Sprite> image;
}
