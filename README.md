# DWSIM Cloud API

A Command-Driven Headless Simulation Engine built on top of DWSIM, exposed via a robust ServiceStack Web API.

## Overview

This project transforms the standalone DWSIM process simulator into a scalable, cloud-ready service. By wrapping the DWSIM headless engine in a ServiceStack message-based architecture, we provide a reliable "Command-Driven" pattern for simulations. This API enables external systems to instantiate flowsheets, manipulate states via discrete commands, and stream solver logs in real-time.

## Architecture

- **Simulation Engine**: DWSIM Automation / Interfaces
- **API Framework**: ServiceStack (.NET)
- **API Pattern**: Message-Based / Command-Driven DTOs
- **Real-Time**: Server-Sent Events (SSE) for streaming solver progress
- **Clients**: Auto-generated typed clients for C#, TypeScript, Python, etc.

## Structure

- `DWSIM.WebApi/`: The main ASP.NET Core / ServiceStack host.
- `DWSIM.Automation/` & `DWSIM.Interfaces/`: The underlying simulation logic from DWSIM.

## Getting Started

To run the Cloud API MVP locally:

```bash
cd DWSIM.WebApi
dotnet build
dotnet run
```

Once running, navigate to the ServiceStack UI to explore the available endpoints, view session states, and test commands via the built-in Locode interface.
