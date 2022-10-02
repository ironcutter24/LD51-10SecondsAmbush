using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "RoundData", menuName = "ScriptableObjects/RoundData", order = 1)]
public class RoundData_SO : SerializedScriptableObject
{
    [SerializeField] List<AttackData> attackList;
    public List<AttackData> AttackList { get => attackList; }

    [System.Serializable]
    public struct AttackData
    {
        public float waitTime;
        public BulletType bulletType;
    }

    public enum BulletType { Normal, Fast, Slow, Explosive };
}
