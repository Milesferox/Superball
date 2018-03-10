﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour 
{
	public CombatManager CM;

	void Start()
	{
		CM = GameObject.Find ("CombatManager").GetComponent<CombatManager> ();
	}

	void Update () 
	{
		if(CM.currentPhase != CombatManager.PHASE.START && CM.currentPhase != CombatManager.PHASE.EXECUTE)
		{
			if (CM.currentCharacter > -1 && CM.currentCharacter < 6) 
			{
				gameObject.transform.position = new Vector3 (CM.combatQueue[CM.currentCharacter].character.transform.position.x, 
					CM.combatQueue[CM.currentCharacter].character.transform.position.y + 1, 2);
			}
		}
		else
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 100f);
		}
	}
}