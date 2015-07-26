# Deposit
Another pet-project. Currently under development. It's an MVC web app for deposit management and a Windows service for daily interest payments.

At the moment the solution consists of 4 projects:
* Deposit - MVC web application
* DepositDatabase - Entity Framework DB model and data access methods
* InterestPaymentService - Windows service for daily interest payments
* InterestPaymentServiceSetup - setup project for InterestPaymentService

For now the next step is to separate the DB from client apps entirely by adding a WCF service as a DB access provider.

Though the only available deposit type was meant to be a time (term) deposit (that cannot be withdrawn for a certain term or period of time), the withdrawal restriction logic is not implemented yet for easier debugging purposes.

