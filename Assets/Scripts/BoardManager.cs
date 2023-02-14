// using System.Collections;
// using System.Collections.Generic;

// public class BoardManager : MonoBehaviour
// {
//   public int width;
//   public int height;
//   public float swapTime = 0.5f;
//   public GameObject tilePrefab;
//   public GameObject[] gamePieces;

//   private GameObject[,] tiles;
//   private GameObject selectedPiece;
//   private Vector2 swipeDirection;
//   private bool isSwapping = false;

//   void Start()
//   {
//     tiles = new GameObject[width, height];
//     SetupBoard();
//   }

//   void SetupBoard()
//   {
//     for (int x = 0; x < width; x++)
//     {
//       for (int y = 0; y < height; y++)
//       {
//         Vector2 position = new Vector2(x, y);
//         GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
//         newTile.transform.parent = transform;
//         tiles[x, y] = newTile;
//         int randomIndex = Random.Range(0, gamePieces.Length);
//         GameObject newGamePiece = Instantiate(gamePieces[randomIndex], position, Quaternion.identity);
//         newGamePiece.transform.parent = newTile.transform;
//         tiles[x, y].GetComponent<Tile>().piece = newGamePiece;
//       }
//     }
//   }

//   public void SelectPiece(GameObject piece)
//   {
//     if (selectedPiece == piece)
//     {
//       selectedPiece = null;
//     }
//     else if (selectedPiece == null)
//     {
//       selectedPiece = piece;
//     }
//     else
//     {
//       SwapPieces(piece);
//     }
//   }

//   void SwapPieces(GameObject piece)
//   {
//     if (isSwapping)
//     {
//       return;
//     }

//     Tile selectedTile = selectedPiece.GetComponentInParent<Tile>();
//     Tile targetTile = piece.GetComponentInParent<Tile>();

//     if (CanSwap(selectedTile, targetTile))
//     {
//       isSwapping = true;

//       Vector2 targetPosition = selectedTile.transform.position;
//       selectedTile.transform.DOMove(targetTile.transform.position, swapTime);
//       targetTile.transform.DOMove(targetPosition, swapTime).OnComplete(() =>
//       {
//         FinishSwap(selectedTile, targetTile);
//       });
//     }
//     else
//     {
//       selectedPiece = null;
//     }
//   }

//   bool CanSwap(Tile selectedTile, Tile targetTile)
//   {
//     if (Mathf.Abs(selectedTile.transform.position.x - targetTile.transform.position.x) +
//         Mathf.Abs(selectedTile.transform.position.y - targetTile.transform.position.y) == 1)
//     {
//       return true;
//     }
//     return false;
//   }

//   void FinishSwap(Tile selectedTile, Tile targetTile)
//   {
//     GameObject selectedPiece = selectedTile.piece;
//     selectedTile.piece = targetTile.piece;
//     targetTile.piece = selectedPiece;

//     if (IsMatched(selectedTile) || IsMatched(targetTile))
//     {
//       selectedTile.piece = targetTile.piece = null;
//       selectedTile.isMatched = targetTile.isMatched = true;
//       isSwapping = false;
//     }
//     else
//     {
//       selectedTile.piece.transform.DOMove(selectedTile.transform.position, swapTime);
//       targetTile.piece.transform.DOMove(targetTile.transform.position, swapTime).OnComplete(() =>
//       {
//         isSwapping = false;
//       });
//     }
//   }
//   bool IsMatched(Tile tile)
//   {
//     if (tile.piece == null)
//     {
//       return false;
//     }

//     bool horizontalMatch = Mathf.Abs(GetMatchLength(tile, Vector2.right) + GetMatchLength(tile, Vector2.left)) >= 2;
//     bool verticalMatch = Mathf.Abs(GetMatchLength(tile, Vector2.up) + GetMatchLength(tile, Vector2.down)) >= 2;

//     return horizontalMatch || verticalMatch;
//   }

//   int GetMatchLength(Tile tile, Vector2 direction)
//   {
//     int matchLength = 0;

//     RaycastHit2D hit = Physics2D.Raycast(tile.transform.position, direction);

//     while (hit.collider != null && hit.collider.GetComponent<Tile>().piece != null &&
//            hit.collider.GetComponent<Tile>().piece.CompareTag(tile.piece.tag))
//     {
//       matchLength++;
//       hit = Physics2D.Raycast(hit.collider.transform.position, direction);
//     }

//     return matchLength;
//   }
// }
