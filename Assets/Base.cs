﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
	public float tiberium = 0;
    public TextMeshPro text;
    public GameObject fighterPrefab;

	private IEnumerator AccumulateTiberium(float time)
	{
		while (true)
		{
			yield return new WaitForSeconds(time);
			tiberium += 1;

			if (tiberium > 10)
			{
				CreateShip();
			}
		}
	}

	private void CreateShip()
	{
		tiberium -= 10;
		GameObject ship = Instantiate(fighterPrefab, transform.position + new Vector3(0, 0, 2), Quaternion.identity);
		ship.transform.parent = this.transform;
		ship.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
		ship.GetComponentInChildren<MeshRenderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
	}

	private void OnEnable()
	{
		StartCoroutine(AccumulateTiberium(1.0f));
	}

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + tiberium;
    }
}
