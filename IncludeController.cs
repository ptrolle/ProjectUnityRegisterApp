using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class IncludeController : MonoBehaviour 
{

	#region UI
	//P/ DATA ATUAL
	public InputField Iday;
	public InputField Iyear;

	//P/ DESCRICAO
	public InputField Idescription;

	//p/ VALOR
	public InputField Ivalue;

	//P/ OPCAO CARTAO, DEBITO OU DINHEIRO <ainda nao esta em desenvolvimento>
	/*public Toggle Tdi;
	public Toggle Tcc;
	public Toggle Tdb;*/

	//P/ DEFINIR CATEGORIA, TIPO (COMIDA, COMBUSTIVEL...)
	public Dropdown Dcategory;
	//P/ DEFINIR MES ATUAL
	public Dropdown Dmonth;

    public Text Tinformation;

    public AudioSource sound;
    public AudioClip click;

    private string monthController;
    #endregion

    //para controlar tempo de popup na tela
    private float timeController;

    void Start () 
	{
		Debug.Log(System.DateTime.Today); //mostra data atual pega no sistema
		Idescription.characterLimit = 14;
		Iday.characterLimit = 2;
		Iyear.characterLimit = 4;
		Ivalue.characterLimit = 9;

		//MESES EM DROPDOWN
		Dmonth.ClearOptions();
		List<string> monthsList = new List<string>{"JANEIRO", "FEVEREIRO", "MARÇO", "ABRIL", "MAIO", "JUNHO", "JULHO", "AGOSTO",
		"SETEMBRO","OUTUBRO", "NOVEMBRO", "DEZEMBRO"};
		Dmonth.AddOptions(monthsList);



		Iday.text = System.DateTime.Today.Day.ToString(); //pega o dia atual pelo sistema
		Iyear.text = System.DateTime.Today.Year.ToString(); //pega o ano atual pelo sistema
		Dmonth.value = (System.DateTime.Today.Month - 1); //para o drop pegar o atual mes pelo sistema


		//CATEGORIAS RECEBE OS VALORES CONTIDOS NA LISTA DE DATASAVE
		Dcategory.ClearOptions();
		Dcategory.AddOptions(DataSave.listCategory);
	}

	//SALVA DADOS VERIFICADOS
	public void BtnSaveDatas()
	{
        sound.PlayOneShot(click, 0.05f);

		monthController = (Dmonth.value + 1).ToString(); //coloca no mes o valor selecionado no dropdown
		Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>:::"+monthController);

		if(Idescription.text == "") //caso descricao vazia coloca "-----" para salvar
		{
			Idescription.text = "S/Descrição";
		}

		//caso dados corretos salvar
		if(Iyear.text == "" || Ivalue.text == "")
		{
			Debug.Log("Dados incorretos, nao foi possivel salvar!!!");
		}
		else
		{

            //SALVA DADOS
            //Debug.Log("salvou o valor!!!");
            string tempDate = monthController + Iyear.text;

            timeController = 0; //para controlar tempo de Tinformation;

            float __checkNumb; //var local para testar se valor em Ivalue pode ser considerado numero
            if(float.TryParse(Ivalue.text.Replace(',', '.'), out __checkNumb))
            {
               
                Debug.Log("SALVOU VALOR!!!");
                Tinformation.color = Color.green;
                Tinformation.text = "Valor Salvo";

                //					pega data / acessa no dropdown o atual nome selecionado /   valor colocado /                     descrição do gasto;
                //utilizo o text.replace pois quando colocava o numero com virgula o float nao reconhecia desta maneira
                DataSave.SaveValues(tempDate, Dcategory.options[Dcategory.value].text, __checkNumb, Idescription.text);

                Idescription.text = ""; //reseta texto de descrição
                Ivalue.text = ""; //reseta casa onde esta o valor a ser salvo

                //mostra propaganda a cada 10 itens salvos
                if (PlayerPrefs.GetInt("showAds") >= 10)
                {
                    ShowAd(); 
                    PlayerPrefs.SetInt("showAds", 0);
                }
                else
                {
                    //adiciona + 1 item salvo ate 10 para ads
                    int auxAds = PlayerPrefs.GetInt("showAds");
                    auxAds++; // add + 1
                    PlayerPrefs.SetInt("showAds", auxAds); //salva novo valor
                }
            }
            else
            {
                Debug.Log("nao foi possivel salvar este valor");
                Tinformation.color = Color.red;
                Tinformation.text = "Ocorreu um erro, casa dos milhares digite neste formato 1000,00";
            }
		}
	}

    public void Update()
    {
        timeController += Time.deltaTime;

        if (timeController > 1.5f) //mais de 1.5 seg apaga mensagem Tinformation;
        {
            Tinformation.text = "";
        }
    }

    //para mostrar propaganda
    public void ShowAd()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
}
