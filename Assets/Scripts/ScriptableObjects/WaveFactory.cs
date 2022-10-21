using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Data/Create New Wave Factory", fileName = "NewWaveFactory")]
public class WaveFactory : ScriptableObject
{
    #region Fields

    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Vector3 _spawnValues;
    [SerializeField] private int _waveSize;
    [SerializeField] private float _spawnWait;
    [SerializeField] private float _startWait;
    [SerializeField] private float _waveWait;

    public float StartWait => _startWait;
    public float WaveWait => _waveWait;

    #endregion //Fields

    #region Factory Methods

    public void SpawnWave(Action onComplete = null)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(SpawnWaveRoutine(onComplete));
    }

    #endregion //Factory Methods

    #region Private Methods

    private IEnumerator SpawnWaveRoutine(Action onComplete)
    {
        for (int i = 0; i < _waveSize; i++)
        {
            GameObject hazard = _prefabs[Random.Range(0, _prefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-_spawnValues.x, _spawnValues.x), _spawnValues.y, _spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(_spawnWait);
        }

        onComplete?.Invoke();
    }

    #endregion //Private Methods
}
