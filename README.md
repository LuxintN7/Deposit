# Deposit
Another pet-project. Currently under development. It's an MVC web app for deposit management and a Windows service for daily interest payments.

##### A branch for implementation of the Dependecy Injection pattern 

At the moment the refactored solution consists of 5 projects:
* Deposit - an MVC web application (presentation layer)
* DomainLogic - contains the data model and provides the DI interfaces (service layer)
* DepositDatabase - an Entity Framework DB model and data access methods (data access layer)
* InterestPaymentService - a Windows service for daily interest payments
* InterestPaymentServiceSetup - a setup project for InterestPaymentService
