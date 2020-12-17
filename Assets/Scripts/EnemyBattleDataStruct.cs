using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EnemyBattleDataStruct : MonoBehaviour
{
    public GameObject enemyPrefab;
    public CharacterStats enemyUnit;

    public CharacterStats GetCharStats() { return enemyUnit; }
}
