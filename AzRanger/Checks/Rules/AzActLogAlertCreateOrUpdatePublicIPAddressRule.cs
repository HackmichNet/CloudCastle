﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzActLogAlertCreateOrUpdatePublicIPAddressRule", ScopeEnum.Azure, MaturityLevel.Tentative, "https://portal.azure.com/#blade/Microsoft_Azure_Monitoring/AzureMonitoringBrowseBlade/alertsV2", ServiceEnum.StorageAccount)]
    [CISAZ("5.2.9", "", Level.L1, "v1.5")]
    [RuleInfo("No Activity Log Alert for 'Create or Update Public IP Address rule'", @"Unwanted changes for ""Public IP Address Rules"" can go unnoticed.", 0, null, null)]
    internal class AzActLogAlertCreateOrUpdatePublicIPAddressRule : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                bool wantedAllertRuleExist = false;
                foreach (ActivityLogAlert alert in sub.Resources.ActivityLogAlerts)
                {
                    if(alert.location == "Global" && alert.properties.enabled == true)
                    {
                        bool scopeIsEntireSubscription = false;
                        foreach (String scope in alert.properties.scopes)
                        {
                            if (scope == sub.id)
                            {
                                scopeIsEntireSubscription = true;
                            }
                        }
                        if (scopeIsEntireSubscription)
                        {
                            foreach(ActivityLogAlertAllof allOf in alert.properties.condition.allOf)
                            {
                                if(allOf.field == "operationName" && allOf.equals.ToLower() == "microsoft.network/publicipaddresses/write")
                                {
                                    wantedAllertRuleExist = true;
                                }
                            }
                        }
                    }
                }
                if(wantedAllertRuleExist == false)
                {
                    this.AddAffectedEntity(sub);
                    passed = false;
                }
            }
            
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}