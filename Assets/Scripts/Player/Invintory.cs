using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Invintory : MonoBehaviour
{
    public int scrupulons;
    public int dangleHoopers;
    public int squabbleTeeples;
    public int invis;
    public int speed;
    public int teleport;
    public int beam;
    void Start()
    {
        scrupulons = 0;
        dangleHoopers = 0;
        squabbleTeeples = 0;
    }

    public void AddScrap()
    {
        int num = Random.Range(0, 3);
        if (num == 0)
            scrupulons++;
        else if (num == 1)
            squabbleTeeples++;
        else
            dangleHoopers++;
    }

    public void CraftInvis()
    {
        scrupulons--;
        dangleHoopers--;
        invis++;
    }
    public void CraftSpeed()
    {
        scrupulons--;
        squabbleTeeples--;
        speed++;
    }
    public void CraftTele()
    {
        dangleHoopers--;
        squabbleTeeples--;
        teleport++;
    }
    public void CraftBeam()
    {
        scrupulons--;
        squabbleTeeples--;
        dangleHoopers--;
        beam++;
    }
}
