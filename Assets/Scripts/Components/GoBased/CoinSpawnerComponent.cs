using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnerComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _goldCoinPrefab;
    [SerializeField] private GameObject _silverCoinPrefab;


    [SerializeField] private int _maxCoinToDrop;
    [SerializeField] int _chanseToDropSilverCoin;

    public void PercentToSpawnCoins()
    {
        for (int i = 0; i < Random.Range((_maxCoinToDrop/2),  _maxCoinToDrop); i++)
        {
            var x = Random.Range(0, 100);

            if (x <= _chanseToDropSilverCoin)
            {
                Instantiate(_silverCoinPrefab, transform.position = _target.position + new Vector3(Random.Range(-0.6f, 0.6f), 0f, 0f) , Quaternion.identity);

            }
            else
            {
                Instantiate(_goldCoinPrefab, transform.position = _target.position + new Vector3(Random.Range(-0.6f, 0.6f), 0f, 0f), Quaternion.identity);

            }

        }


    }






}
