using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerDelete : MonoBehaviour
{

    public Text Tinformation;

    public AudioSource sound;
    public AudioClip click;

    //para controlar tempo de popup na tela
    private float timeController;

    public void BtnDeleteAllDatas()
    {
        sound.PlayOneShot(click, 0.05f);

        timeController = 0; //para controlar tempo de Tinformation;
        Tinformation.color = Color.green;
        Tinformation.text = "Deletou todos os dados";

        Debug.Log("deletou todos os dados");
        DataSave.listCategory.Clear();
        PlayerPrefs.DeleteAll();

        //colocando valores originais na lista
        DataSave.SaveDatasCategory(0);

        PlayerPrefs.SetInt("firstTime", 1);
    }

	void Update ()
    {

        timeController += Time.deltaTime;

        if (timeController > 1.5f) //mais de 1.5 seg apaga mensagem Tinformation;
        {
            Tinformation.text = "";
        }

    }
}
