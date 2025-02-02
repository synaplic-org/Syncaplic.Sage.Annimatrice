﻿using Uni.Scan.Application.Interfaces.Common;
using Uni.Scan.Shared.Wrapper;
using System.Threading.Tasks;
using Uni.Scan.Transfer.Requests.Identity;

namespace Uni.Scan.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}