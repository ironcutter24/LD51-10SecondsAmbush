using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator;

    [SerializeField] List<GameObject> EnemyPrefabs = new List<GameObject>();

    [SerializeField] List<RoundData_SO> roundData = new List<RoundData_SO>();

    void Awake()
    {
        GameManager.OnRoundPassed += StartNextRound;
    }

    private void OnDestroy()
    {
        GameManager.OnRoundPassed -= StartNextRound;
    }

    void StartNextRound(int round)
    {
        StartCoroutine(_SpawnRoutine(round));
    }

    IEnumerator _SpawnRoutine(int round)
    {
        var attacks = roundData[round-1].AttackList;
        Debug.Log("Attacks list: " + attacks.Count);
        foreach (var atk in attacks)
        {
            yield return new WaitForSeconds(atk.waitTime);
            var spawnPostition = pathCreator.path.GetPointAtTime(Random.Range(0f, 1f));
            Instantiate(EnemyPrefabs[(int)atk.bulletType], spawnPostition, Quaternion.identity);
        }
    }
}
