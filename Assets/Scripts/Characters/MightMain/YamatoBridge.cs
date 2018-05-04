﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YamatoBridge : Yamato {
	
    
    void Start() {
        Name = "The Bridge of the Imperial Japanese Battleship Yamato";
        Stamina = maxStamina;
        Role = "Supporter";

		actions = new string[]{ "None", "Throw", "Catch", "Gather", "Observation Gathering", "Captain's Orders", "Hasty Repairs", "Skill4" };
		actionNames = new string[]{ "None", "Throw", "Catch", "Gather", "Observation Gathering", "Captain's Orders", "Hasty Repairs", "Skill4" };
		actionDescription = new string[]{ "Wait", "Throw ball at target enemy", "Attempt to catch any incoming balls", "Gather balls from the ground", "Stuns each ally, but buffs it in return", "Attacks an enemy with an attack 0.5 times stronger, but the bridge becomes steady as well", "Each of the bridge’s allies gets 40 armor, but each ally becomes staggered as well", "" };
		actionTypes = new string[]{ "None", "Offense", "Defense", "Utility", "Offensive", "Offensive", "Offensive", "Utility" };
		defaultTargetingTypes = new int[]{ 0, 1, 0, 0, 0, 1, 0, 0 };
		alternateTargetingTypes = new int[]{ 0, 1, 0, 0, 0, 1, 0, 0 };
		actionCosts = new int[]{ 0, 1, 0, 0, 1, 1, 0, 0 };

		// Check to see if this overwrites stats correctly
		base.Start ();
    }

    void Update() {
     
    }

    public override bool Skill1() {
		for (int i = 0; i < 3; i++)
		{
			if (this.allies [i] != this && !allies [i].dead) 
			{
				allies [i].addStatusEffect ("stun", 2);
				allies [i].addStatusEffect ("buff", 3);
			}
        }

        this.actionCooldowns[4] = 4;
        return true;
    }

    public override bool Skill2() {
        float variance;
            variance = UnityEngine.Random.Range(0.9f, 1.8f);
		Target [0].loseStamina ((int)(Damage * .5 * variance * attackMultiplier * Target[0].defenseMultiplier));
        this.addStatusEffect("steady", 2);
        this.actionCooldowns[5] = 2;
    return true;
    }

    public override bool Skill3() {
        for(int i = 0; i< 3; i++) {
			if(allies[i] != this)
			{
				allies [i].gainStamina (40);
				addStatusEffect ("unsteady", 2);
			}
        }
        this.actionCooldowns[6] = 3;
		return true;
}

    public override bool Skill4() {
		return false;
    }
}
