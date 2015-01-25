using UnityEngine;
using System.Collections;

public class OutputOrder : MonoBehaviour {

	public static void output(int ID, ActionEnum action, Villager target, int numVill = 1, int numRep = 1){
		string o = "";
		if (ID == 1){ //accion normal
			o = "You! " + action + "!";
		}else if (ID == 2){ //yessir!
			o = "Yes, " + action + "! Yes!";
		}else if (ID == 3){ //
			o = "Ungh? Me no understand! Say action first! Ungh!";
			//o = "Uh? Wut?";
			//o = "";
		}else if (ID == 4){
			o = "What we do now?";
		}
		target.gameObject.GetComponent<Speaker>().SpeakUp(o);
	}
}
