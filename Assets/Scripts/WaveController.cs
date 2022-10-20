using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    #region Fields

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private Coroutine _spawnRoutine;

    #endregion //Fields

    #region Unity Methods

    private void OnEnable()
    {
        EventManager.Instance.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.Instance.GameOver -= OnGameOver;
    }

    #endregion //Unity Methods

    #region Public Methods

    public void Init()
    {
        _spawnRoutine = StartCoroutine(SpawnWaves());
    }

    #endregion

    #region Private Methods

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    private void OnGameOver()
    {
        StopCoroutine(_spawnRoutine);
    }

    #endregion //Private Methods
}