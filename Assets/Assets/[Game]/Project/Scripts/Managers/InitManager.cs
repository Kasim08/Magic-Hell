using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        //PlayerPrefs.DeleteAll();//Oyun ba�larken verilerin s�f�rlanmas� isteniyor ise.
        
        //Init Game Here   
        yield return SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);

        /*
        //PlayerPrefs 2 veri al�yor ilk olan istedi�imiz e�er yok ise 2. olan default olark al�n�r. E�er Oyun a��l�rken Menu ile beraber Default bir level y�klenmek istenirse.
        //Mini oyunlar HyperCasual oyunlar ve tek level e sahip oyunlar i�in kullan�lmas� mant�kl� olur.
        yield return SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LastLevel", "Level01"), LoadSceneMode.Additive);
        // Etkin sahneyi belirler. Sahne ���klar� aktif etkin olmas� a��s�ndn �nemli.
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerPrefs.GetString("LastLevel", "Level01"))); 
        */

        //EventManager.OnBangBangGame.Invoke();

        Destroy(gameObject);
    }
}