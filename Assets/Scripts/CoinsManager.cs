using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CoinsManager : MonoBehaviour
{
	//References
	[Header ("UI references")]
	[SerializeField] TMP_Text coinUIText;
	[SerializeField] TMP_Text earnedCoinUIText;
	[SerializeField] GameObject coinEarnedUI;
	//[SerializeField] GameObject animatedCoinPrefab;
	[SerializeField] Transform target;
	[SerializeField] GameObject animatedCoinPrefab;
	[SerializeField] private GameObject canvas;

	[Space]
	[Header ("Available coins : (coins to pool)")]
	[SerializeField] int maxCoins;
	Queue<GameObject> coinsQueue = new Queue<GameObject> ();


	[Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (1f, 4f)] float maxAnimDuration;

	[SerializeField] LeanTweenType easeType;
	[SerializeField] float spread;

	Vector3 targetPosition;
	

	void Start ()
	{
		targetPosition = target.position;

		//prepare pool
		PrepareCoins ();
		if (coinUIText != null) {
			coinUIText.text = PlayerPrefs.GetInt("Coins").ToString();
		}
	}

	private void PrepareCoins ()
	{
		GameObject coin;
		for (int i = 0; i < maxCoins; i++) {
			coin = Instantiate(animatedCoinPrefab);
			coin.transform.SetParent(canvas.transform);
			coin.SetActive(false);
			coinsQueue.Enqueue (coin);
		}
	}

	public void UpdateCoinAmount()
	{
		if (PlayerPrefs.HasKey("Coins")) {
			coinUIText.text = PlayerPrefs.GetInt("Coins").ToString();
		}
	}

	public void Animate (Vector3 collectedCoinPosition, int amount)
	{
		for (int i = 0; i < amount; i++) {
			//check if there's coins in the pool
			if (coinsQueue.Count > 0) {
				//extract a coin from the pool
				GameObject coin = coinsQueue.Dequeue ();
				coin.SetActive(true);

				//move coin to the collected coin pos
				coin.transform.position = collectedCoinPosition + new Vector3 (Random.Range (-spread, spread), 0f, 0f);

				
				
				//animate coin to target position
				float duration = Random.Range (minAnimDuration, maxAnimDuration);
				coin.transform.LeanMove(targetPosition,duration).setEase(easeType).setIgnoreTimeScale(true).setOnComplete(() =>
				{
					coin.SetActive(false);
					coinsQueue.Enqueue (coin);
					coinUIText.text = (Int32.Parse(coinUIText.text) + 1).ToString();
					
					earnedCoinUIText.text = "+ " + (amount-i);

					if (i == amount - 1)
					{
						coinEarnedUI.SetActive(false);
					}
				});
			}
		}
	}
	
}
