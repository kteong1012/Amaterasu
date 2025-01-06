using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

public class CreateRenderTextureFeature : ScriptableRendererFeature
{
    
    [System.Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent injectionPoint = RenderPassEvent.AfterRenderingPostProcessing;
        public Material passMaterial;
        public int passIndex = 0;
        public LayerMask layerMask = ~0; // Default to all layers
        public string rtName = "_ChaMaskTexture";
    }
    CreateRenderTexture customPass;
    
    public CustomPassSettings settings = new CustomPassSettings();

    public override void Create()
    {
        customPass = new CreateRenderTexture(settings);
        customPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(customPass);
    }

    class CreateRenderTexture : ScriptableRenderPass
    {
        private CustomPassSettings settings;
        
        //private ProfilingSampler m_ProfilingSampler = new ProfilingSampler("CustomFullScreenRender");
        private RTHandle m_InputHandle;
        private RTHandle m_OutputHandle;
        private const string k_OutputName = "_ChaMaskTexture";
        private static readonly int m_OutputId = Shader.PropertyToID(k_OutputName);
        private Material m_Material;
        
        private Material materialToUse;
        private FilteringSettings filterSettingsToUse;
        
        public CreateRenderTexture(CustomPassSettings settings)
        {
            this.settings = settings;
        }
        
        class PassData
        {
            internal TextureHandle cameraColorTexture;
            public RendererListHandle rendererListHandle;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameContext)
        {
            using (var builder = renderGraph.AddRasterRenderPass<PassData>("Create Render texture", out var passData))
            {
                #region Obsolete code

                // // Get the data needed to create the list of objects to draw
                // UniversalRenderingData renderingData = frameContext.Get<UniversalRenderingData>();
                // UniversalCameraData cameraData = frameContext.Get<UniversalCameraData>();
                // UniversalLightData lightData = frameContext.Get<UniversalLightData>();
                // SortingCriteria sortFlags = cameraData.defaultOpaqueSortFlags;
                // RenderQueueRange renderQueueRange = RenderQueueRange.opaque;
                // FilteringSettings filterSettings = new FilteringSettings(renderQueueRange, ~0);
                //
                // RenderTextureDescriptor descriptor = cameraData.cameraTargetDescriptor;
                // // 设置宽高
                // descriptor.width = cameraData.camera.pixelWidth;
                // descriptor.height = cameraData.camera.pixelHeight;
                // // 确保格式为 RGBA
                // descriptor.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
                // // 禁用深度缓冲区
                // descriptor.depthBufferBits = 0;
                //
                // // 分配 RTHandle
                // RenderingUtils.ReAllocateHandleIfNeeded(
                //     ref m_OutputHandle,
                //     descriptor,
                //     FilterMode.Bilinear,
                //     TextureWrapMode.Clamp,
                //     descriptor.msaaSamples,
                //     descriptor.depthBufferBits,
                //     k_OutputName
                // );
                //
                //
                // // Redraw only objects that have their LightMode tag set to UniversalForward 
                // ShaderTagId shadersToOverride = new ShaderTagId("UniversalForward");
                //
                // // Create drawing settings
                // DrawingSettings drawSettings = RenderingUtils.CreateDrawingSettings(shadersToOverride, renderingData, cameraData, lightData, sortFlags);
                //
                // // Add the override material to the drawing settings
                // drawSettings.overrideMaterial = materialToUse;
                //
                // // Create the list of objects to draw
                // var rendererListParameters = new RendererListParams(renderingData.cullResults, drawSettings, filterSettings);
                //
                // // Convert the list to a list handle that the render graph system can use
                // passData.rendererListHandle = renderGraph.CreateRendererList(rendererListParameters);
                //
                // // Create a temporary texture
                // TextureHandle texture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "My texture", false);
                // // var textureHandle_00 = renderGraph.ImportTexture(m_OutputHandle);
                // // builder.SetGlobalTextureAfterPass(textureHandle_00,m_OutputId);
                //
                // Shader.SetGlobalTexture(m_OutputId,m_OutputHandle);
                //
                // // Set the texture as the render target
                // //builder.SetRenderAttachment(texture, 0, AccessFlags.Write);
                // UniversalResourceData resourceData = frameContext.Get<UniversalResourceData>();
                // builder.UseRendererList(passData.rendererListHandle);
                // builder.SetRenderAttachment(resourceData.activeColorTexture, 0);
                // builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Write);
                //
                // builder.AllowPassCulling(false);
                //
                // builder.SetRenderFunc((PassData data, RasterGraphContext context) => ExecutePass(data, context));
                //

                #endregion
 
                UniversalResourceData resourceData = frameContext.Get<UniversalResourceData>();
                UniversalRenderingData renderingData = frameContext.Get<UniversalRenderingData>();
                UniversalCameraData cameraData = frameContext.Get<UniversalCameraData>();
                UniversalLightData lightData = frameContext.Get<UniversalLightData>();

                // 配置 RenderTextureDescriptor
                var descriptor = cameraData.cameraTargetDescriptor;
                descriptor.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
                descriptor.depthBufferBits = 0;

                // 分配 RTHandle
                RenderingUtils.ReAllocateHandleIfNeeded(
                    ref m_OutputHandle,
                    descriptor,
                    FilterMode.Bilinear,
                    TextureWrapMode.Clamp,
                    descriptor.msaaSamples,
                    descriptor.depthBufferBits,
                    settings.rtName
                );

                Shader.SetGlobalTexture(settings.rtName, m_OutputHandle);
                // 设置 RenderGraph 输出纹理
                TextureHandle rtHandle = renderGraph.ImportTexture(m_OutputHandle);

                // 配置 ShaderTag 和 DrawingSettings
                var sortFlags = cameraData.defaultOpaqueSortFlags;
                var filterSettings = new FilteringSettings(RenderQueueRange.opaque, settings.layerMask);
                var drawingSettings = RenderingUtils.CreateDrawingSettings(new ShaderTagId("UniversalForward"), renderingData, cameraData, lightData, sortFlags);

                // 添加自定义材质
                drawingSettings.overrideMaterial = settings.passMaterial;
                drawingSettings.overrideMaterialPassIndex = settings.passIndex;

                // 渲染对象列表
                var rendererListParams = new RendererListParams(renderingData.cullResults, drawingSettings, filterSettings);
                passData.rendererListHandle = renderGraph.CreateRendererList(rendererListParams);

                // 设置 RenderTarget
                builder.UseRendererList(passData.rendererListHandle);
                builder.SetRenderAttachment(rtHandle, 0, AccessFlags.Write);

                builder.SetRenderFunc((PassData data, RasterGraphContext context) => ExecutePass(data, context));
  
            }
        }

        static void ExecutePass(PassData data, RasterGraphContext context)
        {          
            // // Clear the render target to yellow
            context.cmd.ClearRenderTarget(true, true, Color.black);            
            
            context.cmd.DrawRendererList(data.rendererListHandle);
        }
    }

}