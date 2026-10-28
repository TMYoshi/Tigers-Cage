using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public enum CursorState
    {
        Idle,
        Hover
    }

    [System.Serializable]
    public class CursorStateSprites
    {
        public CursorState state;
        public Sprite sprite;
    }

    public static CursorManager Instance;
    [SerializeField] Transform cursorTransform;
    [SerializeField] Image cursorAppearance;
    [SerializeField] List<CursorStateSprites> cursorImages;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        cursorTransform.position = PlayerInput.Instance.MouseInput;
    }

    public void UpdateToState(CursorState _state)
    {
        cursorAppearance.sprite = cursorImages
                                    .Where(x => x.state == _state)
                                    .FirstOrDefault()
                                    .sprite;
    }
}
