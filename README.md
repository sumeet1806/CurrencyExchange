# Currency Exchange API

## About
The Currency Exchange API is a .NET Core web application that allows users to convert currencies, manage user balances, and perform various financial transactions. This API integrates with external currency exchange services to provide real-time exchange rates and supports user authentication for secure access.

## Assumptions
During the development of this application, the following assumptions were made:
1. Users have valid authentication credentials for accessing restricted endpoints.
2. External currency exchange services are reliable and provide accurate exchange rates.
3. User balances are stored securely and transactions are validated to prevent unauthorized access.

## Getting Started
To set up and run the Currency Exchange API on your local machine, follow these steps:

### Prerequisites
- .NET Core SDK installed on your machine
- Git installed for version control

### Installation
1. Clone the repository to your local machine:
   ```bash
   git clone https://github.com/sumeet1806/CurrencyExchange.git

## Notes
1. Currency Exchanges was developed by assuming no sql server/ database
2. Authentication /Authorization logic is implemented but throwing error since the connection string is not provided
3. Sessions are used to store users balance for creating / Getting / Updating users balance
4. Create the user first with Initial Balance first and then user can update/Get the user balance.
