// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.ServiceFabric.Powershell.Http
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using Microsoft.ServiceFabric.Common;

    /// <summary>
    /// Gets the health of the specified Service Fabric service, by using the specified health policy.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SFServiceHealthUsingPolicy")]
    public partial class GetServiceHealthUsingPolicyCmdlet : CommonCmdletBase
    {
        /// <summary>
        /// Gets or sets ServiceId. The identity of the service. This ID is typically the full name of the service without the
        /// 'fabric:' URI scheme.
        /// Starting from version 6.0, hierarchical names are delimited with the "~" character.
        /// For example, if the service name is "fabric:/myapp/app1/svc1", the service identity would be "myapp~app1~svc1" in
        /// 6.0+ and "myapp/app1/svc1" in previous versions.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, Position = 0)]
        public string ServiceId { get; set; }

        /// <summary>
        /// Gets or sets EventsHealthStateFilter. Allows filtering the collection of HealthEvent objects returned based on
        /// health state.
        /// The possible values for this parameter include integer value of one of the following health states.
        /// Only events that match the filter are returned. All events are used to evaluate the aggregated health state.
        /// If not specified, all entries are returned. The state values are flag-based enumeration, so the value could be a
        /// combination of these values, obtained using the bitwise 'OR' operator. For example, If the provided value is 6 then
        /// all of the events with HealthState value of OK (2) and Warning (4) are returned.
        /// 
        /// - Default - Default value. Matches any HealthState. The value is zero.
        /// - None - Filter that doesn't match any HealthState value. Used in order to return no results on a given collection
        /// of states. The value is 1.
        /// - Ok - Filter that matches input with HealthState value Ok. The value is 2.
        /// - Warning - Filter that matches input with HealthState value Warning. The value is 4.
        /// - Error - Filter that matches input with HealthState value Error. The value is 8.
        /// - All - Filter that matches input with any HealthState value. The value is 65535.
        /// </summary>
        [Parameter(Mandatory = false, Position = 1)]
        public int? EventsHealthStateFilter { get; set; }

        /// <summary>
        /// Gets or sets PartitionsHealthStateFilter. Allows filtering of the partitions health state objects returned in the
        /// result of service health query based on their health state.
        /// The possible values for this parameter include integer value of one of the following health states.
        /// Only partitions that match the filter are returned. All partitions are used to evaluate the aggregated health
        /// state.
        /// If not specified, all entries are returned. The state values are flag-based enumeration, so the value could be a
        /// combination of these value
        /// obtained using bitwise 'OR' operator. For example, if the provided value is 6 then health state of partitions with
        /// HealthState value of OK (2) and Warning (4) will be returned.
        /// 
        /// - Default - Default value. Matches any HealthState. The value is zero.
        /// - None - Filter that doesn't match any HealthState value. Used in order to return no results on a given collection
        /// of states. The value is 1.
        /// - Ok - Filter that matches input with HealthState value Ok. The value is 2.
        /// - Warning - Filter that matches input with HealthState value Warning. The value is 4.
        /// - Error - Filter that matches input with HealthState value Error. The value is 8.
        /// - All - Filter that matches input with any HealthState value. The value is 65535.
        /// </summary>
        [Parameter(Mandatory = false, Position = 2)]
        public int? PartitionsHealthStateFilter { get; set; }

        /// <summary>
        /// Gets or sets ConsiderWarningAsError. Indicates whether warnings are treated with the same severity as errors.
        /// </summary>
        [Parameter(Mandatory = false, Position = 3)]
        public bool? ConsiderWarningAsError { get; set; } = false;

        /// <summary>
        /// Gets or sets MaxPercentUnhealthyDeployedApplications. The maximum allowed percentage of unhealthy deployed
        /// applications. Allowed values are Byte values from zero to 100.
        /// The percentage represents the maximum tolerated percentage of deployed applications that can be unhealthy before
        /// the application is considered in error.
        /// This is calculated by dividing the number of unhealthy deployed applications over the number of nodes where the
        /// application is currently deployed on in the cluster.
        /// The computation rounds up to tolerate one failure on small numbers of nodes. Default percentage is zero.
        /// </summary>
        [Parameter(Mandatory = false, Position = 4)]
        public int? MaxPercentUnhealthyDeployedApplications { get; set; } = 0;

        /// <summary>
        /// Gets or sets MaxPercentUnhealthyPartitionsPerService. The maximum allowed percentage of unhealthy partitions per
        /// service. Allowed values are Byte values from zero to 100
        /// 
        /// The percentage represents the maximum tolerated percentage of partitions that can be unhealthy before the service
        /// is considered in error.
        /// If the percentage is respected but there is at least one unhealthy partition, the health is evaluated as Warning.
        /// The percentage is calculated by dividing the number of unhealthy partitions over the total number of partitions in
        /// the service.
        /// The computation rounds up to tolerate one failure on small numbers of partitions. Default percentage is zero.
        /// </summary>
        [Parameter(Mandatory = false, Position = 5)]
        public int? MaxPercentUnhealthyPartitionsPerService { get; set; } = 0;

        /// <summary>
        /// Gets or sets MaxPercentUnhealthyReplicasPerPartition. The maximum allowed percentage of unhealthy replicas per
        /// partition. Allowed values are Byte values from zero to 100.
        /// 
        /// The percentage represents the maximum tolerated percentage of replicas that can be unhealthy before the partition
        /// is considered in error.
        /// If the percentage is respected but there is at least one unhealthy replica, the health is evaluated as Warning.
        /// The percentage is calculated by dividing the number of unhealthy replicas over the total number of replicas in the
        /// partition.
        /// The computation rounds up to tolerate one failure on small numbers of replicas. Default percentage is zero.
        /// </summary>
        [Parameter(Mandatory = false, Position = 6)]
        public int? MaxPercentUnhealthyReplicasPerPartition { get; set; } = 0;

        /// <summary>
        /// Gets or sets MaxPercentUnhealthyServices. The maximum allowed percentage of unhealthy services. Allowed values are
        /// Byte values from zero to 100.
        /// 
        /// The percentage represents the maximum tolerated percentage of services that can be unhealthy before the application
        /// is considered in error.
        /// If the percentage is respected but there is at least one unhealthy service, the health is evaluated as Warning.
        /// This is calculated by dividing the number of unhealthy services of the specific service type over the total number
        /// of services of the specific service type.
        /// The computation rounds up to tolerate one failure on small numbers of services. Default percentage is zero.
        /// </summary>
        [Parameter(Mandatory = false, Position = 7)]
        public int? MaxPercentUnhealthyServices { get; set; } = 0;

        /// <summary>
        /// Gets or sets ServiceTypeHealthPolicyMap. The map with service type health policy per service type name. The map is
        /// empty by default.
        /// </summary>
        [Parameter(Mandatory = false, Position = 8)]
        public IEnumerable<ServiceTypeHealthPolicyMapItem> ServiceTypeHealthPolicyMap { get; set; }

        /// <summary>
        /// Gets or sets ExcludeHealthStatistics. Indicates whether the health statistics should be returned as part of the
        /// query result. False by default.
        /// The statistics show the number of children entities in health state Ok, Warning, and Error.
        /// </summary>
        [Parameter(Mandatory = false, Position = 9)]
        public bool? ExcludeHealthStatistics { get; set; }

        /// <summary>
        /// Gets or sets ServerTimeout. The server timeout for performing the operation in seconds. This timeout specifies the
        /// time duration that the client is willing to wait for the requested operation to complete. The default value for
        /// this parameter is 60 seconds.
        /// </summary>
        [Parameter(Mandatory = false, Position = 10)]
        public long? ServerTimeout { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecordInternal()
        {
            var serviceTypeHealthPolicy = new ServiceTypeHealthPolicy(
            maxPercentUnhealthyPartitionsPerService: this.MaxPercentUnhealthyPartitionsPerService,
            maxPercentUnhealthyReplicasPerPartition: this.MaxPercentUnhealthyReplicasPerPartition,
            maxPercentUnhealthyServices: this.MaxPercentUnhealthyServices);

            var applicationHealthPolicy = new ApplicationHealthPolicy(
            considerWarningAsError: this.ConsiderWarningAsError,
            maxPercentUnhealthyDeployedApplications: this.MaxPercentUnhealthyDeployedApplications,
            defaultServiceTypeHealthPolicy: serviceTypeHealthPolicy,
            serviceTypeHealthPolicyMap: this.ServiceTypeHealthPolicyMap);

            var result = this.ServiceFabricClient.Services.GetServiceHealthUsingPolicyAsync(
                serviceId: this.ServiceId,
                eventsHealthStateFilter: this.EventsHealthStateFilter,
                partitionsHealthStateFilter: this.PartitionsHealthStateFilter,
                applicationHealthPolicy: applicationHealthPolicy,
                excludeHealthStatistics: this.ExcludeHealthStatistics,
                serverTimeout: this.ServerTimeout,
                cancellationToken: this.CancellationToken).GetAwaiter().GetResult();

            if (result != null)
            {
                this.WriteObject(this.FormatOutput(result));
            }
        }

        /// <inheritdoc/>
        protected override object FormatOutput(object output)
        {
            var outputResult = output as ServiceHealth;

            var partitionHealthStatesObj = new PSObject(outputResult.PartitionHealthStates);
            partitionHealthStatesObj.Members.Add(new PSCodeMethod("ToString", typeof(OutputFormatter).GetMethod("FormatObject")));

            var healthEventsObj = new PSObject(outputResult.HealthEvents);
            healthEventsObj.Members.Add(new PSCodeMethod("ToString", typeof(OutputFormatter).GetMethod("FormatObject")));

            var healthStatisticsObj = new PSObject(outputResult.HealthStatistics);
            healthStatisticsObj.Members.Add(new PSCodeMethod("ToString", typeof(OutputFormatter).GetMethod("FormatObject")));

            var result = new PSObject(outputResult);

            result.Properties.Add(new PSNoteProperty("ServiceName", outputResult.Name));

            return result;
        }
    }
}
