# Tests API with .NET 9, RestSharp, and NUnit

This project contains automated tests for an API using [.NET 9](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0), [RestSharp](https://restsharp.dev/docs/intro), and [NUnit](https://nunit.org). The API used in this project is from [FakeStoreAPI](https://fakestoreapi.com/docs).

## Table of Contents

- [Introduction](#introduction)
- [Setup](#setup)
- [Running Tests](#running-tests)
- [Generating Reports](#generating-reports)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This project is designed to test the endpoints of the FakeStoreAPI. It uses .NET 9 for the framework, RestSharp for making HTTP requests, and NUnit for writing and running the tests.

## Setup

To set up the project locally, follow these steps:

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/your-repo.git
    cd your-repo
    ```

2. Install the required .NET version:
    ```sh
    dotnet --version
    # Ensure it shows .NET 9.x.x
    ```

3. Restore the dependencies:
    ```sh
    dotnet restore
    ```

## Running Tests

To run the tests, use the following command:
```sh
dotnet test --logger "trx;LogFileName=report.trx"