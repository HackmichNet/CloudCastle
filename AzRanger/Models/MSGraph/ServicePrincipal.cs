﻿using AzRanger.Models.Generic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{

    public class ServicePrincipal : IEntity
    {
        public Guid id { get; set; }
        public string appDisplayName { get; set; }
        public Guid appId { get; set; }
        public string appOwnerOrganizationId { get; set; }
        public Passwordcredential[] passwordCredentials { get; set; }
        public KeyCredentials[] keyCredentials { get; set; }
        public Oauth2permissionscopes[] oauth2PermissionScopes { get; set; }
        public IDTypeResponse[] owners { get; set; }
        public Approleassignment[] appRoleAssignments { get; set; }
        public List<AzurePrincipal> userAbleToAddCreds = new List<AzurePrincipal>();

        public bool CanAddCredentials(Guid id)
        {
            foreach(IDTypeResponse response in this.owners)
            {
                if(response.id == id)
                {
                    return true;
                }
            }
            foreach(AzurePrincipal user in this.userAbleToAddCreds)
            {
                if(user.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", this.appDisplayName, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", id, this.appDisplayName);
        }
    }
}