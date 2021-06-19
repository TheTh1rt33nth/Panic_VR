using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMaker : MonoBehaviour
{
    [Header("Настройки")]
    public int cubeLength;
    public int asteroidsAmount;
    [Range(1,4)]
    public int maxCubeLength;


    [Header("Прочее")]
    public Transform ShipTransform;
    public Transform AsteroidsHolder;
    public GameObject BigAsteroidPrefab, AsteroidPrefab,AsteroidsCube;     

    Vector3Int shipCube;
    Dictionary<Vector3Int, GameObject> asteroidsGrid;
    List<Vector3Int> cubesToShow;
    List<Vector3Int> showedCubes;

    private void Start()
    {
        asteroidsGrid = new Dictionary<Vector3Int, GameObject>();
        cubesToShow = new List<Vector3Int>();
        showedCubes = new List<Vector3Int>();
    }


    Vector3Int ClampCoord()
    {
        Vector3 pos = ShipTransform.position;
        return new Vector3Int( Mathf.RoundToInt(pos.x) / cubeLength, Mathf.RoundToInt(pos.y) / cubeLength, Mathf.RoundToInt(pos.z) / cubeLength);
    }

    void UpdateCubesToShow(Vector3Int center)
    {
        //center -= Vector3Int.one;

        cubesToShow.Clear();

        for (int i = -maxCubeLength; i < maxCubeLength; i++)
        {
            for (int j = -maxCubeLength; j < maxCubeLength; j++)
            {
                for (int k = -maxCubeLength; k < maxCubeLength; k++)
                {
                    Vector3Int shift = new Vector3Int(i, j, k);

                    cubesToShow.Add(center + shift);

                }
            }
        }


    }

    private void Update()
    {
        shipCube = ClampCoord();
        UpdateCubesToShow(shipCube);


        foreach (Vector3Int cubeCoord in showedCubes)
        {
            if (!cubesToShow.Contains(cubeCoord))
                asteroidsGrid[cubeCoord].SetActive(false);
            else
                cubesToShow.Remove(cubeCoord);
        }


        foreach (Vector3Int cubeCoord in cubesToShow)
        {
            if (asteroidsGrid.ContainsKey(cubeCoord))
            {
                asteroidsGrid[cubeCoord].SetActive(true);
                showedCubes.Add(cubeCoord);
            }                
            else
                StartCoroutine(CreateNewAsteroidsCube(cubeCoord));
        }
      
    }

    IEnumerator CreateNewAsteroidsCube(Vector3Int coord)
    {
        GameObject astCube = Instantiate(AsteroidsCube, AsteroidsHolder);
        astCube.name = coord.ToString();
        astCube.transform.position = coord * cubeLength;

        
        if(coord != Vector3Int.zero)
        {
            //Генерация астероидов внутри куба
            for (int i = 0; i < asteroidsAmount; i++)
            { 
                GameObject ast = Instantiate(Random.Range(0, 3) == 1 ? BigAsteroidPrefab : AsteroidPrefab, astCube.transform);
                ast.transform.position = astCube.transform.position - Vector3.one * (cubeLength/2.0f) +
                new Vector3(Random.Range(0,cubeLength), Random.Range(0, cubeLength), Random.Range(0, cubeLength));
                ast.transform.rotation = Quaternion.Euler(Random.Range(0, 180.0f), Random.Range(0, 180.0f), Random.Range(0, 180.0f));
                ast.transform.localScale *= Random.Range(1, 2);
            }
        }       

        asteroidsGrid.Add(coord, astCube);
        showedCubes.Add(coord);
        yield break;
    }

}
