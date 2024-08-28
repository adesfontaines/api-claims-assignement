[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/adesfontaines/api-claims-assignement/e2e.yml)](https://github.com/adesfontaines/api-claims-assignement/actions)
[![codecov](https://codecov.io/github/adesfontaines/api-claims-assignement/graph/badge.svg?token=HNEOXUZ0ZK)](https://codecov.io/github/adesfontaines/api-claims-assignement)
# Insurance Claim API

This API allows for retrieving and updating batches of insurance claims. It is containerized using Docker Compose for easy deployment.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Setup and Run

### Cloning the Repository

```bash
git clone https://github.com/yourusername/insurance-claim-api.git
cd insurance-claim-api
```

## Building and Running with Docker Compose
```bash
docker-compose up --build
```

The API will be accessible at http://localhost:5000.
