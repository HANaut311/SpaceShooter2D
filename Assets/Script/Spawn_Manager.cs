using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerups;



    private bool _StopSpawning = false;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
    

    IEnumerator SpawnEnemyRoutine()
    {   
        // yield return null;//wait one frame
        yield return new WaitForSeconds(3f);
        while(_StopSpawning == false)
        {   
            Vector3 posToSpawn = new Vector3(Random.Range(-9f,9f), 9f,0);
            
            GameObject newEnemy =Instantiate(_enemyPrefab, posToSpawn,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform; //đưa nó vào "cha" khác
            // 
            yield return new WaitForSeconds(3f);
        }

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(_StopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 9f ,0);
            int randomPowerUps = Random.Range(0,3);
            Instantiate(_powerups[randomPowerUps], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5,8));


        }

        
    }



    public void OnPlayerDeath()
    {
        _StopSpawning = true;


    }


}
