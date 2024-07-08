using Game;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using YooAsset;
using YooAsset.Editor;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Assets)]
    public class BuildTask_BuildYooAsset : IBuildTask
    {
        private AssetBundleCollectorSetting _setting;

        private string _versionName;

        public void Run(BuildContext context)
        {
            _setting = SettingLoader.LoadSettingData<AssetBundleCollectorSetting>();

            _versionName = GetVersionName();

            // 构建主资源包
            var mainPackageName = context.BuildParameters.mainPackageName;
            var mainPackage = _setting.Packages.FirstOrDefault(x => x.PackageName == mainPackageName);
            if (mainPackage != null)
            {
                Debug.Log($"Build main package : {mainPackage.PackageName}");
                BuildSBPPackage(context, mainPackage.PackageName);
            }

            // 构建原生文件资源包
            var rawFilePackageName = context.BuildParameters.rawFilePackageName;
            var rawFilePackage = _setting.Packages.FirstOrDefault(x => x.PackageName == rawFilePackageName);
            if (rawFilePackage != null)
            {
                Debug.Log($"Build raw file package : {rawFilePackage.PackageName}");
                BuildRawFilePackage(context, rawFilePackage.PackageName);
            }
        }
        private void BuildSBPPackage(BuildContext context, string packageName)
        {
            var enableHotUpdate = context.BuildParameters.enableHotupdate;
            var buildMode = EBuildMode.IncrementalBuild;
            var fileNameStyle = EFileNameStyle.HashName;
            var buildinFileCopyOption = enableHotUpdate ? EBuildinFileCopyOption.ClearAndCopyByTags : EBuildinFileCopyOption.ClearAndCopyAll;
            var buildinFileCopyParams = enableHotUpdate ? "local;Local;LOCAL" : null;
            var compressOption = ECompressOption.Uncompressed;
            var encryptionServices = (IEncryptionServices)null; //暂时不考虑加密

            var buildParameters = new ScriptableBuildParameters();
            buildParameters.BuildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
            buildParameters.BuildinFileRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot();
            buildParameters.BuildPipeline = EBuildPipeline.ScriptableBuildPipeline.ToString();
            buildParameters.BuildTarget = context.BuildParameters.BuildTarget;
            buildParameters.BuildMode = buildMode;
            buildParameters.PackageName = packageName;
            buildParameters.PackageVersion = _versionName;
            buildParameters.EnableSharePackRule = true;
            buildParameters.VerifyBuildingResult = true;
            buildParameters.FileNameStyle = fileNameStyle;
            buildParameters.BuildinFileCopyOption = buildinFileCopyOption;
            buildParameters.BuildinFileCopyParams = buildinFileCopyParams;
            buildParameters.CompressOption = compressOption;
            buildParameters.EncryptionServices = encryptionServices;

            var pipeline = new ScriptableBuildPipeline();
            var buildResult = pipeline.Run(buildParameters, true);
            if (!buildResult.Success)
            {
                throw new Exception($"Build {packageName} failed ! , {buildResult.ErrorInfo}");
            }
            EditorUtility.RevealInFinder(buildResult.OutputPackageDirectory);
        }

        private void BuildRawFilePackage(BuildContext context, string packageName)
        {
            var enableHotUpdate = context.BuildParameters.enableHotupdate;
            var buildMode = EBuildMode.ForceRebuild;
            var fileNameStyle = EFileNameStyle.HashName;
            var buildinFileCopyOption = enableHotUpdate ? EBuildinFileCopyOption.ClearAndCopyByTags : EBuildinFileCopyOption.ClearAndCopyAll;
            var buildinFileCopyParams = enableHotUpdate ? "local;Local;LOCAL" : null;
            var encryptionServices = (IEncryptionServices)null; //暂时不考虑加密

            var buildParameters = new RawFileBuildParameters();
            buildParameters.BuildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
            buildParameters.BuildinFileRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot();
            buildParameters.BuildPipeline = EBuildPipeline.RawFileBuildPipeline.ToString();
            buildParameters.BuildTarget =  context.BuildParameters.BuildTarget;
            buildParameters.BuildMode = buildMode;
            buildParameters.PackageName = packageName;
            buildParameters.PackageVersion = _versionName;
            buildParameters.VerifyBuildingResult = true;
            buildParameters.FileNameStyle = fileNameStyle;
            buildParameters.BuildinFileCopyOption = buildinFileCopyOption;
            buildParameters.BuildinFileCopyParams = buildinFileCopyParams;
            buildParameters.EncryptionServices = encryptionServices;

            var pipeline = new RawFileBuildPipeline();
            var buildResult = pipeline.Run(buildParameters, true);
            if (!buildResult.Success)
            {
                throw new Exception($"Build {packageName} failed ! , {buildResult.ErrorInfo}");
            }
            EditorUtility.RevealInFinder(buildResult.OutputPackageDirectory);
        }

        private string GetVersionName()
        {
            //yyyy-MM-dd-HHmm
            var date = DateTime.Now;
            return date.ToString("yyyy-MM-dd-HHmmss");
        }
    }
}
