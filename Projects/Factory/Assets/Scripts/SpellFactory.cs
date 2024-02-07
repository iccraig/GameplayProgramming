﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcanum
{
    public enum Spells { Fireball, BlackHole, Meteor, Humanoid, Virus, AntiVirus}

    [RequireComponent(typeof(BlackHoleMaker))]
    [RequireComponent(typeof(FireballMaker))]
    [RequireComponent(typeof(MeteorMaker))]
    [RequireComponent(typeof(HumanoidMaker))]
    [RequireComponent(typeof(VirusMaker))]
    [RequireComponent(typeof(AntiVirusMaker))]
                                          
    public class SpellFactory : MonoBehaviour
    {


        public void BuildSpell(Spells type)
        {
            if(Spells.BlackHole == type)
            {
                var blackHole = this.GetComponent<BlackHoleMaker>().Make();
                blackHole.transform.position = this.transform.position;
            }
            else if (Spells.Fireball == type)
            {
                var fireball = this.GetComponent<FireballMaker>().Make();
                fireball.transform.position = this.transform.position;
            }
            else if (Spells.Meteor == type)
            {
                var fireball = this.GetComponent<MeteorMaker>().Make();
                fireball.transform.position = this.transform.position;
            }
            else if (Spells.Humanoid == type)
            {
                var humanoid = this.GetComponent<HumanoidMaker>().Make();
                humanoid.transform.position = this.transform.position - new Vector3(0, 1, 0);
            }
            else if (Spells.Virus == type)
            {
                var virus = this.GetComponent<VirusMaker>().Make();
                virus.transform.position = this.transform.position;
            } 
            else if (Spells.AntiVirus == type)
            {
                var pill = this.GetComponent<AntiVirusMaker>().Make();
                pill.transform.position = this.transform.position;
            }
        }

        public void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                //this.BuildSpell(Spells.BlackHole);
                this.BuildSpell(Spells.Virus);
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                this.BuildSpell(Spells.Fireball);
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                this.BuildSpell(Spells.Humanoid);
                // this.BuildSpell(Spells.Meteor);
            }
            else if (Input.GetButtonDown("Fire3"))
            {
                this.BuildSpell(Spells.AntiVirus);
            }
        }

    }

}
