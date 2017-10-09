using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public GameObject[] PlatformPrefabs;
    public GameObject coinPrefab, enemyPrefab;
    private List<GameObject> platforms = new List<GameObject>();
    private Camera cam;
    private List<GameObject> pool;

    private bool mustGenerate
    {
        get
        {
            if(platforms.Count == 0)
            {
                return true;
            }
            //Debug.Log(Extensions.OrtographicBounds(Camera.main).max.x);
            var lastPlatform = platforms[platforms.Count - 1];
            //var spriteRenderer = lastPlatform.GetComponent<SpriteRenderer>();
            //return spriteRenderer.bounds.max.x < Camera.main.OrtographicBounds().max.x;
            return nextGenerationX < cam.OrtographicBounds().max.x;
        }
    }

    private bool mustDestroy
    {
        get
        {
            if (platforms.Count == 0)
            {
                return false;
            }
            //Debug.Log(Extensions.OrtographicBounds(Camera.main).max.x);
            var firstPlatform = platforms[0];
            //var spriteRenderer = firstPlatform.GetComponent<SpriteRenderer>();
            //return spriteRenderer.bounds.max.x < Extensions.OrtographicBounds(Camera.main).min.x;
            //return spriteRenderer.bounds.max.x < Camera.main.OrtographicBounds().min.x;
            return nextDestructionX < cam.OrtographicBounds().min.x;
        }
    }

    private float nextGenerationX, nextDestructionX;

    // Use this for initialization
    void Awake ()
    {
        cam = Camera.main;
        platforms = new List<GameObject>();
        pool = new List<GameObject>();
        foreach (var prefab in PlatformPrefabs)
        {
            for(int i=0; i<2; i++)
            {
                var go = Instantiate(prefab);
                pool.Add(go);
                go.SetActive(false);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            GeneratePlatform();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(mustGenerate)
        {
            GeneratePlatform();
        }

        if(mustDestroy)
        {
            DestroyPlatform();
        }
	}

    private GameObject GetRandomPlatformFromPool()
    {
        if(pool.Count == 0)
        {
            var newGo = Instantiate(PlatformPrefabs[0]);
            pool.Add(newGo);
        }
        var index = Random.Range(0, pool.Count);
        var go = pool[index];
        pool.RemoveAt(index);
        go.SetActive(true);
        return go;
    }

    private void Despawn(GameObject go)
    {
        go.SetActive(false);
        pool.Add(go);
    }

    private void GeneratePlatform()
    {
        //var newPlatform = Instantiate(PlatformPrefabs[Random.Range(0, PlatformPrefabs.Length)]);
        var newPlatform = GetRandomPlatformFromPool();
        var newPlatformCol = newPlatform.GetComponent<Collider2D>();
        var targetPosition = Vector3.zero;
        if(platforms.Count > 0)
        {
            GameObject lastPlatform = platforms[platforms.Count-1];
            var col = lastPlatform.GetComponent<Collider2D>();
            var separation = Random.Range(1f, 3f);
            var aux = lastPlatform.transform.position + Vector3.right * (col.GetComponent<Collider2D>().bounds.size.x+separation);
            var aux2 = lastPlatform.transform.position.y + Random.Range(-0.25f, 0.25f);
            aux2 = Mathf.Clamp(aux2, -0.5f, 0.5f);
            //cambiar linea superior si las plataformas se salen de la pantalla
            targetPosition = new Vector3(aux.x, aux2, lastPlatform.transform.position.z);
        }
        else
        {
            nextDestructionX = newPlatform.GetComponent<Collider2D>().bounds.max.x;
        }
        newPlatform.transform.position = targetPosition;
        nextGenerationX = newPlatform.GetComponent<Collider2D>().bounds.max.x;
        platforms.Add(newPlatform);


        //Coin generator
        var numberOfGroups = Random.Range(1, 4);
        if (Random.value < .7f)
        {
            for (int i = 0; i < numberOfGroups; i++)
            {
                var groupStartX = Random.Range(newPlatformCol.bounds.min.x, newPlatformCol.bounds.max.x);
                var numberOfCoins = Random.Range(3, 6);
                var groupY = Random.Range(1, 3);
                for (int j = 0; j < numberOfCoins; j++)
                {
                    var hit = Physics2D.Raycast(new Vector2(groupStartX + j, newPlatformCol.bounds.max.y + 1), Vector2.down, newPlatformCol.bounds.size.y + 3, 1 << 8);
                    if (hit.collider == null)
                    {
                        break;
                    }

                    Instantiate(coinPrefab, new Vector3(hit.point.x, hit.point.y + groupY, 0), Quaternion.identity);
                }
            }
        }

        //Enemy generator 
        if (Random.value < .9f)
        {
                var enemySpawnX = Random.Range(newPlatformCol.bounds.max.x/2, newPlatformCol.bounds.max.x);
                var hit = Physics2D.Raycast(new Vector2(enemySpawnX, newPlatformCol.bounds.max.y + 1), Vector2.down, newPlatformCol.bounds.size.y + 3, 1 << 8);
                
                if(hit.collider != null)
                {
                Instantiate(enemyPrefab, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
                }
        }

        if (Random.value < .8f)
        {
            var enemySpawnX = Random.Range(newPlatformCol.bounds.max.x / 2, newPlatformCol.bounds.max.x);
            var hit = Physics2D.Raycast(new Vector2(enemySpawnX, newPlatformCol.bounds.max.y + 1), Vector2.down, newPlatformCol.bounds.size.y + 3, 1 << 8);

            if (hit.collider != null)
            {
                Instantiate(enemyPrefab, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
            }
        }

        if (Random.value < .7f)
        {
            var enemySpawnX = Random.Range(newPlatformCol.bounds.max.x / 2, newPlatformCol.bounds.max.x);
            var hit = Physics2D.Raycast(new Vector2(enemySpawnX, newPlatformCol.bounds.max.y + 1), Vector2.down, newPlatformCol.bounds.size.y + 3, 1 << 8);

            if (hit.collider != null)
            {
                Instantiate(enemyPrefab, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
            }
        }
    }

    private void DestroyPlatform()
    {
        if(platforms.Count > 0)
        {
            var firstPlatform = platforms[0];
            platforms.RemoveAt(0);
            //Destroy(firstPlatform);
            Despawn(firstPlatform);
            firstPlatform = platforms[0];
            nextDestructionX = firstPlatform.GetComponent<Collider2D>().bounds.max.x;
        }
    }
}
