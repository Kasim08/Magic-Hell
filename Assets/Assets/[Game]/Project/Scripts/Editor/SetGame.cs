using System.Collections; // Sistem koleksiyonları için kütüphane
using System.Collections.Generic; // Sistem koleksiyonları için genel kütüphane
using UnityEditor; // Unity editörü için kütüphane
using UnityEngine; // Unity motoru için kütüphane
using UnityEngine.SceneManagement; // Unity sahne yönetimi için kütüphane

[InitializeOnLoad] // Sınıf yüklendiğinde çalıştırılacak bir öznitelik
public static class SetGame // SetGame adında bir statik sınıf tanımı
{
    // register an event handler when the class is initialized

    static SetGame() // Sınıfın statik yapıcı metodu
    {
        EditorApplication.playModeStateChanged += LogPlayModeState; // Oyun modu değiştiğinde LogPlayModeState fonksiyonunu çağıracak bir olay işleyicisi kaydı
    }

    private static void LogPlayModeState(PlayModeStateChange state) // Oyun modu değiştiğinde çalışacak statik ve özel bir fonksiyon tanımı
    {
        bool hasInit = false; // hasInit adında bir mantıksal değişken tanımı ve başlangıç değeri olarak false ataması


        if (PlayModeStateChange.EnteredPlayMode == state) // Eğer oyun moduna girildiyse
        {
            int sceneCount = SceneManager.sceneCount; // sceneCount adında bir tam sayı değişkeni tanımı ve değer olarak sahne sayısını ataması

            for (int i = 0; i < sceneCount; i++) // Sahne sayısı kadar dönecek bir döngü başlatması
            {

                if (SceneManager.GetSceneAt(i).buildIndex == 0) // Eğer i. sahnenin yapım indeksi 0 ise
                {
                    hasInit = true; // hasInit değişkenini true yapması

                }
            }
            if (!hasInit) // Eğer hasInit değişkeni false ise
            {
                SceneManager.LoadScene(0); // 0. indeksteki sahneyi yüklemesi
            }
        }
    }
}