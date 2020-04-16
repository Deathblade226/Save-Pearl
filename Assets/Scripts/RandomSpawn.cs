using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomSpawn : MonoBehaviour {

[SerializeField] List<GameObject> Spawned;

private GameObject SpawnedItem = null;

void Start() {
    SpawnedItem = Spawned.ElementAt(Random.Range(0, Spawned.Count));
    GameObject.Instantiate(SpawnedItem, gameObject.transform, false);
}

}
