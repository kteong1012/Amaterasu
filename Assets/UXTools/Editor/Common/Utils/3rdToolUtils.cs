using System.Diagnostics;

/// <summary>
/// �򵥵�Unity Python ����
/// </summary>
public static class PythonUtils
{
    public static void CallPython_Script(string pyScriptPath)
    {
        DataReceivedEventHandler handler = new DataReceivedEventHandler(ReceivePython_Script);
        CallPythonBase(pyScriptPath, handler);
    }

    public static void ReceivePython_Script(object sender, DataReceivedEventArgs e)
    {
        // �����Ϊ�ղŴ�ӡ
        if (string.IsNullOrEmpty(e.Data) == false)
        {
            UnityEngine.Debug.Log(e.Data);
        }
    }

    /// <summary>
    /// Unity ���� Python
    /// </summary>
    /// <param name="pyScriptPath">python �ű�·��</param>
    /// <param name="handler">�������ί��</param>
    /// <param name="argvs">python ��������</param>
    public static void CallPythonBase(string pyScriptPath, DataReceivedEventHandler handler, params string[] argvs)
    {
        Process process = new Process();

        // python �Ľ�����λ�� python.exe
        process.StartInfo.FileName = @"C:\DevelopTools\Python\Python38\python.exe";

        pyScriptPath = @"C:\Project_Compony\UXTools\UXTools2022\UXTools\Assets\Tools\ImageMatch\main.py";
        if (argvs != null)
        {
            foreach (string item in argvs)
            {
                pyScriptPath += " " + item;
            }
        }
        UnityEngine.Debug.Log(pyScriptPath);

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Arguments = pyScriptPath;     // ·��+����
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;        // ����ʾִ�д���

        // ��ʼִ�У���ȡִ���������ӽ�����ί��
        process.Start();
        process.BeginOutputReadLine();
        process.OutputDataReceived += handler;
        process.WaitForExit();
    }
    public static void CallPython_MatchDesignImage(string pyScriptPath, DataReceivedEventHandler receivedFunc ,string designImgPath, string templateFolderPath)
    {
        DataReceivedEventHandler handler = new DataReceivedEventHandler(receivedFunc);
        CallPythonBase(pyScriptPath, handler, designImgPath, templateFolderPath);
    }
}

/// <summary>
/// �򵥵�Unity ExeӦ�ó��� ����
/// </summary>
public static class ExeUtils
{
    public static void CallExe(string pyScriptPath)
    {
        DataReceivedEventHandler handler = new DataReceivedEventHandler(ReceiveExeResponse);
        CallExeBase(pyScriptPath, handler);
    }

    public static void ReceiveExeResponse(object sender, DataReceivedEventArgs e)
    {
        // �����Ϊ�ղŴ�ӡ
        if (string.IsNullOrEmpty(e.Data) == false)
        {
            UnityEngine.Debug.Log(e.Data);
        }
    }

    /// <summary>
    /// Unity ���� ExeӦ�ó���
    /// </summary>
    /// <param name="exePath">exe ·��</param>
    /// <param name="handler">�������ί��</param>
    /// <param name="argvs">����</param>
    public static void CallExeBase(string exePath, DataReceivedEventHandler handler, params string[] argvs)
    {
        Process process = new Process();

        exePath = @"C:\Project_Compony\UXTools\UXTools2022\UXTools\Assets\Tools\ImageMatch\main.exe";
        process.StartInfo.FileName = exePath;

        string argumentsStr = "";
        if (argvs != null)
        {
            foreach (string item in argvs)
            {
                argumentsStr += " " + item;
            }
        }
        UnityEngine.Debug.Log(argumentsStr);

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Arguments = argumentsStr;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;    // ����ʾִ�д���

        // ��ʼִ�У���ȡִ���������ӽ�����ί��
        process.Start();
        process.BeginOutputReadLine();
        process.OutputDataReceived += handler;
        process.WaitForExit();
    }
    public static void CallExe_MatchDesignImage(string exePath, DataReceivedEventHandler receivedFunc, string designImgPath, string templateFolderPath)
    {
        DataReceivedEventHandler handler = new DataReceivedEventHandler(receivedFunc);
        CallExeBase(exePath, handler, designImgPath, templateFolderPath);
    }
}