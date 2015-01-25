using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Word {
	public string text;
	public WordEnum type;
	
	public Word(string txt, WordEnum typ){
		text = txt;
		type = typ;
	}
}

public enum WordEnum{
	VERB,
	PEOPLE,
	NUMBER,
	PRE,
	THINGS,
	UNKNOWN
}

public class InputOrder : MonoBehaviour {
	public Dictionary<string, ActionEnum> actionDict = new Dictionary<string, ActionEnum>();
	public UnityEngine.UI.InputField input;
	public UnityEngine.UI.Text display;
	private Main main;
	
	void Start (){
		main = GetComponent<Main> ();
		input.onEndEdit.AddListener (read);
		display.text = "";
		actionDict.Add("kill", ActionEnum.KILL);
		actionDict.Add("murder", ActionEnum.KILL);
		actionDict.Add("assasinate", ActionEnum.KILL);
		actionDict.Add("chop", ActionEnum.CHOP);
        actionDict.Add("farm", ActionEnum.FARM);
        actionDict.Add("dance", ActionEnum.DANCE);
	}
	
	void read (string e){
		//hay que meter algo para que no salte cuando quitas el foco del texto
		//if (Input.GetKeyDown(KeyCode.KeypadEnter)){
		display.text = "";
		ActionEnum action = ActionEnum.PROCASTINATE;
		int numberVill = 1;
		int numberRep = 1;
		int index = 1;
		int gibberish = -1;
		
		List<Word> words = new List<Word>();
		string[] auxArray = input.text.Trim('!').Split(' ');
		foreach (string w in auxArray) {
			words.Add (new Word(w.ToLower(), setType (w)));
		}
		int length = words.Count;
		if (length == 0) {
			return;
		}
		//meter un for que elimine los nulls, que no se porque pasa
		while(true) {
			if (words[words.Count-1] == null){
				words.RemoveAt (words.Count-1);
			}else{
				break;
			}
		}
		
		index = 0;
		
		if (words[index].type == WordEnum.UNKNOWN){
			//a saber que leches ha puesto el mangarran ahi
			index++;
		}
		
		if (length <= index){
			gibberish = 3;
		}else if (words[index].type == WordEnum.VERB){
			//verbo!!
			action =  actionDict[words[index].text];
			index++;

			if (length > index){
				//tiene un verbo y 1+ palabras

				if (words[index].type == WordEnum.NUMBER){
					//la segunda palabra es un numero, el numero de recursos que debe de recolectar el aldeano
					numberRep = getNumber(words[index].text);
					index++;
					if (length > index && words[index].type == WordEnum.THINGS){
						//la cosa en cuestion; trees, fields, etc
						index++;
					}
				}
				
				if (length > index){
					//vamos a ver si ha puesto un with
					if (words[index].type == WordEnum.PRE){
						//ha puesto un with o similar
						index++;
						if (index < length){
							if (words[index].type == WordEnum.NUMBER){
								//el numero de aldeanos en cuestion
								numberVill = getNumber (words[index].text);
							}else{
								gibberish = 3;
							}
						}
					}else{
						gibberish = 3;
					}
				}
			}
		}else{
			//no hay verbo. A la mierda
			gibberish = 3;
		}
		
		if (gibberish > -1) {
			//texto!
            //OutputOrder.output(gibberish);
			display.text = "SINSENTIDO!!";
			return;
		}

		if (numberVill == 0){ //a todos
			List<GameObject> allVill = main.getAll();
			for (int i = 0; i < allVill.Count; i++){
				ActionManager.AddAction (allVill[i].GetComponent<Villager>(), action, numberRep, true);
			}
		}else{
			for (int i = 0; i < numberVill; i++){
				Villager auxVill = main.getRandomIdle();
				if (auxVill == null){
					//no hay suficientes aldeanos
					auxVill = main.getRandomOccupied();
					
				}
				//OutputOrder.output(1, );
				ActionManager.AddAction (auxVill, action, numberRep, true);
			}
		}
		
		//test output
		//display.text = "";
		//display.text += "Accion: " + action + " NumAldeanos: " + numberVill + " NumRepeticiones: " + numberRep;
		
		/*
		foreach(Word w in words){
			display.text += w.text + "(" + w.type + ") ";
		}*/
	}
	
	int getNumber (string word){
		int number = 1; //ID -1 = numero mayor de tres, ID 0 = todos, ID 1-3 = el numero
		
		if (string.Compare("one", word, true) == 0 || string.Compare("1", word, true) == 0){
			number = 1;
		}else if (string.Compare("two", word, true) == 0 || string.Compare("2", word, true) == 0){
			number = 2;
		}else if (string.Compare("three", word, true) == 0 || string.Compare("3", word, true) == 0){
			number = 3;
		}else if (string.Compare("all", word, true) == 0 || string.Compare("everyone", word, true) == 0){
			number = 0;
		}
		
		return number;
	}
	
	WordEnum setType (string word){
		//aqui se determina si una palabra es verbo, sustantivo, etc.
		string[] dictVerbs = {"murder", "kill", "assasinate", "chop", "cut", "farm", "dance"};
		string[] dictNumbers = {"one", "two", "three", "1", "2", "3", "all", "everyone"};
		string[] dictPeople = {"you", "me", "him", "we", "us", "villager", "villagers"};
		string[] dictPrenumbers = {"with"};
		string[] dictThings = {"tree", "hut", "field", "wheat", "meat", "goat"};
		
		List<string[]> dictionaries = new List<string[]>();
		dictionaries.Add (dictVerbs);
		dictionaries.Add (dictNumbers);
		dictionaries.Add (dictPeople);
		dictionaries.Add (dictPrenumbers);
		dictionaries.Add (dictThings);
		//string a = "kill";
		//string b = "kill";
		//Debug.Log(string.Compare (a,b));
		for(int i = 0; i < dictionaries.Count; i++){
			foreach (string w in dictionaries[i]){
				if (string.Compare(w, word, true) == 0 || (i == 4 && string.Compare(w+"s", word, true) == 0)){
					//return(i+1);
					if (i+1 == 1){
						return WordEnum.VERB;
					}else if (i+1 == 2){
						return WordEnum.NUMBER;
					}else if (i+1 == 3){
						return WordEnum.PEOPLE;
					}else if (i+1 == 4){
						return WordEnum.PRE;
					}else if (i+1 == 5){
						return WordEnum.THINGS;
					}
				}
			}
		}
		return WordEnum.UNKNOWN;
	}
}
