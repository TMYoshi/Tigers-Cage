using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_Manager : MonoBehaviour
{
    [SerializeField] private Transform game_transform_;
    [SerializeField] private Transform piece_prefab_; // Set it to a small square (100 x 100?)

    private List<Transform> pieces_;
    private int size_;
    private int empty_location_;
    private bool shuffling_;

    #region CreateGamePieces
    // Create the game setup with size_ x size_ pieces
    private void CreateGamePieces(float gap_thickness)
    {
        float width = 1 / (float)size_;

        for (int row = 0; row < size_; ++row)
        {
            for (int column = 0; column < size_; ++column)
            {
                Transform piece = Instantiate(piece_prefab_, game_transform_);
                pieces_.Add(piece);

                // Pieces in the game go from -1 -> 1
                piece.localPosition = new Vector3(-1 + (2 * width * column) + width,
                                                   1 - (2 * width * row) - width,
                                                   0);

                piece.localScale = ((2 * width) - gap_thickness) * Vector3.one;
                piece.name = $"{(row * size_) + column}"; // Interpolated String

                // Keep the title in the bottom right empty 
                if ((row == size_ - 1) && (column == size_ - 1))
                {
                    empty_location_ = (size_ * size_) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    // Mapping UV coordinates
                    float gap = gap_thickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];

                    // UV Coordinate order: (0,1), (1,1), (0,0), (1,0)
                    uv[0] = new Vector2((width * column) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (column + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * column) + gap, 1 - ((width * row) - gap));
                    uv[3] = new Vector2((width * (column + 1)) - gap, 1 - ((width * row) - gap));

                    // Assign UV cords to the mesh.
                    mesh.uv = uv;
                }
            }
        }
    }
    #endregion

    #region SwapIfValid
    private bool SwapIfValid(int tile, int offset, int column_check)
    {
        if (((tile % size_) != column_check) && ((tile + offset) == empty_location_))
        {
            Debug.Log("Swapping tiles" + tile + " and " + (tile + offset));
            // Swap the tiles' game states
            (pieces_[tile], pieces_[tile + offset]) = (pieces_[tile + offset], pieces_[tile]);

            // Swap the tiles' positions
            (pieces_[tile].localPosition, pieces_[tile + offset].localPosition) = (pieces_[tile + offset].localPosition, pieces_[tile].localPosition);

            // Update empty location
            empty_location_ = tile;
            return true;
        }
        Debug.Log(tile + " was not swapped");
        return false;
    }
    #endregion

    #region CheckCompletion
    public bool CheckCompletion()
    {
        for (int tile = 0; tile < pieces_.Count; ++tile)
        {
            if (pieces_[tile].name != $"{tile}")
            {
                return false;
            }
        }

        StartCoroutine(WaitToReveal());
        Debug.Log("Puzzle completed!");

        return true;
    }
    #endregion

    #region WaitShuffle
    private IEnumerator WaitShuffle(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);
        Shuffle();
        shuffling_ = false;
    }
    #endregion

    #region Shuffle
    private void Shuffle()
    {
        // Use brute force shuffle because number of elements low + accounts for unsolvable tile sets
        // If we really wanted to, weyou could work backwards and manually shuffle the tiles here so that each solution is the same, but ehhh
        int count = 0;
        int prev = 0;
        while (count < size_ * size_ * size_ * size_)
        {
            // Pick a random location 
            int random = Random.Range(0, size_ * size_);

            // Prevent swapping the last move
            if (random == prev) { continue; }
            prev = empty_location_;

            // Use the same algorithm used to swap as the player would
            if (SwapIfValid(random, -size_, size_)) { ++count; }
            else if (SwapIfValid(random, size_, size_)) { ++count; }
            else if (SwapIfValid(random, -1, 0)) { ++count; }
            else if (SwapIfValid(random, 1, size_ - 1)) { ++count; }
        }
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EntryCondition()
    {
        gameObject.SetActive(true);

        pieces_ = new List<Transform>();
        size_ = 3;
        CreateGamePieces(0.01f);

        StartCoroutine(WaitShuffle(0.5f));
    }

    #region Update
    // Update is called once per frame
    // Use to actually move the pieces
    public void Puzzle()
    {
        // On click, send out a ray that detects if we clicked a piece
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                Debug.Log("Hit Something");
                // Traverse list to get which tile we clocked
                // The number of tiles is small so O(n) is neglible lol
                Debug.Log(pieces_);
                for (int tile = 0; tile < pieces_.Count; ++tile)
                {
                    // Check for valid moves in each direction
                    if (pieces_[tile] == hit.transform)
                    {
                        Debug.Log("Hit a tile");
                        if (SwapIfValid(tile, -size_, size_)) { break; }
                        if (SwapIfValid(tile, size_, size_)) { break; }
                        if (SwapIfValid(tile, -1, 0)) { break; }
                        if (SwapIfValid(tile, 1, size_ - 1)) { break; }
                        else if (SwapIfValid(tile, size_, size_)) { break; }
                        else if (SwapIfValid(tile, -1, 0)) { break; }
                        else if (SwapIfValid(tile, 1, size_ - 1)) { break; }
                    }
                }

                // Check for completion, if completed shuffle again
                if (!shuffling_ && CheckCompletion())
                {
                    shuffling_ = true;
                }
            }
        }

    }
    #endregion

    #region WaitToReveal
    private IEnumerator WaitToReveal()
    {
        Debug.Log("Show puzzle!");
        pieces_[empty_location_].gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
    }
    #endregion
}
