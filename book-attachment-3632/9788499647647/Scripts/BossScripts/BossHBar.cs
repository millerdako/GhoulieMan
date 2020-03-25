using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHBar : MonoBehaviour {

	public float fillAmount;
	public Image fill;
	public float lerpSpeed;

	public GameObject headTrigger;
	private BossHealth health;
	public int currentHealth;
	public float MaxHealth;


	// Use this for initialization
	void Start () {
		
		health = headTrigger.GetComponent<BossHealth>();
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();	
	}

	public void HandleBar() {

		fill.fillAmount = Mathf.Lerp (fill.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

		if (currentHealth >= 0) {
			
		    currentHealth = health.bossHealth;
			fillAmount = (currentHealth / MaxHealth);
		}				
	}	
}
