using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using SharePrice.Events;
using SharePrice.Controls;
using SharePrice.Controls.InputViews;

namespace SharePrice.Service
{
    public class InputAlertDialogService : IInputAlertDialogService
    {
        public async Task<string> OpenCancellableTextInputAlertDialog(string titleText, string placeHolderText, string saveButtonText,
            string cancelButtonText, string validationText)
        {
            // create the TextInputView
            var inputView = new TextInputCancellableView(titleText, placeHolderText, saveButtonText,
             cancelButtonText,  validationText);

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((TextInputCancellableView)sender).TextInputResult))
                    {
                        ((TextInputCancellableView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((TextInputCancellableView)sender).TextInputResult);
                    }
                    else
                    {
                        ((TextInputCancellableView)sender).IsValidationLabelVisible = true;
                    }
                };

            // subscribe to the TextInputView's Button click event
            inputView.CancelButtonEventHandler +=
                (sender, obj) =>
                {
                    popup.PageClosedTaskCompletionSource.SetResult(null);
                };

            // return user inserted text value
            return await Navigate(popup);
        }
        

        /// <summary>
        /// Handle popup page Navigation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="popup"></param>
        /// <returns></returns>
        private async Task<T> Navigate<T>(InputAlertDialogBase<T> popup)
        {
            // Push the page to Navigation Stack
            await PopupNavigation.PushAsync(popup);

            // await for the user to enter the text input
            var result = await popup.PageClosedTask;

            // Pop the page from Navigation Stack
            await PopupNavigation.PopAsync();

            return result;
        }
    }
}
