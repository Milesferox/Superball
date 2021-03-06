﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greg: Character
{
	public Character trevor;

    new void Start()
    {
        Name = "Greg";
        Stamina = maxStamina;
        
        Role = "Support";

		actionNames = new string[]{ "None", "Throw", "Catch", "Gather", "Terrapin", "Hide", "Steal", "Roller Derby" };
		actionDescription = new string[]{ "Wait", "Throw ball at target enemy", "Attempt to catch any incoming balls", "Gather balls", 
										  "Become immune to all attacks for 1 turn, sending any balls aimed at you to Trevor. <color=red>2</color> turn cooldown.\nCost: 1    Target: Self", 
										  "Block incoming attacks, but become <color=yellow>stunned</color> on next turn. <color=red>2</color> turn cooldown.\nCost: None    Target: Self",
                                          "Reduce an enemy's ball count by 2 and increase your own by 2. <color=red>2</color> tunr cooldown.\nCost: None    Target: Single Enemy",
                                          "Reduce an enemy's stamina by 25% of their max stamina. <color=red>3</color> turn cooldown.\nCost: 3    Target: Single Enemy" };
		actionTypes = new string[]{ "None", "Offense", "Defense", "Utility", "Defense", "Defense", "Utility", "Offense" };
		defaultTargetingTypes = new int[]{ 0, 1, 0, 0, 0, 0, 1, 1 };
		actionCosts = new int[]{ 0, 1, 0, 0, 1, 0, 0, 3 };



		base.Start ();

		trevor = GameObject.FindObjectOfType<Trevor> ();
		/*
		foreach (Character C in enemies) 
		{
			if(C.Name == "Trevor")
			{
				trevor = C;
			}
		}
		foreach (Character C in allies) 
		{
			if(C.Name == "Trevor")
			{
				trevor = C;
			}
		}*/
    }


    new void Update() {
		base.Update ();
    }
    

	public new void Init(CombatManager CM, CharacterSelectUI combatUI)
	{
		base.Init (CM, combatUI);

		foreach (Character C in enemies) 
		{
			if(C.Name == "Trevor")
			{
				trevor = C;
			}
		}
		foreach (Character C in allies) 
		{
			if(C.Name == "Trevor")
			{
				trevor = C;
			}
		}
	}

	// This skill is Greg's Terrapin skill
	// If a hit is successful against Greg and terrapin has been used it rebounds into Trevor's ball pool
	// Is there a cost for this?
	public override int Skill1()
    {
		if (trevor) {
			//recall this is a defense skill so it is called to see if you get hit, ignoring what the enemie's ability is. If they throw multiple balls, then Terrapin happens multiple times
			if (trevor.heldBalls < trevor.maxBalls)
				trevor.heldBalls++;
			actionCooldowns [4] = 3;
			for (int i = 0; i < combat.combatQueue [combat.currentCharacter].actionNames.Length; i++) {
				if (combat.combatQueue [combat.currentCharacter].action == combat.combatQueue [combat.currentCharacter].actionNames [i]) {
					combat.combatQueue [combat.currentCharacter].heldBalls -= combat.combatQueue [combat.currentCharacter].GetActionCost (i);
				}
			}
		}
		return -1;
    }

	//Skill2
	public override int Skill2()
	{
        //recall status effects dont stack
		this.addStatusEffect(STATUS.STUN, 2);
        actionCooldowns[5] = 3;
		return -1;
	}

	// Steal
	public override int Skill3()
	{
		for(int i = 0; i < 3; i++)
		{
			if (Target[0].heldBalls > 0 && this.heldBalls < this.maxBalls) {
				Target[0].heldBalls--;
				this.heldBalls++;
			}
		}
        actionCooldowns[6] = 3;
		return 0;
	}

    public override int Skill4() {
        int max = Target[0].maxStamina;
        Target[0].loseStamina(Target[0].maxStamina / 4);
        this.heldBalls -= actionCosts[7];
        this.actionCooldowns[7] = 4;
        return max/4;
    }
    /*
    public override bool Skill2() {
       int diff = Target[0].maxBalls - Target[0].heldBalls;
        while (diff > 0 && this.heldBalls > 0) {
            Target[0].heldBalls++;
            diff = Target[0].maxBalls - Target[0].heldBalls;
        }
		return true;
    }
    */
}
