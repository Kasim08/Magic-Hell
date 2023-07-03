using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TowerScript))]
public class TowerScriptEditor : Editor
{
    // Declare the serialized properties
    private SerializedProperty scanTransform;
    private SerializedProperty enemysTransforms;
    private SerializedProperty sphereRadius;
    private SerializedProperty maxDistance;
    private SerializedProperty fireTransform;
    private SerializedProperty bulletPrefab;
    private SerializedProperty shotForce;
    private SerializedProperty fireAngle;
    private SerializedProperty layer;
    private SerializedProperty fireRate;
    private SerializedProperty towerType;
    private SerializedProperty targetMethod;
    private SerializedProperty selectionMode;
    private SerializedProperty calculationMethod;

    private void OnEnable()
    {
        // Find the serialized properties
        scanTransform = serializedObject.FindProperty("scanTransform");
        enemysTransforms = serializedObject.FindProperty("enemysTransforms");
        sphereRadius = serializedObject.FindProperty("sphereRadius");
        maxDistance = serializedObject.FindProperty("maxDistance");
        fireTransform = serializedObject.FindProperty("fireTransform");
        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
        shotForce = serializedObject.FindProperty("shotForce");
        fireAngle = serializedObject.FindProperty("fireAngle");
        layer = serializedObject.FindProperty("layer");
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

        EditorGUILayout.PropertyField(fireTransform);
        EditorGUILayout.PropertyField(bulletPrefab);
        EditorGUILayout.PropertyField(fireRate);

        EditorGUILayout.PropertyField(scanTransform);
        EditorGUILayout.PropertyField(sphereRadius);
        EditorGUILayout.PropertyField(layer);


        // Tower Type deðerine göre farklý deðiþkenleri gösterin veya gizleyin
        switch (towerType.enumValueIndex)
        {
            case (int)TowerScript.TowerType.Bullet:
                // Eðer Tower Type Bullet ise, Fire Transform, Bullet Prefab ve Shot Force deðiþkenlerini gösterin
                //EditorGUILayout.PropertyField(fireTransform);
                //EditorGUILayout.PropertyField(bulletPrefab);
                EditorGUILayout.PropertyField(shotForce);
                break;
            case (int)TowerScript.TowerType.Projectile:
                // Eðer Tower Type Projectile ise, Fire Transform, Bullet Prefab, Shot Force ve Fire Angle deðiþkenlerini gösterin
                EditorGUILayout.PropertyField(calculationMethod);
                //EditorGUILayout.PropertyField(fireTransform);
                //EditorGUILayout.PropertyField(bulletPrefab);
                break;
        }

        // Target Method deðerine göre farklý deðiþkenleri gösterin veya gizleyin
        switch (targetMethod.enumValueIndex)
        {
            case (int)TowerScript.TargetMethod.SphereCast:
                //EditorGUILayout.PropertyField(scanTransform);
                //EditorGUILayout.PropertyField(sphereRadius);
                EditorGUILayout.PropertyField(maxDistance);
                //EditorGUILayout.PropertyField(layer);
                break;
            case (int)TowerScript.TargetMethod.OverlapSphere:
                //EditorGUILayout.PropertyField(scanTransform);
                //EditorGUILayout.PropertyField(sphereRadius);
                //EditorGUILayout.PropertyField(layer);
                break;
            case (int)TowerScript.TargetMethod.Random:
                EditorGUILayout.PropertyField(enemysTransforms);
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