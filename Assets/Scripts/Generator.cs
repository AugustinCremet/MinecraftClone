using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
	[SerializeField] GameObject bedRockPrefab, rockPrefab, dirtPrefab, grassPrefab, woodPrefab, leafPrefab, waterPrefab;
	[Range(2.0f, 100.0f)]
	[SerializeField] float smoothness, height;
	[SerializeField] int minStoneHeight, maxStoneHeight;
	[SerializeField] int waterLevel;
	[Range(0, 100)]
	[SerializeField] int treeChance;
	public int width;

	public int chunk = 0;

	GameObject levelContainer;
	GameObject waterContainer;
    
	void Awake()
	{
		levelContainer = new GameObject("LevelContainer");
	}

    void Start()
    {
        Generation();
    }

    void Generation()
	{
		for(int x = 0; x < width; ++x)
		{
			for(int z = 0; z < width; ++z)
			{
				float perlinNoise = Mathf.PerlinNoise(x / smoothness, z / smoothness);
				int heightPerlinNoise = Mathf.RoundToInt(perlinNoise * height);
				int minRockSpawnDistance = heightPerlinNoise - minStoneHeight;
                int maxRockSpawnDistance = heightPerlinNoise - maxStoneHeight;
                int totalRockSpawnDistance = Random.Range(minRockSpawnDistance, maxRockSpawnDistance);

				for(int y = 0; y <= heightPerlinNoise; ++y)
				{
					if(y == 0)
					{
						Instantiate(bedRockPrefab, new Vector3(x, y, z), Quaternion.identity, levelContainer.transform);
					}
					else if(y == heightPerlinNoise)
					{
						Instantiate(grassPrefab, new Vector3(x, y, z), Quaternion.identity, levelContainer.transform);
						if(treeChance > Random.Range(0, 100))
						{
							GenerateTree(x, y + 1, z);
						}
					}
					else if(y <= totalRockSpawnDistance)
					{
						Instantiate(rockPrefab, new Vector3(x, y, z), Quaternion.identity, levelContainer.transform);
					}
					else
					{
						Instantiate(dirtPrefab, new Vector3(x, y, z), Quaternion.identity, levelContainer.transform);
					}				
				}
				for(int y = 0; y < waterLevel - heightPerlinNoise; ++y)
				{
					Instantiate(waterPrefab, new Vector3(x, y + heightPerlinNoise + 1, z), Quaternion.identity, levelContainer.transform);
				}
			}
		}
	}
	
	void GenerateTree(int x, int y, int z)
	{
		int treeHeight = Random.Range(3, 8);
		for(int i = 0; i < treeHeight; ++i)
		{
			Instantiate(woodPrefab, new Vector3(x, y + i, z), Quaternion.identity, levelContainer.transform);
			if(i + 1 == treeHeight)
			{
				int leafHeight = Random.Range(2, 5);
				for(int j = 0; j < leafHeight; ++j)
				{
					Instantiate(leafPrefab, new Vector3(x + 1, y + i + j, z), Quaternion.identity, levelContainer.transform);
					Instantiate(leafPrefab, new Vector3(x - 1, y + i + j, z), Quaternion.identity, levelContainer.transform);
					Instantiate(leafPrefab, new Vector3(x, y + i + j, z + 1), Quaternion.identity, levelContainer.transform);
					Instantiate(leafPrefab, new Vector3(x, y + i + j, z - 1), Quaternion.identity, levelContainer.transform);
					Instantiate(leafPrefab, new Vector3(x + 1, y + i + j, z + 1), Quaternion.identity, levelContainer.transform);
					Instantiate(leafPrefab, new Vector3(x + 1, y + i + j, z - 1), Quaternion.identity, levelContainer.transform);
					Instantiate(leafPrefab, new Vector3(x - 1, y + i + j, z + 1), Quaternion.identity, levelContainer.transform);
					Instantiate(leafPrefab, new Vector3(x - 1, y + i + j, z - 1), Quaternion.identity, levelContainer.transform);
					Instantiate(leafPrefab, new Vector3(x, y + i + j + 1, z), Quaternion.identity, levelContainer.transform);
				}
			}
		}
	}
}
