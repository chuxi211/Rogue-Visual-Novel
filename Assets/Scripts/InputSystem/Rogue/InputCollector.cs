using UnityEngine;
using UnityEngine.SceneManagement;
namespace Input
{

    public class InputCollector : MonoBehaviour
    {
        [SerializeField]
        private Camera MainCamera;
        private InputManager InputManager;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            BindMainCamera();
        }
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
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
        public void Bind(InputManager manager)
        {
            if (manager == null)
            {
                Debug.LogError(
                    "InputCollector failed to bind: InputManager is null.");
                return;
            }

            InputManager = manager;
        }
        private void CollectLeftClick()
        {
            if (MainCamera == null)
            {
                Debug.LogError("InputCollector: MainCamera is null.");
                return;
            }

            Ray ray = MainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hitInfo))
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
            if (InputManager == null)
            {
                Debug.LogError("InputCollector: InputManager is null.");
                return;
            }

            InputManager.InputStateMachine.Push(command);
        }
    }
}
