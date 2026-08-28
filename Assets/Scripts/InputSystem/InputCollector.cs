using UnityEngine;
using UnityEngine.SceneManagement;

public class InputCollector : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    public static InputCollector Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        BindMainCamera();
    }
    private void OnDestroy()
    {
        if (Instance != this)
        {
            return;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
        Instance = null;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CollectLeftClick();
        }
    }
    private void OnSceneLoaded(Scene Scene, LoadSceneMode LoadSceneMode)
    {
        BindMainCamera();
    }
    private void BindMainCamera()
    {
        MainCamera = Camera.main;
        if (MainCamera == null)
        {
            Debug.LogError("InputCollector: MainCamera is null.");
        }
    }
    private void CollectLeftClick()
    {
        if (MainCamera == null)
        {
            Debug.LogError("InputCollector: MainCamera is null.");
            return;
        }

        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray,out RaycastHit hitInfo))
        {
            return;
        }
        Debug.Log(hitInfo.collider.transform.position);
        INodeClick nodeClick = hitInfo.collider.GetComponent<INodeClick>();
        if (nodeClick == null)
        {
            Debug.LogError("InputCollector: nodeClick is null");
            return;
        }
        InputCommand command = new InputCommand(nodeClick);
        if (InputManager.Instance == null)
        {
            Debug.LogError("InputCollector: InputManager.Instance is null.");
            return;
        }

        InputManager.Instance.InputStateMachine.Push(command);
    }
}
