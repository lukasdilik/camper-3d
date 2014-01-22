using System;
using System.Collections.Generic;

namespace ApplicationLogic.Interfaces
{
    public interface IApplicationUI
    {
        void SendMessage(string msg);
        void ShowAvailableModels(List<string> models);
        void UpdateCameraCoordinates(float x, float y, float z);
        void ExceptionOccured(Exception e);
    }
}
