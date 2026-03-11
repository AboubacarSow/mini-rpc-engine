# Lightweight RPC Engine (C#)

A minimal Remote Procedure Call (RPC) framework built with TCP sockets in C#.

The project explores how RPC systems work internally by implementing the core mechanisms from scratch.

---

## What is RPC?

A **Remote Procedure Call (RPC)** is a communication model that allows a program to execute a method on another machine (or process) as if it were a local function call.

Instead of manually handling networking, serialization, and communication protocols, developers simply call a method and the RPC framework takes care of sending the request to the remote service and returning the response.

### The RPC Lifecycle

Conceptually, an RPC call looks like this:

```text
Client code
    ↓
Local proxy (stub)
    ↓
Network transport
    ↓
Remote server
    ↓
Service method execution
    ↓
Response returned to the client

```

Internally, the framework performs several steps to make this feel like a normal method call:

1. **Serialization:** Marshal the request into a byte stream.
2. **Transport:** Send it through the network via TCP.
3. **Unmarshalling:** On the server, the request is reconstructed.
4. **Execution:** The actual method is invoked on the server instance.
5. **Return:** The response is serialized and sent back to the caller.

---

## Features

* **TCP-based transport:** Uses `TcpListener` and `TcpClient` for low-level communication.
* **Length-prefixed message framing:** Implements a protocol to identify message boundaries in a continuous stream.
* **JSON serialization:** Handles request and response payloads using JSON.
* **Reflection-based dispatching:** Automatically maps incoming requests to server-side methods.
* **Dynamic client proxies:** Utilizes `DispatchProxy` to intercept interface calls.

---

## Architecture

The framework is organized into multiple layers to ensure a separation of concerns:

| Layer | Responsibility |
| --- | --- |
| **Transport** | Handles TCP connections and raw data transmission. |
| **Protocol** | Defines message framing (Length-prefixing) for data integrity. |
| **Marshaller** | Serializes and deserializes RPC messages (JSON). |
| **Core** | Handles request dispatching and service invocation using Reflection. |
| **Wrapper** | Abstracts the Server/Client to mimic modern frameworks like gRPC. |

> **Note:** While gRPC is the industry standard framework built by Google.

---

## Project Structure

```text
engine
  ├───Core           # Dispatching and Proxy logic
  │   └───Interfaces
  ├───Extensions     # Dependency Injection helpers
  ├───Marshaller     # Serialization (JSON)
  │   ├───Interfaces
  │   └───Models     
  ├───Protocol       # RpcRequest and RpcResponse definitions
  ├───Transport      # TCP Sockets implementation, Framing 
  └───Wrapper        # High-level API for easy integration

```

---

## Usage Guide

### Server-Side Registration

To host a service, register it within your service collection:

```csharp
builder.Services.AddRpcEngine();

builder.Services.AddServices(config =>
{
    // Register the service implementation
    config.AddService(nameof(MyService), new MyService());
});

builder.Services.AddScoped<IMyService, MyService>();

```

### Client-Side Invocation

The client uses an interface to generate a dynamic proxy, allowing for a seamless developer experience:

```csharp
// 1. Establish the connection channel
var channel = RpcChannel.ForAddress(50000);

// 2. Create the proxy based on the shared interface
var myserviceClient = RpcProxyFactory.Create<IMyService>(channel);

// 3. Execute the call
var forecast =  myserviceClient.GetForecast();

```
<br/>

IMyService should look like this bellow:
```csharp
public interface IMyService{
  object GetForcast();
}

```


---

## Resources Explored

The following resources were instrumental in building this engine:

* **C# Networking (Dev.to):** Insights into Raw Sockets, TCP, and UDP programming for building the Transport layer.
* **Internal Structuring:** Studied existing RPC repositories to understand how to decouple the Marshaller from the Transport protocol.
* **Community Knowledge:** Utilized **StackOverflow** for resolving edge cases in `DispatchProxy` and asynchronous socket handling.

---

## Goal

The goal of this project is educational: to understand the internal mechanics behind industry-standard frameworks such as **gRPC**, **Apache Thrift**, and **WCF**. By implementing a simplified engine, it demonstrates how complex networking can be abstracted into simple local method calls.

---

## Status
- As you can see, the port of the server is hardcoded, later on, this will be the next improvement that will be done by dynamically configuring the port.

