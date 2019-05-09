using UnityEngine;

namespace Tetris
{
    public class SpawnerScript : MonoBehaviour
    {
        #region SpawnerScrip fields

        [SerializeField] private GameObject[] tetrisObjects;

        private GameObject _previewObject;
        private GameObject _nextObject;
        private Transform _anchor;

        private readonly Vector3 previewPosition = new Vector3(0, 0, 0);
               
        public GameObject NextObject { get => _nextObject; }
        public GameObject PreviewObject { get => _previewObject; }
        public Transform Anchor { set => _anchor = value; }
        #endregion

        public void SpawnFirstObject()
        {
            _nextObject = Instantiate
                (
                    tetrisObjects[Random.Range(0, tetrisObjects.Length)],
                    transform.position,
                    Quaternion.identity
                );

            _nextObject.transform.parent = _anchor;
            _nextObject.transform.rotation = _anchor.rotation;
            _nextObject.transform.localScale = new Vector3(1, 1, 1);

            _previewObject = Instantiate
                (
                    tetrisObjects[Random.Range(0,
                    tetrisObjects.Length)],
                    previewPosition,
                    Quaternion.identity
                );

            _previewObject.transform.parent = _anchor;
            _previewObject.transform.rotation = _anchor.rotation;
            _previewObject.GetComponent<TetrisObject>().enabled = false;
            _previewObject.SetActive(false);
            _previewObject.transform.localScale = new Vector3(1, 1, 1);
        }

        public void SpawnNextObject()
        {
            _previewObject.transform.position = transform.position;

            _nextObject = _previewObject;
            _nextObject.SetActive(true);
            _nextObject.GetComponent<TetrisObject>().enabled = true;

            _previewObject = Instantiate
                (
                    tetrisObjects[Random.Range(0, tetrisObjects.Length)],
                    previewPosition,
                    Quaternion.identity
                );
            _previewObject.transform.parent = _anchor;
            _previewObject.transform.rotation = _anchor.rotation;
            _previewObject.GetComponent<TetrisObject>().enabled = false;
            _previewObject.SetActive(false);
            _previewObject.transform.localScale = new Vector3(1, 1, 1);
        }

        public void ClearObjects()
        {
            Destroy(_nextObject);
            Destroy(_previewObject);
            _nextObject = null;
            _previewObject = null;

        }
    }
}