﻿using Uni.Scan.Client.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.IO;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using Uni.Scan.Shared.Constants.Storage;
using Uni.Scan.Shared.Enums;
using Uni.Scan.Transfer.Requests.Identity;

namespace Uni.Scan.Client.Pages.Identity
{
    public partial class Profile
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private char _firstLetterOfName;
        private readonly UpdateProfileRequest _profileModel = new();

        public string UserId { get; set; }

        private async Task UpdateProfileAsync()
        {
            var response = await _accountManager.UpdateProfileAsync(_profileModel);
            if (response.Succeeded)
            {
                await _authenticationManager.Logout();
                _snackBar.Add(Localizer["Your Profile has been updated. Please Login to Continue."], Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _global.CurrentTitle = Localizer["MON COMPTE"];
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            _profileModel.Email = user.GetEmail();
            _profileModel.FirstName = user.GetFirstName();
            _profileModel.LastName = user.GetLastName();
            _profileModel.PhoneNumber = user.GetPhoneNumber();
            UserId = user.GetUserId();
            if (_global.IsMobileView == false)
            {
                var data = await _accountManager.GetProfilePictureAsync(UserId);
                if (data.Succeeded)
                {
                    ImageDataUrl = data.Data;
                }
            }


            if (_profileModel.FirstName.Length > 0)
            {
                _firstLetterOfName = _profileModel.FirstName[0];
            }
        }

        private IBrowserFile _file;

        [Parameter] public string ImageDataUrl { get; set; }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var fileName = $"{UserId}-{Guid.NewGuid()}{extension}";
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                var request = new UpdateProfilePictureRequest
                {
                    Data = buffer, FileName = fileName, Extension = extension, UploadType = UploadType.ProfilePicture
                };
                var result = await _accountManager.UpdateProfilePictureAsync(request, UserId);
                if (result.Succeeded)
                {
                    await _localStorage.SetItemAsync(StorageConstants.Local.UserImageUrl, result.Data);
                    _snackBar.Add(Localizer["Profile picture added."], Severity.Success);
                    _navigationManager.NavigateTo("/account", true);
                }
                else
                {
                    foreach (var error in result.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }

        private async Task DeleteAsync()
        {
            var parameters = new DialogParameters
            {
                {
                    nameof(Shared.Dialogs.DeleteConfirmationDialog.ContentText),
                    $"{string.Format(Localizer["Do you want to delete the profile picture of {0}"], _profileModel.Email)}?"
                }
            };
            var options = new DialogOptions
                { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog =
                _dialogService.Show<Shared.Dialogs.DeleteConfirmationDialog>(Localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var request = new UpdateProfilePictureRequest
                    { Data = null, FileName = string.Empty, UploadType = UploadType.ProfilePicture };
                var data = await _accountManager.UpdateProfilePictureAsync(request, UserId);
                if (data.Succeeded)
                {
                    await _localStorage.RemoveItemAsync(StorageConstants.Local.UserImageUrl);
                    ImageDataUrl = string.Empty;
                    _snackBar.Add(Localizer["Profile picture deleted."], Severity.Success);
                    _navigationManager.NavigateTo("/account", true);
                }
                else
                {
                    foreach (var error in data.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }
    }
}