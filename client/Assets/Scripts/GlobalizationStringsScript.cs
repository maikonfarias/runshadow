/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GlobalizationStringsScript : MonoBehaviour
{

    static SystemLanguage currenSystemtLanguage;

    const string pt_BR = "pt_BR";
    const string es_ES = "es_ES";
    const string ja_JP = "ja_JP";
    const string zh_CN = "zh_CN";
    const string zh_TW = "zh_TW";
    const string en_US = "en_US";    

    static Dictionary<string, string> data = new Dictionary<string, string>();

    void Start()
    {

    }

    public static Dictionary<string, string> GetLanguageList()
    {
        var list = new Dictionary<string, string>();
        list.Add(en_US, "English");
        list.Add(pt_BR, "Português");
        list.Add(es_ES, "Español");
        list.Add(ja_JP, "日本の");
        list.Add(zh_CN, "中文(简体)");
        list.Add(zh_TW, "中文(繁體)");
        return list;
    }

    static GlobalizationStringsScript()
    {
        UpdateLanguageData();
    }

    static public string GetString(string key)
    {
        if (currenSystemtLanguage != Application.systemLanguage)
        {
            currenSystemtLanguage = Application.systemLanguage;

            if (GetPrefLanguage() == "")
            {
                UpdateLanguageData();
            }
        }
        // TODO if dont find key find it in english, if not return key
        if (data.ContainsKey(key))
        {
            return data[key];
        }
        else
        {
            if (GetCurrentLanguage() != en_US)
            {
                Debug.LogWarning("No key '" + key + "' in language '" + GetCurrentLanguage() + "'");
            }
            return key;
        }
    }


    public static string GetCurrentLanguageName()
    {
        string selectedLanguage = GetPrefLanguage();

        if (string.IsNullOrEmpty(selectedLanguage))
        {
            selectedLanguage = GetSystemLanguage();
        }
        var list = GetLanguageList();

        if (list.ContainsKey(selectedLanguage))
        {
            return list[selectedLanguage];
        }
        else
        {
            return selectedLanguage;
        }        
    }

    public static string GetSystemLanguageName()
    {
        var selectedLanguage = GetSystemLanguage();
        
        var list = GetLanguageList();

        if (list.ContainsKey(selectedLanguage))
        {
            //var tempData = SelectLanguageData(selectedLanguage);

            return string.Format("System ({0})", list[selectedLanguage]);
            //return list[selectedLanguage];
        }
        else
        {
            return selectedLanguage;
        }  
    }

    public static void SetPrefLanguage(string languageCode)
    {
        PlayerPrefs.SetString("SelectedLanguage", languageCode);
        UpdateLanguageData();
    }
    public static string GetPrefLanguage()
    {
        return PlayerPrefs.GetString("SelectedLanguage", "");
    }

    public static void UpdateLanguageData()
    {
        data = GetLanguageData();
    }

    static string GetCurrentLanguage()
    {
        string selectedLanguage = GetPrefLanguage();

        if (string.IsNullOrEmpty(selectedLanguage))
        {
            selectedLanguage = GetSystemLanguage();
        }

        return selectedLanguage;
    }

    static Dictionary<string, string> GetLanguageData()
    {

       string selectedLanguage = GetCurrentLanguage();
        /* // get language from android, working but not fully tested
             // && !UNITY_EDITOR
            var androidLocale = new AndroidJavaClass("java.util.Locale");
            var resultTest = androidLocale.CallStatic<AndroidJavaObject>("getDefault");
            string res = resultTest.Call<string>("toString");
            //androidLocale.getDefault().getLanguage();
            var translation = new Dictionary<string, string>();
            translation.Add("Start", res);
            return translation;
              

        return SelectLanguageData(selectedLanguage);

    }

    static Dictionary<string, string> SelectLanguageData(string selectedLanguage)
    {
        switch (selectedLanguage)
        {
            case pt_BR:
                return GetPtBrLanguage();
            case es_ES:
                return GetEsEsLanguage();
            case ja_JP:
                return GetJaJpLanguage();
            case zh_CN:
                return GetZhCnLanguage();
            case zh_TW:
                return GetZhTwLanguage();
            default:
                return GetEnUsLanguage();
        }
    }

    static string GetSystemLanguage()
    {
        switch (Application.systemLanguage) 
        {
            case SystemLanguage.Portuguese:
                return pt_BR;
            case SystemLanguage.Spanish:
                return es_ES;
            case SystemLanguage.Japanese:
                return ja_JP;
            case SystemLanguage.Chinese:
                return zh_CN;
            default:
                return en_US;
        }
    }

    static Dictionary<string, string> GetEnUsLanguage()
    {
        var translation = new Dictionary<string, string>();
        translation.Add("System Language", GetSystemLanguageName());
        translation.Add("System", "System (English)");
        return translation;
    }

    static Dictionary<string, string> GetPtBrLanguage()
    {
        var translation = new Dictionary<string, string>();

        translation.Add("System Language", GetSystemLanguageName());

        translation.Add("RUN SHADOW", "CORRA SOMBRA");
        translation.Add("Start", "Jogar");
        translation.Add("Space/Touch/Click = Jump (Double Jump)", "Espaço/Toque na tela/Click = Pular (Pulo Duplo)");
        translation.Add("Esc/Back = Pause/Menu", "Esc/Voltar = Pausa/Menu");
        translation.Add("R = Restart Level", "R = Recomeçar a partida");
        
        translation.Add("Shadow", "Sombra");
        translation.Add("Human", "Humano");

        translation.Add("Sound ON", "Som Ligado");
        translation.Add("Sound OFF", "Som Desligado");
        translation.Add("Quit", "Sair");

        translation.Add("Score", "Pontuação");
        translation.Add("Pause", "Pausar");
        translation.Add("GAME PAUSED", "JOGO PAUSADO");
        translation.Add("Continue", "Continuar");
        translation.Add("Restart", "Recomeçar");

        translation.Add("GAME OVER", "FIM DE JOGO");
        translation.Add("Retry?", "Denovo?");
        translation.Add("Main Menu", "Menu Principal");
        translation.Add("Cancel", "Cancelar");

        translation.Add("Time", "Tempo");
        translation.Add("Rubies", "Rubis");
        translation.Add("Options", "Opções");
        translation.Add("Normal Map", "Mapa Normal");
        translation.Add("Black Map", "Mapa Preto");
        translation.Add("Back", "Voltar");

        translation.Add("Error", "Erro");
        translation.Add("Loading...", "Carregando...");
        translation.Add("Refresh", "Atualizar");
        translation.Add("Scoreboard", "Placar");
        translation.Add("Sent", "Enviado");
        translation.Add("Sending...", "Enviando...");
        translation.Add("Submit Score", "Enviar Pontos");

        return translation;
    }

    static Dictionary<string, string> GetEsEsLanguage()
    {
        var translation = new Dictionary<string, string>();

        translation.Add("System Language", GetSystemLanguageName());

        translation.Add("RUN SHADOW", "CORRA SOMBRA");
        translation.Add("Start", "Jugar");
        translation.Add("Space/Touch/Click = Jump (Double Jump)", "Espacio/Touch/Clic = Saltar (Doble Salto)");
        translation.Add("Esc/Back = Pause/Menu", "Esc/Volver = Pausa/Menú");
        translation.Add("R = Restart Level", "R = Reiniciar nivel");

        translation.Add("Shadow", "Sombra");
        translation.Add("Human", "Humano");

        translation.Add("Sound ON", "Sonido ON");
        translation.Add("Sound OFF", "Sonido OFF");
        translation.Add("Quit", "Salir");

        translation.Add("Score", "Puntuación");
        translation.Add("Pause", "Pausar");
        translation.Add("GAME PAUSED", "JUEGO EN PAUSA");
        translation.Add("Continue", "Continuar");
        translation.Add("Restart", "Reiniciar");

        translation.Add("GAME OVER", "FIN DEL JUEGO");
        translation.Add("Retry?", "Reintentar?");
        translation.Add("Main Menu", "Menú Principal");
        translation.Add("Cancel", "Cancelar");

        translation.Add("Time", "Tiempo");
        translation.Add("Rubies", "Rubíes");
        translation.Add("Options", "Opciones");
        translation.Add("Normal Map", "Mapa Normal");
        translation.Add("Black Map", "Mapa Negro");
        translation.Add("Back", "Volver");

        translation.Add("Error", "Erro");
        translation.Add("Loading...", "Cargando...");
        translation.Add("Refresh", "Actualizar");
        translation.Add("Scoreboard", "Marcador");
        translation.Add("Sent", "Enviado");
        translation.Add("Sending...", "Enviando...");
        translation.Add("Submit Score", "Enviar Puntuación");

        return translation;
    }
    static Dictionary<string, string> GetJaJpLanguage()
    {
        var translation = new Dictionary<string, string>();

        translation.Add("System Language", GetSystemLanguageName());

        translation.Add("RUN SHADOW", "影を実行");
        translation.Add("Start", "スタート");
        translation.Add("Space/Touch/Click = Jump (Double Jump)", "SPACE/タッチ/クリック = ジャンプ（ダブルジャンプ）");
        translation.Add("Esc/Back = Pause/Menu", "ESC /戻る = 一時停止/メニュー");
        translation.Add("R = Restart Level", "R=再起動レベル");

        translation.Add("Shadow", "シャドウ");
        translation.Add("Human", "人間");

        translation.Add("Sound ON", "音 ON");
        translation.Add("Sound OFF", "音 OFF");
        translation.Add("Quit", "ゲームを終了");

        translation.Add("Score", "スコア");
        translation.Add("Pause", "一時停止");
        translation.Add("GAME PAUSED", "一時停止");
        translation.Add("Continue", "続ける");
        translation.Add("Restart", "再起動");

        translation.Add("GAME OVER", "ゲームオーバー");
        translation.Add("Retry?", "リトライ");
        translation.Add("Main Menu", "開始画面");
        translation.Add("Cancel", "取り消す");

        translation.Add("Time", "時間");
        translation.Add("Rubies", "ルビー");
        translation.Add("Options", "オプション");
        translation.Add("Normal Map", "正規図");
        translation.Add("Black Map", "ブラック地図");
        translation.Add("Back", "戻る");

        translation.Add("Error", "エラー");
        translation.Add("Loading...", "荷重...");
        translation.Add("Refresh", "リフレッシュ");
        translation.Add("Scoreboard", "スコアボード");
        translation.Add("Sent", "送信済み");
        translation.Add("Sending...", "送信...");
        translation.Add("Submit Score", "スコアを提出");

        return translation;
    }

    static Dictionary<string, string> GetZhCnLanguage()
    {
        var translation = new Dictionary<string, string>();
        
        translation.Add("System Language", GetSystemLanguageName());

        translation.Add("RUN SHADOW", "跑阴影");
        translation.Add("Start", "开始游戏");
        translation.Add("Space/Touch/Click = Jump (Double Jump)", "空格键/触摸/点击 = 跳转（双跳）");
        translation.Add("Esc/Back = Pause/Menu", "Esc键/返回 = 暂停/菜单");
        translation.Add("R = Restart Level", "R=重置关卡");
        //Play as Shadow
        translation.Add("Shadow", "阴影");
        translation.Add("Human", "人的");

        translation.Add("Sound ON", "声音开");
        translation.Add("Sound OFF", "声音关闭");
        translation.Add("Quit", "退出");

        translation.Add("Score", "得分");
        translation.Add("Pause", "暂停");
        translation.Add("GAME PAUSED", "游戏暂停");
        translation.Add("Continue", "继续");
        translation.Add("Restart", "重试");

        translation.Add("GAME OVER", "游戏结束了");
        translation.Add("Retry?", "重试？");
        translation.Add("Main Menu", "主菜单");
        translation.Add("Cancel", "取消");

        translation.Add("Time", "时间");
        translation.Add("Rubies", "红宝石");
        translation.Add("Options", "选项");
        translation.Add("Normal Map", "法线贴图");
        translation.Add("Black Map", "黑色地图");
        translation.Add("Back", "回去");

        translation.Add("Error", "错误");
        translation.Add("Loading...", "载入中...");
        translation.Add("Refresh", "刷新");
        translation.Add("Scoreboard", "记分牌");
        translation.Add("Sent", "做过");
        translation.Add("Sending...", "信息邮寄...");
        translation.Add("Submit Score", "提交得分");

        return translation;
    }

    static Dictionary<string, string> GetZhTwLanguage()
    {
        var translation = new Dictionary<string, string>();

        translation.Add("System Language", GetSystemLanguageName());

        translation.Add("RUN SHADOW", "跑陰影");
        translation.Add("Start", "開始遊戲");
        translation.Add("Space/Touch/Click = Jump (Double Jump)", "空格鍵/觸摸/點擊 = 跳轉（雙跳");
        translation.Add("Esc/Back = Pause/Menu", "Esc鍵/返回 = 暫停/菜單");
        translation.Add("R = Restart Level", "R=重置關卡");
        //Play as Shadow
        translation.Add("Shadow", "陰影");
        translation.Add("Human", "人的");

        translation.Add("Sound ON", "聲音開");
        translation.Add("Sound OFF", "聲音關閉");
        translation.Add("Quit", "退出");

        translation.Add("Score", "得分");
        translation.Add("Pause", "暫停");
        translation.Add("GAME PAUSED", "遊戲暫停");
        translation.Add("Continue", "繼續");
        translation.Add("Restart", "重試");

        translation.Add("GAME OVER", "遊戲結束");
        translation.Add("Retry?", "重試？");
        translation.Add("Main Menu", "主菜單");
        translation.Add("Cancel", "取消");

        translation.Add("Time", "時間");
        translation.Add("Rubies", "紅寶石");
        translation.Add("Options", "選項");
        translation.Add("Normal Map", "法線貼圖");
        translation.Add("Black Map", "黑色地圖");
        translation.Add("Back", "回去");

        translation.Add("Error", "錯誤");
        translation.Add("Loading...", "載入中...");
        translation.Add("Refresh", "刷新");
        translation.Add("Scoreboard", "記分牌");
        translation.Add("Sent", "發送");
        translation.Add("Sending...", "發出...");
        translation.Add("Submit Score", "提交得分");

        return translation;
    }

}
*/