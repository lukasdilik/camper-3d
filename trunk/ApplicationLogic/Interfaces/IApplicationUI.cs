using System;
using System.Collections.Generic;

namespace ApplicationLogic.Interfaces
{
    public interface IApplicationUI
    {
        void ModelSucessfullyLoaded(string modeFileName);
        void ShowAvailableModels(List<string> models);
        void ExceptionOccured(Exception e);
    }
}
