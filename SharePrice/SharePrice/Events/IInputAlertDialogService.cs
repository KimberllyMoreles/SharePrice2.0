using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharePrice.Controls.InputViews;

namespace SharePrice.Events
{
    public interface IInputAlertDialogService
    { 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleText"></param>
        /// <param name="placeHolderText"></param>
        /// <param name="saveButtonText"></param>
        /// <param name="cancelButtonText"></param>
        /// <param name="validationText"></param>
        /// <returns></returns>
        Task<string> OpenCancellableTextInputAlertDialog(string titleText, 
            string placeHolderText, string saveButtonText, 
            string cancelButtonText, string validationText);
        
    }
}
