using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private int _levelCount;
    [SerializeField] private int _additionalScale;
    [SerializeField] private Beam _beam;
    [SerializeField] private SpawnPlatform _spawnPlatform;
    [SerializeField] private Platform[] _platforms;
    [SerializeField] private FinishPlatform _finishPlatform;

    private float _startAndFinishAdditionalScale = 0.5f;

    public float BeamScaleY => _levelCount / 2f + _startAndFinishAdditionalScale + _additionalScale / 2f;

    private void Awake()
    {
        Build();
    }
    private void Build()
    {
        Beam beam = Instantiate(_beam, transform);
        Debug.Log($"{beam.transform.position} {transform.position}");
        beam.transform.localScale = new Vector3(1, BeamScaleY, 1);

        Vector3 spawnPosition = beam.transform.position;
        spawnPosition.y += beam.transform.localScale.y - _additionalScale;
        SpawnPlatform(_spawnPlatform, ref spawnPosition, transform);

        for(int i = 0; i < _levelCount; i++)
            SpawnPlatform(_platforms[Random.Range(0, _platforms.Length)], ref spawnPosition, transform);

        SpawnPlatform(_finishPlatform, ref spawnPosition, transform);
    }

    private void SpawnPlatform(Platform platform, ref Vector3 spawnPosition, Transform parent)
    {
       Instantiate(platform, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0), parent);
        spawnPosition.x = 0;
        spawnPosition.z = 0;
        spawnPosition.y -= 1;
        Debug.Log(spawnPosition);
    }

    private void DestroyAll()
    {
        Destroy(FindObjectOfType<Camera>().gameObject);
        Destroy(FindObjectOfType<Ball>().gameObject);
        Destroy(FindObjectOfType<Beam>().gameObject);
        foreach (var platform in FindObjectsOfType<Platform>())
            Destroy(platform.gameObject);
    }

    public void BuildNewLevel()
    {
        DestroyAll();

        _levelCount = Convert.ToInt32(_levelCount * 1.5f);
        Build();
    }
}
