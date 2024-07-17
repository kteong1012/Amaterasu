using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using UnityEditor.IMGUI.Controls;

namespace ThunderFireUITool
{

    public class UICommonScriptCheckWindow : EditorWindow
    {
        private static UICommonScriptCheckWindow m_window;

        [MenuItem(ThunderFireUIToolConfig.Menu_ComponentCheck, false, 161)]
        public static void OpenWindow()
        {
            int width = 900;
            int height = 500;
            m_window = GetWindow<UICommonScriptCheckWindow>();
            m_window.minSize = new Vector2(width, height);
            m_window.titleContent.text = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_脚本检查标题);

            m_window.InitWindowData();
            m_window.InitWindowUI();
        }

        private static GUIStyle hierarchyReferenceStyle;
        public static List<Transform> checkResultGoTransList = new List<Transform>();
        [SerializeField]
        private TreeViewState m_resultTreeViewState;
        private UIComponentCheckResultTableView m_resultTreeView;


        private List<UIComponentCheckFilterBase> m_filterList = new List<UIComponentCheckFilterBase>();
        private List<UIComponentCheckResult> m_results;

        public static string CheckOptionString; //筛选条件
        public static string CheckAddOptionString; //添加筛选条件
        public static string CheckValueString; //按脚本变量值筛选
        public static string CheckExistString; //按脚本是否添加筛选
        public static string CheckButtonString; //查找

        public static string CheckResult_NameString; //Prefab名称
        public static string CheckResult_PathString; //Prefab路径
        public static string CheckResult_ObjectListString; //筛选结果

        public static string CheckComponentString; //检查脚本
        public static string CheckFieldString; //变量名称
        public static string FilterTipsString;//请选择可以添加到GameObject上的脚本
        public static string CompareOptionString; //变量比较选项


        private void InitWindowData()
        {
            m_filterList = new List<UIComponentCheckFilterBase>();
            m_results = new List<UIComponentCheckResult>();

            CheckOptionString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_筛选条件);
            CheckAddOptionString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_添加筛选条件);
            CheckValueString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_按脚本变量值筛选);
            CheckExistString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_按脚本是否添加筛选);
            CheckButtonString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_查找);

            CheckResult_NameString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_检查结果Prefab);
            CheckResult_PathString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_检查结果路径);
            CheckResult_ObjectListString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_检查结果节点);

            CheckComponentString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_检查脚本);
            CheckFieldString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_变量名称);
            FilterTipsString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_检查脚本提示);
            CompareOptionString = EditorLocalization.GetLocalization(EditorLocalizationStorage.Def_比较方式);

            if (m_resultTreeView == null)
            {
                //初始化TreeView
                if (m_resultTreeViewState == null)
                    m_resultTreeViewState = new TreeViewState();
                var headerState = UIComponentCheckResultTableView.CreateDefaultMultiColumnHeaderState(position.width);
                var multiColumnHeader = new MultiColumnHeader(headerState);
                m_resultTreeView = new UIComponentCheckResultTableView(m_resultTreeViewState, multiColumnHeader);
            }
            m_resultTreeView.Reload();
        }

        private void InitWindowUI()
        {

        }

        private void OnEnable()
        {
            hierarchyReferenceStyle = new GUIStyle()
            {
                padding =
            {
                left = EditorStyles.label.padding.left + 1,
                top = EditorStyles.label.padding.top + 1
            },
                normal =
                {
                    textColor = Color.yellow
                }
            };

            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private void OnDisable()
        {
            checkResultGoTransList.Clear();
            EditorApplication.hierarchyWindowItemOnGUI -= HandleHierarchyWindowItemOnGUI;
        }

        static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj != null)
            {
                if (checkResultGoTransList.Contains(obj.transform))
                {
                    EditorGUI.DrawRect(selectionRect, Color.grey); // 绘制背景矩形
                    GUI.Label(selectionRect, obj.name, hierarchyReferenceStyle);
                }
            }
        }

        #region UI
        Vector2 scrollPos;
        private void OnGUI()
        {
            DrawFilterList();
            DrawOptionBar();

            //DrawResultListViewIMGUI();
            DrawResultListView();
        }
        private void DrawFilterList()
        {
            EditorGUILayout.LabelField(CheckOptionString);

            //画个背景
            if (m_filterList.Count == 0)
            {
                EditorGUILayout.HelpBox("", MessageType.None);
            }
            else
            {
                var lastRect = GUILayoutUtility.GetLastRect();
                var bgRectPos = new Vector2(lastRect.x, lastRect.y + lastRect.height + 2);
                float height = (EditorGUIUtility.singleLineHeight + 3) * m_filterList.Count;
                EditorGUI.HelpBox(new Rect(bgRectPos, new Vector2(lastRect.width, height)), "", MessageType.None);
            }

            for (int i = 0; i < m_filterList.Count; i++)
            {
                UIComponentCheckFilterBase filter = m_filterList[i];
                filter.DrawFilterUI();
            }
        }
        private void DrawOptionBar()
        {
            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(CheckAddOptionString, GUILayout.Width(100));

            if (GUILayout.Button(CheckValueString, GUILayout.Width(200))) //Filt by Component Value 
            {
                AddFilter(ComponentFilterType.ComponentValue);
            }

            if (GUILayout.Button(CheckExistString, GUILayout.Width(200))) //Filt by Component Added
            {
                AddFilter(ComponentFilterType.ComponentExist);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(CheckButtonString, GUILayout.Width(100)))
            {
                CheckScripts();
                //查找之后重新生成TreeView
                m_resultTreeView.UpdateTree(m_results);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
        //使用TreeView画列表
        private void DrawResultListView()
        {

            Rect controlrect = EditorGUILayout.GetControlRect(true);
            Rect rect = new Rect(controlrect.x, controlrect.y, position.width, position.height - controlrect.y);
            m_resultTreeView.OnGUI(rect);
        }
        //IMGUI直接画列表，已经不用了
        private void DrawResultListViewIMGUI()
        {
            EditorGUILayout.BeginHorizontal();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            foreach (UIComponentCheckResult result in m_results)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.ObjectField(result.prefabGo, typeof(GameObject), false, GUILayout.Width(200));
                EditorGUILayout.EndVertical();

                EditorGUILayout.LabelField("", GUI.skin.verticalSlider, GUILayout.Height(100), GUILayout.Width(5));

                EditorGUILayout.BeginVertical();
                foreach (string gopath in result.nodePaths)
                {
                    EditorGUILayout.LabelField(gopath, GUILayout.Width(200));
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndHorizontal();
        }
        #endregion


        #region Logic
        /// <summary>
        /// 不再使用了 只是作为反射获取继承类代码的备忘
        /// </summary>
        private void GetMonoComponentListByReflection()
        {
            Dictionary<string, Type> monoComponentFullName2TypeDic = new Dictionary<string, Type>();

            Type baseType = typeof(MonoBehaviour);

            // 获取当前项目中所有程序集中的类型
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(baseType))
                    {
                        monoComponentFullName2TypeDic.Add(type.FullName, type);
                    }
                }
            }
        }

        private void CheckScripts()
        {
            m_results.Clear();

            //var checkAtlasSettings = AssetDatabase.LoadAssetAtPath<UIAtlasCheckRuleSettings>(ThunderFireUIToolConfig.UICheckSettingFullPath);
            //string prefabFolderPath = checkAtlasSettings.prefabFolderPath;
            string[] guids = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets" });

            foreach (string guid in guids)
            {
                var prefabPath = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                var transList = prefab.GetComponentsInChildren<Transform>().ToList();


                var resultGoPaths = new List<string>();
                var resultGoFileIDs = new List<long>();
                bool result = false;
                foreach (var trans in transList)
                {
                    result = Filt(trans.gameObject);
                    if (result)
                    {
                        resultGoPaths.Add(trans.gameObject.name);
                        resultGoFileIDs.Add(UIComponentCheckResultTableView.GetLocalIdentfierInFile(trans.gameObject));
                    }
                }

                if (resultGoPaths.Count > 0)
                {
                    UIComponentCheckResult checkResult = new UIComponentCheckResult();
                    checkResult.prefabPath = prefabPath;
                    checkResult.prefabGo = prefab;
                    checkResult.nodePaths = resultGoPaths;
                    checkResult.nodeFileIds = resultGoFileIDs;
                    m_results.Add(checkResult);
                }
            }
        }

        public void RemoveFilter(UIComponentCheckFilterBase filter)
        {
            m_filterList.Remove(filter);

            Repaint();
        }

        private void AddFilter(ComponentFilterType filterType)
        {
            if (filterType == ComponentFilterType.ComponentExist)
            {
                var filter = new UIComponentExistCheckFilter(this);
                m_filterList.Add(filter);
            }

            if (filterType == ComponentFilterType.ComponentValue)
            {
                var filter = new UIComponentValueCheckFilter(this);
                m_filterList.Add(filter);
            }
        }

        private bool Filt(GameObject go)
        {
            bool result = false;

            foreach (var filter in m_filterList)
            {
                result = filter.Filt(go);
                if (result == false)
                    break;
            }
            return result;
        }
        #endregion

    }
}
