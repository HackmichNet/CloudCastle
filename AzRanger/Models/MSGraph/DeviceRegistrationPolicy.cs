﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class DeviceRegistrationPolicy
    {
        public string odatacontext { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public int userDeviceQuota { get; set; }
        public string multiFactorAuthConfiguration { get; set; }
        public Azureadregistration azureADRegistration { get; set; }
        public Azureadjoin azureADJoin { get; set; }
    }

    public class Azureadregistration
    {
        public string appliesTo { get; set; }
        public bool isAdminConfigurable { get; set; }
        public object[] allowedUsers { get; set; }
        public object[] allowedGroups { get; set; }
    }

    public class Azureadjoin
    {
        public string appliesTo { get; set; }
        public bool isAdminConfigurable { get; set; }
        public object[] allowedUsers { get; set; }
        public object[] allowedGroups { get; set; }
    }

}