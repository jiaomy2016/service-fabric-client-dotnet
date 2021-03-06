# Unregister-SFApplicationType
Removes or unregisters a Service Fabric application type from the cluster.
## Description

This operation can only be performed if all application instances of the application type have been deleted. Once the 
application type is unregistered, no new application instances can be created for this particular application type.



## Required Parameters
#### -ApplicationTypeName

The name of the application type.



#### -ApplicationTypeVersion

The version of the application type as defined in the application manifest.



## Optional Parameters
#### -Async

The flag indicating whether or not unprovision should occur asynchronously. When set to true, the unprovision 
operation returns when the request is accepted by the system, and the unprovision operation continues without any 
timeout limit. The default value is false. However, we recommend setting it to true for large application packages 
that were provisioned.



#### -ServerTimeout

The server timeout for performing the operation in seconds. This timeout specifies the time duration that the client 
is willing to wait for the requested operation to complete. The default value for this parameter is 60 seconds.



