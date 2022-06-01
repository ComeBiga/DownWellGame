using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReinforcer
{
    private List<Weapon> line;
    //private Weapon weapon;
    private int index = 0;

    public Weapon Current { get { return line[index]; } }

    public event System.Action OnReinforce;

    public WeaponReinforcer(List<Weapon> line)
    {
        this.line = line;
        //this.weapon = weapon;
        index = 0;
    }

    public bool Reinforce()
    {
        if (index + 1 >= line.Count)
        {
            //reinforced = null;
            Debug.Log("Reinforce Failed!");
            return false;
        }

        index++;
        //reinforced = line[index];
        OnReinforce.Invoke();
        //Debug.Log("Weapon Reinforced!");
        return true;
    }

    public void ReinforceRange(float addedRange)
    {
        Gun gun;

        for(int i = 0; i < line.Count; i++)
        {
            gun = line[i] as Gun;
            gun.addedRange += addedRange;
            //Debug.Log($"Gun addedRange : {gun.addedRange}");

            line[i] = gun;
            Gun dlg = line[i] as Gun;
            //Debug.Log($"Weapon addedRange : {dlg.addedRange}");
        }
    }

    public Weapon BackToFirst()
    {
        index = 0;
        return line[index];
    }
}
