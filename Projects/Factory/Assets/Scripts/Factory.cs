using UnityEngine;

public class Factory : MonoBehaviour
{
    public enum Spells {Humanoid, Virus, AntiVirus}

    private static int SerialID = 0;

    public MonoBehaviour GetNewInstance(Spells type)
    {
        //return Instantiate(GameObject);
        return null;
    }
}