using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Death : Trigger {

	public override void TriggerOnEnter(GameObject _other) {
		if (_other == player)
			if(!PlayerController.instance.Respawning)
				PlayerController.instance.Die(false);
		else
			Destroy(_other);
	}
}
