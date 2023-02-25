using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorController : MonoBehaviour 
{

	public InputField IvalueDelete;
	public InputField Iyear;

	public Text Tinformation; //texto informativo 

	public Dropdown Dcategory;
	public Dropdown Dmonth;

    public AudioSource sound;
    public AudioClip click;

    //para controlar tempo de popup na tela
    private float timeController;


    void Start () 
	{

		IvalueDelete.characterLimit = 9;
		Iyear.characterLimit = 4;

		//MESES EM DROPDOWN
		Dmonth.ClearOptions();
		List<string> monthsList = new List<string>{"JANEIRO", "FEVEREIRO", "MARÇO", "ABRIL", "MAIO", "JUNHO", "JULHO", "AGOSTO",
		"SETEMBRO","OUTUBRO", "NOVEMBRO", "DEZEMBRO"};
		Dmonth.AddOptions(monthsList);

		Dcategory.ClearOptions();
		Dcategory.AddOptions(DataSave.listCategory);

		Iyear.text = System.DateTime.Today.Year.ToString(); //pega o ano atual pelo sistema
		Dmonth.value = (System.DateTime.Today.Month - 1); //para o drop pegar o atual mes pelo sistema
	}

	public void DeleteValue()
	{
        sound.PlayOneShot(click, 0.05f); //som botao

		if(IvalueDelete.text == "")
		{
			Debug.Log("Dados incorretos, nao foi possivel deletar o valor da categoria!!!");
           
        }
		else
		{
            timeController = 0; //para controlar tempo de Tinformation;
            float __checkNumb; //var local para testar se valor em IvalueDelete pode ser considerado numero

            //testa primeiro verifica se tem virgula passando para ponto e depois se esta var é um numero
            if (float.TryParse(IvalueDelete.text.Replace(',','.') , out __checkNumb))
            {
                Tinformation.color = Color.green;
                Tinformation.text = "Subtraiu o valor digitado da categoria";

                __checkNumb = Mathf.Abs(__checkNumb); //pega o valor absoluto pois se o usuario colocar o sinal de menos
                Debug.Log("VERIFICANDO SE EH NUMERO QUE CONSTA: "+__checkNumb);

                Debug.Log("ok, valor de delete: " + __checkNumb);

                //Debug.Log(DmainCategory.options[DmainCategory.value].text);
                //auxKey para pegar key precisa     MES         +    ANO     +   CATEGORIA ATUAL NA CAIXA DE CATEGORIA
                string __auxKey = (Dmonth.value + 1).ToString() + Iyear.text + Dcategory.options[Dcategory.value].text;
                float __getValueCategory = PlayerPrefs.GetFloat(__auxKey); //pega o valor atual nesta categoria selecionado para diminuir
                float __newValue = __getValueCategory - __checkNumb; //diminui valor digitado

                if (__newValue < 0) //caso o valor que deseja deletar é maior do que possui na categoria e automaticamente colocado 0 e nao num negativo
                    __newValue = 0;

                PlayerPrefs.SetFloat(__auxKey, __newValue); //salva novo valor da categoria
                IvalueDelete.text = "";
            }
            else
            {
                Tinformation.color = Color.red;
                Tinformation.text = "Ocorreu um erro, casa dos milhares digite neste formato 1000,00";
                Debug.Log("NAO PASSOU NO TESTE DE NUMERO!!!!!!!!!!!");
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
}
