# Loan Application

## Motivation

I came across this excellent article [How to create better code using Domain-Driven Design](https://www.altkomsoftware.com/blog/create-better-code-using-domain-driven-design/), however it didn't really delve into the thorny issue of validation.

The most common approach I've seen is for input validation to occur at the web layer (typically using a framework such as FluentValidation), but then not re-validated at the domain layer. Additional domain invariants are then handled within the domain layer via exceptions. Typically a limited number of exceptions are thrown that are then mapped to specific status codes at the web layer (`ValidationException`, `NotFoundException`, `DomainException`). This is often the most pragmatic approach as it's difficult to flow validation issus all the way from the domain layer back to the web layer, but it means you sacrafice an [Always-Valid Domain Model](https://enterprisecraftsmanship.com/posts/always-valid-domain-model/).

I wanted to see if it was possible to handle ALL validation at the domain layer as simply as possible and without using exceptions for control flow.

## Getting Started

This project references a sql server database and a seq instance for logging.

By default, the database will be configured to run in a docker container and already has the connection
string configured in your launch settings.

To start the sql server container and seq instance, run the following from the root directory.

`docker-compose up`

### Running the API

Once you have your database(s) running, you can run the API either from your favourite IDE or by running the following command from the root directory.

`dotnet run --project src\LoanApplication.WebHost`

Once the API is running you can view the API documentation at [https://localhost:5001/index.html](https://localhost:5001/index.html).

See the [HTTP request](src/LoanApplication.http) file for some sample valid and invalid requests.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
