using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera _camera;
    [SerializeField] private TilesScript[] tiles;
    private int _emptySpaceIndex = 8;
    private bool _isFinished = false;
    [SerializeField] private GameObject _endPanel;

    void Start()
    {
        _camera = Camera.main;
        Shuffle();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 2.1)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    TilesScript thisTile = hit.transform.GetComponent<TilesScript>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = FindIndex(thisTile);
                    tiles[_emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    _emptySpaceIndex = tileIndex;
                }
            }
        }

        int correctTiles = 0;
        foreach (var a in tiles)
        {
            if (a != null)
            {


                if (a.inRightPlace)

                    correctTiles++;
            }
        }

        if (correctTiles == tiles.Length - 1)
        {
            _isFinished = true;
            _endPanel.SetActive(true);
        }
    }

    public void Shuffle()
    {
        if (_emptySpaceIndex != 8)
        {
            var tileOn8LastPos = tiles[8].targetPosition;
            tiles[8].targetPosition = emptySpace.position;
            emptySpace.position = tileOn8LastPos;
            tiles[_emptySpaceIndex] = tiles[8];
            tiles[8] = null;
            _emptySpaceIndex = 8;
        }
        
        int invertion;
        
        do
        {

            for (int i = 0; i <= 8; i++)
            {
                var lastPos = tiles[i].targetPosition;
                int randomIndex = Random.Range(0, 8);
                tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                tiles[randomIndex].targetPosition = lastPos;
                var tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;
            }

            invertion = GetInversions(); 
        } 
        while (invertion % 2 != 0);
    }

    public int FindIndex(TilesScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }

        return -1;
    }
    
    int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0;i < tiles.Length;i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        return inversionsSum;
    }
}
