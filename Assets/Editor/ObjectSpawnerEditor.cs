using UnityEngine;
using UnityEditor;

public class ObjectSpawnerEditor : EditorWindow
{
    private GameObject prefabToSpawn;
    private int numberOfObjects = 10;
    private float spawnRange = 10f;
    private string parentName = "ObjectSpawner";
    private Vector3 objectSpwanerPosition = new Vector3(0,1,0);
    [MenuItem("Tools/Object Spawner")]
    public static void ShowWindow()
    {
        GetWindow<ObjectSpawnerEditor>("Object Spawner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn Settings", EditorStyles.boldLabel);

        prefabToSpawn = (GameObject)EditorGUILayout.ObjectField("Prefab to Spawn", prefabToSpawn, typeof(GameObject), false);
        numberOfObjects = EditorGUILayout.IntField("Number of Objects", numberOfObjects);
        spawnRange = EditorGUILayout.FloatField("Spawn Range", spawnRange);
        parentName = EditorGUILayout.TextField("Parent Object Name", parentName);
        objectSpwanerPosition = EditorGUILayout.Vector3Field("Object Sapwner Parent Position", objectSpwanerPosition);

        if (GUILayout.Button("Spawn Objects"))
        {
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab is not assigned.");
            return;
        }

        GameObject parent = GameObject.Find(parentName);
        if (parent == null)
        {
            parent = new GameObject(parentName);
            parent.transform.position = objectSpwanerPosition;
        }

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0f,
                Random.Range(-spawnRange, spawnRange)
            );

            GameObject spawned = (GameObject)PrefabUtility.InstantiatePrefab(prefabToSpawn);
            spawned.transform.position = randomPosition;
            spawned.transform.parent = parent.transform;
        }

        Debug.Log($"{numberOfObjects} objects spawned under '{parentName}'.");
    }
}
