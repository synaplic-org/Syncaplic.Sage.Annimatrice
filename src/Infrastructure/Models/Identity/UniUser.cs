﻿using Uni.Scan.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uni.Scan.Shared.Enums;


namespace Uni.Scan.Infrastructure.Models.Identity
{
    public class UniUser : IdentityUser<string>, IAuditableEntity<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string CreatedBy { get; set; }

        [Column(TypeName = "text")]
        public string ProfilePictureDataUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }
        public string SiteID { get; set; }
        public bool IsDeleted { get; set; }
        public string EmployeeID { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public UserType UserType { get; set; } = UserType.Gestionaire;


        public UniUser()
        {
        }
    }
}