using Helper;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GridController : MonoSingleton<GridController>
{
    public int n;
    private List<GameObject> _objectPool;
    [SerializeField] private GameObject _gridCellPrefab;
    private GridCellScript[,] _allGridCells;


    private void Start()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        CameraController.Instance.SetOrthographicSize(n);
        DisablePoolObjects();
        _allGridCells = new GridCellScript[n, n];
        var pos = Vector2.zero;

        var offset = (float)n / 2 - 0.5f;
        for (int i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var gridCellObject = _objectPool.FirstOrDefault(x => !x.activeSelf);

                if (gridCellObject != null)
                {
                    gridCellObject.transform.position = new Vector2(i - offset, j - offset);
                    gridCellObject.SetActive(true);
                }
                else
                {
                    gridCellObject = Instantiate(_gridCellPrefab, new Vector2(i - offset, j - offset), Quaternion.identity, transform);
                    _objectPool.Add(gridCellObject);
                }
                var gridCell = gridCellObject.GetComponent<GridCellScript>();
                _allGridCells[i, j] = gridCell;
                gridCell.x = i;
                gridCell.y = j;
            }
        }


    }

    private void DisablePoolObjects()
    {
        _objectPool ??= new List<GameObject>();
        for (var i = 0; i < _objectPool.Count; i++)
        {
            var obj = _objectPool[i];
            if (obj == null)
            {
                _objectPool.RemoveAt(i);
                i--;
                continue;
            }
            obj.SetActive(false);
        }
    }

    private static readonly Vector2Int[] NeighborIndexes =
    {
        new Vector2Int(-1,0),
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(0,-1)
    };

    public List<GridCellScript> CheckNeighborhood(int x, int y, List <GridCellScript> oldNeighbors = null)
    {
        oldNeighbors ??= new List<GridCellScript>();
        foreach (var index in NeighborIndexes)
        {
            if (x + index.x >= n || x + index.x < 0 || y + index.y >= n || y + index.y < 0)
                continue;

            var neighbor = _allGridCells[x + index.x, y + index.y];
            if (NeighborIndexes == null || !neighbor.isActive || oldNeighbors.Contains(neighbor))
                continue;

            oldNeighbors.Add(neighbor);
            CheckNeighborhood(neighbor.x, neighbor.y, oldNeighbors);       
        }

        return oldNeighbors;
    }
}
