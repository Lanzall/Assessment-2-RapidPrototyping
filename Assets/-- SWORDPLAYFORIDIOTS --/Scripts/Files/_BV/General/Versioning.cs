using UnityEditor;
using UnityEngine;

public class Versioning : MonoBehaviour
{
    [SerializeField] private string prefix = "Version ";
    private string version => prefix + Application.version.ToString();
    private void Start()
    {
        SetVersion();
    }

    private void SetVersion()
    {
        if (GetComponent<UnityEngine.UI.Text>() != null)
            GetComponent<UnityEngine.UI.Text>().text = version;
        if (GetComponent<TMPro.TMP_Text>() != null)
            GetComponent<TMPro.TMP_Text>().text = version;
    }

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(Versioning))]
    public class VersioningEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Versioning versioning = (Versioning)target;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Version"))
            {
                versioning.SetVersion();
                EditorUtility.SetDirty(versioning);
            }
            GUILayout.EndHorizontal();
            DrawDefaultInspector();
        }
    }
#endif
    #endregion
}
