using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[ExecuteInEditMode]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] PathCreator pathCreatorLeft;
    [SerializeField] PathCreator pathCreatorRight;

    [SerializeField] List<GameObject> EnemyPrefabs = new List<GameObject>();

    [SerializeField] List<RoundData_SO> roundData = new List<RoundData_SO>();

    void Awake()
    {
        GameManager.OnRoundPassed += StartRound;
    }

    private void OnDestroy()
    {
        GameManager.OnRoundPassed -= StartRound;
    }

#if UNITY_EDITOR
    void Update()
    {
        CheckRoundDurations();
    }
#endif

    void StartRound(int round)
    {
        if (round <= roundData.Count)
            StartCoroutine(_SpawnRoutine(round));
        else
            Debug.LogError("Round data not defined!");
    }

    IEnumerator _SpawnRoutine(int round)
    {
        var attacks = roundData[round - 1].AttackList;
        Debug.Log("Attacks list: " + attacks.Count);
        foreach (var atk in attacks)
        {
            yield return new WaitForSeconds(atk.waitTime);

            if (GameManager.Instance.IsGameOver)
                yield break;

            float rnd = Random.Range(0f, 1f);

            PathCreator pathCreator;
            if (rnd < .5f)
            {
                pathCreator = pathCreatorLeft;
                rnd = Utility.UMath.Normalize(rnd, 0f, .5f);
            }
            else
            {
                pathCreator = pathCreatorRight;
                rnd = Utility.UMath.Normalize(rnd, .5f, 1f);
            }

            var spawnPostition = pathCreator.path.GetPointAtTime(rnd);
            Instantiate(EnemyPrefabs[(int)atk.bulletType], spawnPostition, Quaternion.identity);
        }
    }

    void CheckRoundDurations()
    {
        for (int i = 0; i < roundData.Count; i++)
        {
            float duration = 0f;
            foreach (var atk in roundData[i].AttackList)
            {
                duration += atk.waitTime;
            }

            if (duration > 10)
            {
                Debug.LogWarning("Round " + (i + 1) + " data is longer than 10 seconds.");
            }
        }
    }
}
