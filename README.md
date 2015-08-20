# Deposit
Another pet-project. Currently under development. It's an MVC web app for deposit management and a Windows service for daily interest payments.

##### Now I'am refactoring the solution to implement the Dependecy Injection pattern in the DI branch

At the moment the refactored solution consists of 5 projects (in the DI branch):
* Deposit - an MVC web application (presentation layer)
* DomainLogic - contains the data model and provides the DI interfaces (service layer)
* DepositDatabase - an Entity Framework DB model and data access methods (data access layer)
* InterestPaymentService - a Windows service for daily interest payments
* InterestPaymentServiceSetup - a setup project for InterestPaymentService

Old version consists of 4 projects (in the master branch):
* Deposit - an MVC web application
* DepositDatabase - an Entity Framework DB model and data access methods
* InterestPaymentService - a Windows service for daily interest payments
* InterestPaymentServiceSetup - a setup project for InterestPaymentService

Though the only available deposit type was meant to be a time (term) deposit (that cannot be withdrawn for a certain term or period of time), the withdrawal restriction logic is not implemented yet for easier debugging purposes.

