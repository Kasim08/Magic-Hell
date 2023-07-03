// CustomEditor attribute'ünü kullanarak, bu sýnýfýn TowerData sýnýfýnýn custom editor'ü olduðunu belirtin
using UnityEditor;

[CustomEditor(typeof(TowerData))]
public class TowerDataEditor : Editor
{
    // Declare the serialized properties
    private SerializedProperty sphereRadius;
    private SerializedProperty maxDistance;
    private SerializedProperty bulletPrefab;
    private SerializedProperty shotForce;
    private SerializedProperty fireAngle;
    private SerializedProperty enemyLayer;
    private SerializedProperty fireRate;
    private SerializedProperty towerType;
    private SerializedProperty targetMethod;
    private SerializedProperty selectionMode;
    private SerializedProperty calculationMethod;

    private void OnEnable()
    {
        // Find the serialized properties
        sphereRadius = serializedObject.FindProperty("sphereRadius");
        maxDistance = serializedObject.FindProperty("maxDistance");
        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
        shotForce = serializedObject.FindProperty("shotForce");
        fireAngle = serializedObject.FindProperty("fireAngle");
        enemyLayer = serializedObject.FindProperty("enemyLayer");
        fireRate = serializedObject.FindProperty("fireRate");
        towerType = serializedObject.FindProperty("towerType");
        targetMethod = serializedObject.FindProperty("targetMethod");
        selectionMode = serializedObject.FindProperty("selectionMode");
        calculationMethod = serializedObject.FindProperty("calculationMethod");
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Draw the custom inspector
        EditorGUILayout.PropertyField(towerType);
        EditorGUILayout.PropertyField(targetMethod);
        EditorGUILayout.PropertyField(selectionMode);

        EditorGUILayout.PropertyField(bulletPrefab);
        EditorGUILayout.PropertyField(fireRate);

        EditorGUILayout.PropertyField(sphereRadius);
        EditorGUILayout.PropertyField(enemyLayer);

        // Tower Type deðerine göre farklý deðiþkenleri gösterin veya gizleyin
        switch (towerType.enumValueIndex)
        {
            case (int)TowerScript.TowerType.Bullet:
                //EditorGUILayout.PropertyField(bulletPrefab);
                EditorGUILayout.PropertyField(shotForce);
                break;
            case (int)TowerScript.TowerType.Projectile:
                EditorGUILayout.PropertyField(calculationMethod);
                //EditorGUILayout.PropertyField(bulletPrefab);
                break;
        }

        // Target Method deðerine göre farklý deðiþkenleri gösterin veya gizleyin
        switch (targetMethod.enumValueIndex)
        {
            case (int)TowerScript.TargetMethod.SphereCast:
                //EditorGUILayout.PropertyField(sphereRadius);
                EditorGUILayout.PropertyField(maxDistance);
                //EditorGUILayout.PropertyField(layer);
                break;
            case (int)TowerScript.TargetMethod.OverlapSphere:
                //EditorGUILayout.PropertyField(sphereRadius);
                //EditorGUILayout.PropertyField(layer);
                break;
            case (int)TowerScript.TargetMethod.Random:
                break;
        }

        switch (calculationMethod.enumValueIndex)
        {
            case (int)TowerScript.CalculationMethod.CalculateProjectileVelocity:
                EditorGUILayout.PropertyField(fireAngle);
                break;
            case (int)TowerScript.CalculationMethod.CalculateProjectileAngle:
                EditorGUILayout.PropertyField(shotForce);
                break;
        }

        // Deðiþiklikleri uygulamak için serializedObject.ApplyModifiedProperties() metodunu çaðýrýn
        serializedObject.ApplyModifiedProperties();
    }
}

