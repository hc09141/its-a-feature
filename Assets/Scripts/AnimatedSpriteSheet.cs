using UnityEngine;
using System.Collections;

public class AnimatedSpriteSheet : MonoBehaviour
{
	public int  fps = 10;
	public Sprite[] sprites;
	
	//Update
	void Update () { SetSpriteAnimation(fps);  }

	//SetSpriteAnimation
	void SetSpriteAnimation(int fps){

		// Calculate index
		int index  = (int)(Time.time * fps);
		// Repeat when exhausting all cells
		index = index % sprites.Length;

		GetComponent<SpriteRenderer>().sprite = sprites[index];
	}
}