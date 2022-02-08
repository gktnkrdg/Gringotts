# Bank Api


![Heroku deployment](https://github.com/gktnkrdg/Gringotts/actions/workflows/heroku-deploy.yaml/badge.svg)
## Description

GringottsApi is .NET 5 Web API. PostgreSQL is used as database, provided by Heroku. EFCore is used as ORM. Implement optimistic concurency(Concurency Token, xmin) for transaction consistency. Endpoints secured with bearer authentication.


## Tech

- .NET 5
- Postgresql 
- Entity Framework Core
- FluentValidation 
- Bearer Authentication
- Docker
- Github Actions
- Heroku
- Error Handling Middleware

## Demo
https://gringotts-api.herokuapp.com/swagger


## Endpoints

The available APIs include:

- POST /api/v1.0/authentication/login - Authenticates a user
- POST /api/v1.0/bank-accounts - Create new bank account
- GET  /api/v1.0/bank-accounts - List customer all accounts
- GET  /api/v1.0/bank-accounts/{bankAccountId} - Detail customer account
- POST /api/v1.0/bank-accounts/{bankAccountId}/deposit - Add money to bank account
- POST /api/v1.0/bank-accounts/{bankAccountId}/withdraw - Withdraw money from bank account
- GET /api/v1.0/bank-accounts/{bankAccountId}/transactions - List transactions of an account
- POST /api/v1.0/customers - Create new Customer
- GET /api/v1.0/customers/me - Get customer information
- GET /api/v1.0/transactions -List transactions of customer between a time period

## Authentication
Authentication Endpoint: 

[HttpPost] /api/v1.0/authentication/login 

Example username password : test@test.com - 123456

## Install
Local
- git clone

- cd src/Gringotts.Api 

- dotnet run

----
Docker
- docker build -t GringottsApi .

- docker run -d -p 8080:80 --name api GringottsApi

