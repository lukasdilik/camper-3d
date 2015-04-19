using System.Collections.Generic;
using System.Drawing;
using ApplicationLogic.Scene;

namespace ApplicationLogic.Interfaces
{
    public interface IApplicationUI
    {
        string GetSelectedModelName();
        void SendMessage(string msg);
        void ShowAvailableModels(List<string> models);
        void UpdateStatusBarInfo(string info);
        void ModelAdded(ModelProperties modelProperties);
        void ModelSelected(ModelProperties modelProperties);
        void ModelRemoved(string modelName);
        void LightAdded(LightProperties lightProperties);
        void LightSelected(LightProperties lightProperties);
        void LightRemoved(string lightName);
        void CameraAdded(SecurityCameraProperties cameraProperties);
        void CameraRemoved(string cameraName);
        void CameraSelected(SecurityCameraProperties cameraProperties);
        void UpdateCameraProperties(SecurityCameraProperties cameraProperties);
        void UpdateLightProperties(LightProperties lightProperties);
        Size GetCameraPreviewDimension();
        void UpdateCameraView(SecurityCameraProperties properties, Bitmap bmp);
        void UpdateCameraOrientation(int yawDeg, int pitchDeg);
        void ActiveModeChanged(AppController.Mode newMode);
        void ClearPreview();
        void Close();
        void LogMessage(string msg);
    }
}
