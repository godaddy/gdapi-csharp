gdapi-csharp
============

A C# client for Go Daddy&reg; REST APIs.


Requirements
---------
* Json.NET
* An account in a compatible service, such as [Cloud Servers&trade;](http://www.godaddy.com/hosting/cloud-computing.aspx)
* Your API Access and Secret key pair

Getting Started
--------
If you haven't already tried it, open up the base URL of the API you want to use in a browser.
Enter your Access Key as the username and your Secret Key as the password.
This interface will allow you to get familiar with what Resource types are available in the API
and what operations and actions you can perform on them.  For more information, see the [documentation site](http://docs.cloud.secureserver.net/).

Setting Up
---------

### Using the source
Download the source:
> git clone https://github.com/godaddy/gdapi-csharp.git

Create a client:
```csharp

string url        = "https://api.cloud.secureserver.net/v1/";
string access_key = "your-access-key";
string secret_key = "your-secret-key";

Client gdapi = new Client(url, access_key, secret_key);

```

Finding Resources
--------
Resources are pulled from the API by the Client.

### Listing all resources in a collection
```csharp

Collection machines = gdapi.getCollection("virtualmachines");
Console.WriteLine( "There are " + machines.getResources().Count + " machines:" );

foreach ( Resource machine in machines )
{
  Console.WriteLine( machine.getProperty( "name" ) );
}

```
    
### Filtering
Filters allow you to search a collection for resources matching a set of conditions.
```csharp

CollectionFilter filter = new CollectionFilter("publicStartPort","gt","80");
Collection networkrules = gdapi.getCollection("networkrules",filter);
Console.WriteLine( "There are " + networkrules.getResources().Count + " networkrules with a publicStartPort greater than 80." );

```

### Getting a single Resource by ID
If you know the ID of the resource you are looking for already, you can also get it directly.
```csharp

Resource machine = gdapi.getResourceById("virtualmachines","your-machine-id");

```

Working with Resources
--------

### Accessing attributes
Attributes of a Resource are stored in a properties dictionary inside a Resource object. To access a particular resource the getProperty method is available.
```csharp

Resource machine = gdapi.getResourceById("virtualmachines","your-machine-id");

String privateIp = machine.getProperty("privateIpv4Address"); // e.g. '10.1.1.3'
String size = machine.getProperty("ramSizeMb"); // e.g. 1024

```

### Making changes
```csharp

Resource machine = gdapi.getResourceById("virtualmachines","your-machine-id");
machine.setProperty("name","bigger machine");
machine.setProperty("offering","2gb-4cpu");

// Save the changes
Resource result = gdapi.save("virtualmachines",machine);

```

### Creating new resources
```csharp

Resource network = new Resource();
network.setProperty("name","My Network");
network.setProperty("domain","mynetwork.local");
network.setProperty("ipv4Cidr","192.168.0.0/24");

Resource result = gdapi.create("networks",network);

```

### Removing resources

With an instance of the resource:
```csharp

Resource machine = gdapi.getResourceById("virtualmachines","your-machine-id");
Resource result = gdapi.delete("virtualmachines",machine);

```

### Executing Actions
Actions are used to perform operations that go beyond simple create/read/update/delete. Available actions are stored in the actions Dictionary of the Resource object.

```csharp

Resource machine = gdapi.getResourceById("virtualmachines","your-machine-id");
Resource result = gdapi.doAction(machine.getAction("restart"));

```

Following Links
--------
Response collections and resources generally have a "links" attribute containing URLs to related resources and collections.  For example a virtual machine belongs to a network and has one or more volumes.
```csharp

Resource machine = gdapi.getResourceById("virtualmachines","your-machine-id");
Resource network = gdapi.fetchLinkAsResource(machine,"network"); // Network resource
Collection volumes = gdapi.fetchLinkAsCollection(machine,"volumes"); // Collection of Volume resources

```

Handling Errors
--------
By default, any error response will be thrown as an exception.  The most general type of exception is APIException.
```csharp

try
{
  Resource machine = gdapi.getResourceById("virtualmachines","your-machine-id");
  Console.WriteLine( "I found it" );
}
catch ( NotFoundException e )
{
  Console.WriteLine( "I couldn't find that machine" );
}
catch ( APIException e )
{
  Console.WriteLine( "Something else went wrong" );
}

```
