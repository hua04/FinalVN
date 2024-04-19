using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
	public string characterName;
	public int[] phaseOne = new int[5];
	public int[] phaseTwo = new int[5];
	public int[] phaseThree = new int[5];

}
