using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour {
    ScoreKeeper scoreKeeper;

    [SerializeField] GameObject prefab;

    // Use this for initialization
    void Start () {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(prefab, new Vector3(26f, 0, 10f), Quaternion.identity);
            Destroy(this.gameObject);
            scoreKeeper.Score(1);
        }
    }
}
