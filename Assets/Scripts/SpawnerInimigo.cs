using System.Linq;
using UnityEngine;

public class SpawnerInimigo : MonoBehaviour
{
    [SerializeField] GameObject inimigo;
    [SerializeField] GameObject zumbi;
    
    float spawnMin = 0.5f;
    float spawnMax = 2f;
    [SerializeField]  float tempo;

    

    private void Start()
    {
        spawnTimerReset();
        

    }

    private void Update()
    {
        tempo -= Time.deltaTime;
        if (tempo <= 0f) {
            spawnTimerReset();
            spawnEnemy();
            spawnEnemy();
            spawnEnemy();
        }

    }

    void spawnTimerReset()
    {
        tempo = Random.Range(spawnMin, spawnMax);
    }

    void spawnEnemy()
    {
        Vector2[] posArray = new Vector2[4];
        float rand = Random.Range(-14f, 14f);
        posArray[0] = new Vector2(rand, 7.8f);
        posArray[1] = new Vector2(rand, 9.09f);
        rand = Random.Range(7.8f, -9.09f);
        posArray[2] = new Vector2(14, rand);
        posArray[3] = new Vector2(-14, rand);

        int indice = Random.Range(0, 4);
        Vector2 pos = posArray[indice];

        //GameObject[] enemyArray = new GameObject[2];
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(inimigo, pos, Quaternion.identity);
        }
        else
        {
            Instantiate(zumbi, pos, Quaternion.identity);
        }

    }
}